/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using System.Collections.Generic;

namespace DebuggerExample
{
    class DebuggerExample
    {
        /// <summary>
        /// Opens the debugger.
        /// </summary>
        private static Debugger OpenDebugger(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            Dictionary<String, String> optMap = new Dictionary<String, String>();
            DebuggerEnvironment debuggerEnv = new DebuggerEnvironment();
            DebuggerGraphProcessingEnvironment debuggerProcEnv = new DebuggerGraphProcessingEnvironment(graph, procEnv);
            Debugger debugger = new Debugger(debuggerEnv, debuggerProcEnv, new ElementRealizers(), "Organic", optMap);
            debugger.DetailedModeShowPreMatches = true;
            debugger.DetailedModeShowPostMatches = true;
            debuggerEnv.Debugger = debugger;
            return debugger;
        }

        /// <summary>
        /// Ensures that the yComp display is up to date, prints out a message, and waits for a key press.
        /// </summary>
        /// <param name="text">The message to be printed.</param>
        private static void PrintAndWait(String text, Debugger debugger)
        {
            if(debugger != null && debugger.YCompClient != null)
                debugger.YCompClient.UpdateDisplay();
            if(debugger != null && debugger.YCompClient != null)
                debugger.YCompClient.Sync();
            Console.WriteLine(text);
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey(true);
        }

        // example showing how to debug a sequence at API level (also rendering the graph)
        public static void Main(string[] args)
        {
            LGSPNamedGraph graph;
            LGSPGlobalVariables globalVars = new LGSPGlobalVariables();
            LGSPActions actions;
            LGSPGraphProcessingEnvironment procEnv;

            try
            {
                LGSPBackend.Instance.CreateNamedFromSpec("Mutex.grg", globalVars, null, 0, out graph, out actions);
                procEnv = new LGSPGraphProcessingEnvironment(graph, actions);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to create graph from specification: " + ex.Message);
                return;
            }

            NodeType processType = graph.GetNodeType("Process");
            EdgeType nextType = graph.GetEdgeType("next");

            LGSPNode p1 = graph.AddLGSPNode(processType);
            LGSPNode p2 = graph.AddLGSPNode(processType);
            graph.AddEdge(nextType, p1, p2);
            graph.AddEdge(nextType, p2, p1);

            Debugger debugger = OpenDebugger(graph, procEnv); // Let yComp observe any changes to the graph, and execute the sequence step by step

            PrintAndWait("Initial 2-process ring constructed. Starting now to initialized 7-process ring with resource and requests.", debugger);
            Sequence sequence = procEnv.ParseSequence("newRule[5] && mountRule && requestRule[7]");
            debugger.InitNewRewriteSequence(sequence, true); // Initialize 7-process ring with resource and requests.
            procEnv.ApplyGraphRewriteSequence(sequence);
            sequence.ResetExecutionState();

            PrintAndWait("Done constructing. Following sequence won't be debugged, but direct graph changes will be displayed in still open yComp", debugger);

            procEnv.ApplyGraphRewriteSequence("(takeRule && releaseRule && giveRule)*");

            PrintAndWait("About to add 4 processes in the ring.", debugger);
            actions.GetAction("newRule").ApplyMinMax(procEnv, 4, 4);
        }
    }
}
