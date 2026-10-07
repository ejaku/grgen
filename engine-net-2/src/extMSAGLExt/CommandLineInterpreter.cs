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

        public CommandLineInterpreter(IBasicGraphViewerClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Executes one command given as list of arguments (command keyword first, as split by the CommandLineParser).
        /// Returns true if the connection is to be closed (exit command).
        /// </summary>
        public async Task<bool> InterpretAsync(List<string> args, NetworkStream stream)
        {
            if(args.Count == 0)
            {
                Console.Error.WriteLine("extMSAGLExt: warning: empty command line ignored");
                return false;
            }

            string cmd = args[0];

            switch(cmd)
            {
                case "addNodeRealizer":
                    if(CheckArgumentCount(args, 6))
                        client.AddNodeRealizer(args[1], CommandLineParser.ParseColor(args[2]), CommandLineParser.ParseColor(args[3]), CommandLineParser.ParseColor(args[4]), CommandLineParser.ParseShape(args[5]));
                    break;

                case "addEdgeRealizer":
                    if(CheckArgumentCount(args, 6))
                    {
                        int lw; int.TryParse(args[4], out lw);
                        client.AddEdgeRealizer(args[1], CommandLineParser.ParseColor(args[2]), CommandLineParser.ParseColor(args[3]), lw, CommandLineParser.ParseLineStyle(args[5]));
                    }
                    break;

                case "addSubgraphNode":
                    if(CheckArgumentCount(args, 5))
                        client.AddSubgraphNode(args[2], args[3], CommandLineParser.Decode(args[4]));
                    break;

                case "addNode":
                    if(CheckArgumentCount(args, 5))
                        client.AddNode(args[2], args[3], CommandLineParser.Decode(args[4]));
                    break;

                case "addEdge":
                    if(CheckArgumentCount(args, 6))
                        client.AddEdge(args[1], args[2], args[3], args[4], CommandLineParser.Decode(args[5]));
                    break;

                case "moveNode":
                    if(CheckArgumentCount(args, 3))
                        client.MoveNode(args[1], args[2]);
                    break;

                case "changeNode":
                    if(CheckArgumentCount(args, 3))
                        client.ChangeNode(args[1], args[2]);
                    break;

                case "changeEdge":
                    if(CheckArgumentCount(args, 3))
                        client.ChangeEdge(args[1], args[2]);
                    break;

                case "changeNodeAttr":
                    if(CheckArgumentCount(args, 4))
                    {
                        string ownerType, attrName, attrType;
                        if(CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType))
                            client.SetNodeAttribute(args[1], ownerType, attrName, attrType, CommandLineParser.Decode(args[3]));
                    }
                    break;

                case "changeEdgeAttr":
                    if(CheckArgumentCount(args, 4))
                    {
                        string ownerType, attrName, attrType;
                        if(CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType))
                            client.SetEdgeAttribute(args[1], ownerType, attrName, attrType, CommandLineParser.Decode(args[3]));
                    }
                    break;

                case "clearNodeAttr":
                    if(CheckArgumentCount(args, 3))
                    {
                        string ownerType, attrName, attrType;
                        if(CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType))
                            client.ClearNodeAttribute(args[1], ownerType, attrName, attrType);
                    }
                    break;

                case "clearEdgeAttr":
                    if(CheckArgumentCount(args, 3))
                    {
                        string ownerType, attrName, attrType;
                        if(CommandLineParser.SplitAttrKey(args[2], out ownerType, out attrName, out attrType))
                            client.ClearEdgeAttribute(args[1], ownerType, attrName, attrType);
                    }
                    break;

                case "setNodeLabel":
                    if(CheckArgumentCount(args, 3))
                        client.SetNodeLabel(args[1], CommandLineParser.Decode(args[2]));
                    break;

                case "setEdgeLabel":
                    if(CheckArgumentCount(args, 3))
                        client.SetEdgeLabel(args[1], CommandLineParser.Decode(args[2]));
                    break;

                case "deleteNode":
                    if(CheckArgumentCount(args, 2))
                        client.DeleteNode(args[1]);
                    break;

                case "deleteEdge":
                    if(CheckArgumentCount(args, 2))
                        client.DeleteEdge(args[1]);
                    break;

                case "deleteGraph":
                    CheckArgumentCount(args, 1);
                    client.ClearGraph();
                    break;

                case "setLayout":
                    if(CheckArgumentCount(args, 2))
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
                    if(CheckArgumentCount(args, 3))
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
                    bool enable = CheckArgumentCount(args, 2) && args[1] == "true";
                    client.WaitForElement(enable);
                    if(enable)
                    {
                        // poll the UI-thread CommandAvailable flag; each await yields to the message pump
                        while(!client.CommandAvailable && !client.ConnectionLost)
                            await Task.Delay(10);
                        if(!client.ConnectionLost)
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
                    Console.Error.WriteLine("extMSAGLExt: command \"" + cmd + "\" is deprecated and not supported");
                    break;

                case "exit":
                    CheckArgumentCount(args, 1);
                    return true; // signal to close connection

                default:
                    Console.Error.WriteLine("extMSAGLExt: unknown command \"" + cmd + "\"");
                    break;
            }
            return false;
        }

        // expectedCount includes the command keyword in args[0];
        // returns false if arguments are missing (command is then ignored), warns about superfluous ones (which are ignored)
        static bool CheckArgumentCount(List<string> args, int expectedCount)
        {
            if(args.Count < expectedCount)
            {
                Console.Error.WriteLine("extMSAGLExt: command \"" + args[0] + "\" expects " + (expectedCount - 1)
                    + " argument(s) but got only " + (args.Count - 1) + " -- command ignored");
                return false;
            }
            if(args.Count > expectedCount)
            {
                Console.Error.WriteLine("extMSAGLExt: warning: command \"" + args[0] + "\" expects " + (expectedCount - 1)
                    + " argument(s) but got " + (args.Count - 1) + " -- superfluous arguments are ignored");
            }
            return true;
        }
    }
}
