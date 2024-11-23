/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        private static ConsoleDebugger OpenDebugger(INamedGraph graph, IGraphProcessingEnvironment procEnv, GraphViewerTypes graphViewerType)
        {
            Dictionary<String, String> optMap = new Dictionary<String, String>();
            DebuggerGraphProcessingEnvironment debuggerProcEnv = new DebuggerGraphProcessingEnvironment(graph, procEnv);

            DebuggerEnvironment debuggerEnv = null;
            ConsoleDebugger debugger = null;
            if(graphViewerType == GraphViewerTypes.YComp)
            {
                debuggerEnv = new DebuggerEnvironment(DebuggerConsoleUI.Instance, DebuggerConsoleUI.Instance);
                debugger = new ConsoleDebugger(debuggerEnv, debuggerProcEnv, new ElementRealizers(),
                    graphViewerType, "Organic"/*"Hierarchic"*/, optMap, null);
            }
            else
            {
                bool twoPane = true; // true: use two panes (consoles), false: one console
                IHostCreator guiConsoleDebuggerHostCreator = GraphViewerClient.GetGuiConsoleDebuggerHostCreator();
                IGuiConsoleDebuggerHost host = guiConsoleDebuggerHostCreator.CreateGuiConsoleDebuggerHost(twoPane);
                debuggerEnv = new DebuggerEnvironment(host.GuiConsoleControl, twoPane ? host.OptionalGuiConsoleControl : host.GuiConsoleControl);
                debugger = new ConsoleDebugger(debuggerEnv, debuggerProcEnv, new ElementRealizers(),
                    graphViewerType, "MDS"/*"SugiyamaScheme"*/, optMap, host);
            }

            debugger.DetailedModeShowPreMatches = true;
            debugger.DetailedModeShowPostMatches = true;
            debuggerEnv.Debugger = debugger;
            return debugger;
        }

        /// <summary>
        /// Ensures that the graph viewer display is up to date, prints out a message, and waits for a key press.
        /// </summary>
        /// <param name="text">The message to be printed.</param>
        private static void PrintAndWait(String text, ConsoleDebugger debugger)
        {
            if(debugger != null && debugger.GraphViewerClient != null)
                debugger.GraphViewerClient.UpdateDisplay();
            if(debugger != null && debugger.GraphViewerClient != null)
                debugger.GraphViewerClient.Sync();
            debugger.env.WriteLine(text);
            debugger.env.PauseUntilAnyKeyPressed("Press any key to continue...");
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
                ConsoleUI.errorOutWriter.WriteLine("Unable to create graph from specification: " + ex.Message);
                return;
            }

            NodeType processType = graph.GetNodeType("Process");
            EdgeType nextType = graph.GetEdgeType("next");

            LGSPNode p1 = graph.AddLGSPNode(processType);
            LGSPNode p2 = graph.AddLGSPNode(processType);
            graph.AddEdge(nextType, p1, p2);
            graph.AddEdge(nextType, p2, p1);

            // in case of GraphViewerTypes.YComp: uses normal stdout-console of this Console Application in order to print sequence execution, allowing to follow execution step by step,
            // showing the graph in the external yComp application (changes to the graph are observed, rule applications are highlighted in case of detail mode)
            // in case of GraphViewerTypes.MSAGL: opens a WindowsForms form with a console-like control in order to print sequence execution, allowing to follow execution step by step, (you could use it directly in a non-console project)
            // showing the graph with the WindowsForms graph viewer control of the MSAGL library (changes to the graph are observed, rule applications are highlighted in case of detail mode)
            ConsoleDebugger debugger = OpenDebugger(graph, procEnv, /*GraphViewerTypes.YComp*/GraphViewerTypes.MSAGL);

            PrintAndWait("Initial 2-process ring constructed. Starting now to initialized 7-process ring with resource and requests.", debugger);
            Sequence sequence = procEnv.ParseSequence("newRule[5] && mountRule && requestRule[7]");
            debugger.InitNewRewriteSequence(sequence, true); // Initialize 7-process ring with resource and requests.
            procEnv.ApplyGraphRewriteSequence(sequence);
            sequence.ResetExecutionState();

            PrintAndWait("Done constructing. Following sequence won't be debugged, but direct graph changes will be displayed in the still open graph viewer/debugger", debugger);
            debugger.AbortRewriteSequence(); // ensure step mode is off

            procEnv.ApplyGraphRewriteSequence("(takeRule && releaseRule && giveRule)*");

            PrintAndWait("About to add 4 processes in the ring.", debugger);
            actions.GetAction("newRule").ApplyMinMax(procEnv, 4, 4);
        }
    }
}
