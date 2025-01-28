/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define DUMP_COMMANDS_TO_YCOMP

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Class starting the yComp server (on a specific socket)
    /// </summary>
    public class YCompServerProxy
    {
        /// <summary>
        /// Searches for a free TCP port in the range 4242-4251.
        /// To be called in order to obtain a free yComp port to i) start yComp at ii) communicate with yComp.
        /// </summary>
        /// <returns>A free TCP port, or throws an exception if all are occupied</returns>
        public static int GetFreeTCPPort()
        {
            for(int i = 4242; i < 4252; ++i)
            {
                try
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, i);
                    // Check whether the current socket is already open by connecting to it
                    using(Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                    {
                        try
                        {
                            socket.Connect(endpoint);
                            socket.Disconnect(false);
                            // Someone is already listening at the current port, so try another one
                            continue;
                        }
                        catch(SocketException)
                        {
                        } // Nobody there? Good...
                    }

                    // Unable to connect, so try to bind the current port.
                    // Trying to bind directly (without the connect-check before), does not
                    // work on Windows Vista even with ExclusiveAddressUse set to true (which does not work on Mono).
                    // It will bind to already used ports without any notice.
                    using(Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                        socket.Bind(endpoint);
                }
                catch(SocketException)
                {
                    continue;
                }
                return i;
            }

            throw new Exception("Didn't find a free TCP port in the range 4242-4251!");
        }

        /// <summary>
        /// Starts yComp (acting as a local server) at the given port (throws an exception if it fails so).
        /// The preferred way to obtain a port is GetFreeTCPPort().
        /// </summary>
        /// <param name="ycompPort">The port to start yComp at</param>
        public YCompServerProxy(int ycompPort)
        {
            try
            {
                port = ycompPort;
                viewerProcess = Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    + Path.DirectorySeparatorChar + "ycomp", "--nomaximize -p " + ycompPort);
            }
            catch(Exception e)
            {
                throw new Exception("Unable to start yComp: " + e.ToString());
            }
        }

        /// <summary>
        /// Ends the yComp process.
        /// </summary>
        public void Close()
        {
            viewerProcess.Close();
        }

        public readonly Process viewerProcess;
        public readonly int port;
    }

    /// <summary>
    /// The stream over which the client communicates with yComp
    /// </summary>
    class YCompStream
    {
        NetworkStream stream;
        readonly byte[] readBuffer = new byte[4096];
        bool closing = false;

        public event ConnectionLostHandler OnConnectionLost;

#if DUMP_COMMANDS_TO_YCOMP
        StreamWriter dumpWriter;
#endif

        public YCompStream(TcpClient client)
        {
            stream = client.GetStream();

#if DUMP_COMMANDS_TO_YCOMP
            dumpWriter = new StreamWriter("ycomp_dump.txt");
#endif
        }

        public void Write(String message)
        {
            try
            {
#if DUMP_COMMANDS_TO_YCOMP
                dumpWriter.Write(message);
                dumpWriter.Flush();
#endif
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch(Exception)
            {
                stream = null;
                if(closing)
                    return;
#if DUMP_COMMANDS_TO_YCOMP
                dumpWriter.Write("connection lost!\n");
                dumpWriter.Flush();
#endif
                if(OnConnectionLost != null)
                    OnConnectionLost();
            }
        }

#if DUMP_COMMANDS_TO_YCOMP
        public void Dump(String message)
        {
            dumpWriter.Write(message);
            dumpWriter.Flush();
        }
#endif

        /// <summary>
        /// Reads up to 4096 bytes from the stream
        /// </summary>
        /// <returns>The read bytes converted to a String using ASCII encoding</returns>
        public String Read()
        {
            try
            {
                int bytesRead = stream.Read(readBuffer, 0, 4096);
                return Encoding.ASCII.GetString(readBuffer, 0, bytesRead);
            }
            catch(Exception)
            {
                stream = null;
                if(OnConnectionLost != null)
                    OnConnectionLost();
                return null;
            }
        }

        public bool Ready
        {
            get
            {
                if(stream == null)
                {
                    if(OnConnectionLost != null)
                        OnConnectionLost();
                    return false;
                }

                try
                {
                    return stream.DataAvailable;
                }
                catch(Exception)
                {
                    stream = null;
                    if(OnConnectionLost != null)
                        OnConnectionLost();
                    return false;
                }
            }
        }

        public bool IsStreamOpen
        {
            get { return stream != null; }
        }
        public bool Closing
        {
            get { return closing; }
            set { closing = value; }
        }
    }


    /// <summary>
    /// Class communicating with yComp over a socket via the GrGen-yComp protocol,
    /// mainly telling yComp what should be displayed (and how)
    /// </summary>
    public class YCompClient : IBasicGraphViewerClient
    {
        TcpClient ycompClient;
        internal YCompStream ycompStream;
        
        private static Dictionary<String, bool> availableLayouts;


        static YCompClient()
        {
            availableLayouts = new Dictionary<string, bool>();
            availableLayouts.Add("Random", true);
            availableLayouts.Add("Hierarchic", true);
            availableLayouts.Add("Organic", true);
            availableLayouts.Add("Orthogonal", true);
            availableLayouts.Add("Circular", true);
            availableLayouts.Add("Tree", true);
            availableLayouts.Add("Diagonal", true);
            availableLayouts.Add("Incremental Hierarchic", true);
            availableLayouts.Add("Compilergraph", true);
        }

        /// <summary>
        /// Creates a new YCompClient instance and connects to the local YComp server.
        /// If it is not available an Exception is thrown.
        /// </summary>
        public YCompClient(int connectionTimeout, int port)
        {
            try
            {
                int startTime = Environment.TickCount;

                do
                {
                    try
                    {
                        ycompClient = new TcpClient("localhost", port);
                    }
                    catch(SocketException)
                    {
                        ycompClient = null;
                        Thread.Sleep(1000);
                    }
                }
                while(ycompClient == null && Environment.TickCount - startTime < connectionTimeout);

                if(ycompClient == null)
                    throw new Exception("Connection timeout!");

                ycompStream = new YCompStream(ycompClient);

                // TODO: Add group related events
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to yComp at port " + port + ": " + ex.Message);
            }
        }

        public void Close()
        {
            if(ycompStream.IsStreamOpen)
            {
                ycompStream.Closing = true;     // don't care if exit doesn't work
                ycompStream.Write("exit\n");
            }
            ycompClient.Close();
            ycompClient = null;
        }

        public void SleepAndDoEvents()
        {
            System.Threading.Thread.Sleep(1);
        }


        public static IEnumerable<String> AvailableLayouts
        {
            get { return availableLayouts.Keys; }
        }

        public static bool IsValidLayout(String layoutName)     // TODO: allow case insensitive layout name
        {
            return availableLayouts.ContainsKey(layoutName);
        }

        public event ConnectionLostHandler OnConnectionLost
        {
            add { ycompStream.OnConnectionLost += value; }
            remove { ycompStream.OnConnectionLost -= value; }
        }

        public bool CommandAvailable
        {
            get { return ycompStream.Ready; }
        }

        public bool ConnectionLost
        {
            get { return !ycompStream.IsStreamOpen; }
        }

        public String ReadCommand()
        {
            return ycompStream.Read();
        }

        /// <summary>
        /// Sets the current layouter of yComp
        /// </summary>
        /// <param name="moduleName">The name of the layouter.
        ///     Can be one of:
        ///     - Random
        ///     - Hierarchic
        ///     - Organic
        ///     - Orthogonal
        ///     - Circular
        ///     - Tree
        ///     - Diagonal
        ///     - Incremental Hierarchic
        ///     - Compilergraph
        /// </param>
        public void SetLayout(String moduleName)
        {
            ycompStream.Write("setLayout \"" + moduleName + "\"\n");
        }

        /// <summary>
        /// Retrieves the available options of the current layouter of yComp and the current values.
        /// </summary>
        /// <returns>A description of the available options of the current layouter of yComp
        /// and the current values.</returns>
        public String GetLayoutOptions()
        {
            ycompStream.Write("getLayoutOptions\n");
            String msg = "";
            do
            {
                msg += ycompStream.Read();
            }
            while(!msg.EndsWith("endoptions\n"));
            return msg.Substring(0, msg.Length - 11);       // remove "endoptions\n" from message
        }

        /// <summary>
        /// Sets a layout option of the current layouter of yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value.</param>
        /// <returns>"optionset\n", or an error message, if setting the option failed.</returns>
        public String SetLayoutOption(String optionName, String optionValue)
        {
            ycompStream.Write("setLayoutOption \"" + optionName + "\" \"" + optionValue + "\"\n");
            String msg = ycompStream.Read();
            return msg;
        }

        /// <summary>
        /// Forces yComp to relayout the graph.
        /// </summary>
        public void ForceLayout()
        {
            ycompStream.Write("layout\n");
        }

        /// <summary>
        /// Shows the graph (without relayout).
        /// </summary>
        public void Show()
        {
            ycompStream.Write("show\n");
        }

        /// <summary>
        /// Sends a "sync" request and waits for a "sync" answer
        /// </summary>
        public bool Sync()
        {
            ycompStream.Write("sync\n");
            return ycompStream.Read() == "sync\n";
        }

        public void AddSubgraphNode(String name, String nrName, String nodeLabel)
        {
            ycompStream.Write("addSubgraphNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + nodeLabel + "\"\n"); // -1 is for subgraph parent, could be added directly instead of a later MoveNode
        }

        public void AddNode(String name, String nrName, String nodeLabel)
        {
            ycompStream.Write("addNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + nodeLabel + "\"\n"); // -1 is for subgraph parent, could be added directly instead of a later MoveNode
        }

        public void SetNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            ycompStream.Write("changeNodeAttr \"n" + name + "\" \"" + ownerTypeName + "::" + attrTypeName + " : "
                + attrTypeString + "\" \"" + attrValueString + "\"\n");
        }

        public void AddEdge(String edgeName, String srcName, String tgtName, String edgeRealizerName, String edgeLabel)
        {
            ycompStream.Write("addEdge \"e" + edgeName + "\" \"n" + srcName + "\" \"n" + tgtName
                + "\" \"" + edgeRealizerName + "\" \"" + edgeLabel + "\"\n");
        }

        public void SetEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            ycompStream.Write("changeEdgeAttr \"e" + name + "\" \"" + ownerTypeName + "::" + attrTypeName + " : "
                + attrTypeString + "\" \"" + attrValueString + "\"\n");
        }

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        public void ChangeNode(String nodeName, String realizer)
        {
            ycompStream.Write("changeNode \"n" + nodeName + "\" \"" + realizer + "\"\n");
        }

        /// <summary>
        /// Sets the edge realizer of the given edge.
        /// If realizer is null, the realizer for the type of the edge is used.
        /// </summary>
        public void ChangeEdge(String edgeName, String realizer)
        {
            ycompStream.Write("changeEdge \"e" + edgeName + "\" \"" + realizer + "\"\n");
        }

        public void SetNodeLabel(String name, String label)
        {
            ycompStream.Write("setNodeLabel \"n" + name + "\" \"" + label + "\"\n");
        }

        public void SetEdgeLabel(String name, String label)
        {
            ycompStream.Write("setEdgeLabel \"e" + name + "\" \"" + label + "\"\n");
        }

        public void ClearNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            ycompStream.Write("clearNodeAttr \"n" + name + "\" \"" + ownerTypeName + "::" + attrTypeName + " : "
                + attrTypeString + "\"\n");
        }

        public void ClearEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            ycompStream.Write("clearEdgeAttr \"e" + name + "\" \"" + ownerTypeName + "::" + attrTypeName + " : "
                + attrTypeString + "\"\n");
        }

        public void DeleteNode(String nodeName)
        {
            ycompStream.Write("deleteNode \"n" + nodeName + "\"\n");
        }

        public void DeleteEdge(String edgeName)
        {
            // TODO: Update group relation
            ycompStream.Write("deleteEdge \"e" + edgeName + "\"\n");
        }

        public void RenameNode(String oldName, String newName)
        {
            ycompStream.Write("renameNode \"n" + oldName + "\" \"n" + newName + "\"\n"); // changes the name(==id) by which the node is accessible
        }

        public void RenameEdge(String oldName, String newName)
        {
            ycompStream.Write("renameEdge \"e" + oldName + "\" \"e" + newName + "\"\n"); // changes the name(==id) by which the edge is accessible
        }

        public void ClearGraph()
        {
            ycompStream.Write("deleteGraph\n");
        }

        public void WaitForElement(bool val)
        {
            ycompStream.Write("waitForElement " + (val ? "true" : "false") + "\n");
        }

        public void MoveNode(String srcName, String tgtName)
        {
            ycompStream.Write("moveNode \"n" + srcName + "\" \"n" + tgtName + "\"\n");
        }

        public void AddNodeRealizer(String name, GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape)
        {
            ycompStream.Write("addNodeRealizer \"" + name + "\" \""
                                + VCGDumper.GetColor(borderColor) + "\" \""
                                + VCGDumper.GetColor(color) + "\" \""
                                + VCGDumper.GetColor(textColor) + "\" \""
                                + VCGDumper.GetNodeShape(nodeShape) + "\"\n");
        }

        public void AddEdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            ycompStream.Write("addEdgeRealizer \"" + name + "\" \""
                                + VCGDumper.GetColor(color) + "\" \""
                                + VCGDumper.GetColor(textColor) + "\" \""
                                + lineWidth + "\" \"" 
                                + VCGDumper.GetLineStyle(lineStyle) + "\"\n");
        }

        public String Encode(String str)
        {
            if(str == null)
                return "";

            StringBuilder sb = new StringBuilder(str);
            sb.Replace("  ", " &nbsp;");
            sb.Replace("\n", "\\n");
            sb.Replace("\"", "&quot;");
            return sb.ToString();
        }
    }
}
