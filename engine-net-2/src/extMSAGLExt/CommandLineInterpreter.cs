// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Executes parsed GrGen-yComp protocol commands (keyword plus arguments) on an IBasicGraphViewerClient
    /// (typically MSAGLExtClient); responses are written to the stream the command arrived on.
    /// Must be used from the UI thread (see GraphCommandReceiver), so the client calls stay on the UI thread.
    /// </summary>
    public class CommandLineInterpreter
    {
        readonly IBasicGraphViewerClient client;
        bool stopped;

        public CommandLineInterpreter(IBasicGraphViewerClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Ends the waiting for an element in a pending waitForElement command (the client is about to be disposed).
        /// </summary>
        public void Stop()
        {
            stopped = true;
        }

        /// <summary>
        /// Executes one command given as list of arguments (command keyword first, as split by the CommandLineParser).
        /// Returns true if the connection is to be closed (exit command).
        /// Throws a ProtocolSyntaxException upon syntax errors; semantic errors are reported by exceptions from the client and passed on.
        /// </summary>
        public async Task<bool> InterpretAsync(List<string> args, NetworkStream stream)
        {
            if(args.Count == 0)
                throw new ProtocolSyntaxException("empty command line");

            string cmd = args[0];

            switch(cmd)
            {
                case "addNodeRealizer":
                    CheckArgumentCount(args, 6);
                    client.AddNodeRealizer(args[1], CommandLineParser.ParseColor(args[2]), CommandLineParser.ParseColor(args[3]), CommandLineParser.ParseColor(args[4]), CommandLineParser.ParseShape(args[5]));
                    break;

                case "addEdgeRealizer":
                    CheckArgumentCount(args, 6);
                    client.AddEdgeRealizer(args[1], CommandLineParser.ParseColor(args[2]), CommandLineParser.ParseColor(args[3]), CommandLineParser.ParseInt(args[4]), CommandLineParser.ParseLineStyle(args[5]));
                    break;

                case "addSubgraphNode":
                    CheckArgumentCount(args, 5);
                    client.AddSubgraphNode(args[2], args[3], args[4]);
                    break;

                case "addNode":
                    CheckArgumentCount(args, 5);
                    client.AddNode(args[2], args[3], args[4]);
                    break;

                case "addEdge":
                    CheckArgumentCount(args, 6);
                    client.AddEdge(args[1], args[2], args[3], args[4], args[5]);
                    break;

                case "moveNode":
                    CheckArgumentCount(args, 3);
                    client.MoveNode(args[1], args[2]);
                    break;

                case "changeNode":
                    CheckArgumentCount(args, 3);
                    client.ChangeNode(args[1], args[2]);
                    break;

                case "changeEdge":
                    CheckArgumentCount(args, 3);
                    client.ChangeEdge(args[1], args[2]);
                    break;

                case "changeNodeAttr":
                    CheckArgumentCount(args, 4);
                    {
                        string ownerType, attrName, attrType;
                        CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType);
                        client.SetNodeAttribute(args[1], ownerType, attrName, attrType, args[3]);
                    }
                    break;

                case "changeEdgeAttr":
                    CheckArgumentCount(args, 4);
                    {
                        string ownerType, attrName, attrType;
                        CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType);
                        client.SetEdgeAttribute(args[1], ownerType, attrName, attrType, args[3]);
                    }
                    break;

                case "clearNodeAttr":
                    CheckArgumentCount(args, 3);
                    {
                        string ownerType, attrName, attrType;
                        CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType);
                        client.ClearNodeAttribute(args[1], ownerType, attrName, attrType);
                    }
                    break;

                case "clearEdgeAttr":
                    CheckArgumentCount(args, 3);
                    {
                        string ownerType, attrName, attrType;
                        CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType);
                        client.ClearEdgeAttribute(args[1], ownerType, attrName, attrType);
                    }
                    break;

                case "setNodeLabel":
                    CheckArgumentCount(args, 3);
                    client.SetNodeLabel(args[1], args[2]);
                    break;

                case "setEdgeLabel":
                    CheckArgumentCount(args, 3);
                    client.SetEdgeLabel(args[1], args[2]);
                    break;

                case "deleteNode":
                    CheckArgumentCount(args, 2);
                    client.DeleteNode(args[1]);
                    break;

                case "deleteEdge":
                    CheckArgumentCount(args, 2);
                    client.DeleteEdge(args[1]);
                    break;

                case "deleteGraph":
                    CheckArgumentCount(args, 1);
                    client.ClearGraph();
                    break;

                case "setLayout":
                    CheckArgumentCount(args, 2);
                    client.SetLayout(args[1]);
                    break;

                case "getLayoutOptions":
                {
                    CheckArgumentCount(args, 1);
                    string opts = client.GetLayoutOptions();
                    byte[] data = Encoding.ASCII.GetBytes(opts + "endoptions\n");
                    await stream.WriteAsync(data, 0, data.Length);
                    break;
                }

                case "setLayoutOption":
                    CheckArgumentCount(args, 3);
                    {
                        string result = client.SetLayoutOption(args[1], args[2]);
                        byte[] data = Encoding.ASCII.GetBytes(result);
                        await stream.WriteAsync(data, 0, data.Length);
                    }
                    break;

                case "layout":
                    CheckArgumentCount(args, 1);
                    client.ForceLayout();
                    break;

                case "show":
                    CheckArgumentCount(args, 1);
                    client.Show();
                    break;

                case "sync":
                {
                    CheckArgumentCount(args, 1);
                    byte[] data = Encoding.ASCII.GetBytes("sync\n");
                    await stream.WriteAsync(data, 0, data.Length);
                    break;
                }

                case "waitForElement":
                {
                    CheckArgumentCount(args, 2);
                    if(args[1] != "true" && args[1] != "false")
                        throw new ProtocolSyntaxException("command \"waitForElement\" expects true or false, but got \"" + args[1] + "\"");
                    bool enable = args[1] == "true";
                    client.WaitForElement(enable);
                    if(enable)
                    {
                        // poll the UI-thread CommandAvailable flag; each await yields to the message pump
                        while(!stopped && !client.CommandAvailable && !client.ConnectionLost)
                            await Task.Delay(10);
                        if(!stopped && !client.ConnectionLost)
                        {
                            string response = client.ReadCommand();
                            byte[] data = Encoding.ASCII.GetBytes(response);
                            await stream.WriteAsync(data, 0, data.Length);
                        }
                    }
                    break;
                }

                case "renameNode":
                case "renameEdge":
                    // deprecated -- not supported by MSAGL
                    throw new ProtocolSyntaxException("command \"" + cmd + "\" is deprecated and not supported");

                case "exit":
                    CheckArgumentCount(args, 1);
                    return true; // signal to close connection

                default:
                    throw new ProtocolSyntaxException("unknown command \"" + cmd + "\"");
            }
            return false;
        }

        // expectedCount includes the command keyword in args[0];
        // missing as well as superfluous arguments are syntax errors
        static void CheckArgumentCount(List<string> args, int expectedCount)
        {
            if(args.Count != expectedCount)
            {
                throw new ProtocolSyntaxException("command \"" + args[0] + "\" expects " + (expectedCount - 1)
                    + " argument(s) but got " + (args.Count - 1));
            }
        }
    }
}
