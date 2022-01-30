/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace YCompExample
{
    class YCompExample
    {
        /// <summary>
        /// Ensures that the yComp display is up to date, prints out a message, and waits for a key press.
        /// </summary>
        /// <param name="text">The message to be printed.</param>
        private static void PrintAndWait(String text, GraphViewer graphViewer)
        {
            if(graphViewer != null)
                graphViewer.UpdateDisplayAndSync();
            Console.WriteLine(text);
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey(true);
        }

        // example showing how to render a graph at API level, tracking changes
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

            GraphViewer graphViewer = new GraphViewer();
            graphViewer.ShowGraph(graph, "Organic"); // Let yComp observe any changes to the graph

            NodeType processType = graph.GetNodeType("Process");
            EdgeType nextType = graph.GetEdgeType("next");

            LGSPNode p1 = graph.AddLGSPNode(processType);
            LGSPNode p2 = graph.AddLGSPNode(processType);
            graph.AddEdge(nextType, p1, p2);
            graph.AddEdge(nextType, p2, p1);
            PrintAndWait("Initial 2-process ring constructed.", graphViewer);

            graphViewer.UpdateDisplay = true;

            procEnv.ApplyGraphRewriteSequence("newRule[5] && mountRule && requestRule[7]");
            PrintAndWait("Initialized 7-process ring with resource and requests.", graphViewer);

            graphViewer.EndShowGraph();
            Console.WriteLine("Do many changes slowing down too much with YComp (not in this example)...");
            procEnv.ApplyGraphRewriteSequence("(takeRule && releaseRule && giveRule)*");
            PrintAndWait("Nothing changed so far on the display.", null);

            graphViewer.ShowGraph(graph, "Organic");
            PrintAndWait("Graph newly uploaded to yComp.", graphViewer);

            graphViewer.UpdateDisplay = false;

            actions.GetAction("newRule").ApplyMinMax(procEnv, 4, 4);
            PrintAndWait("Added 4 processes in the ring.", graphViewer);

            graphViewer.EndShowGraph();

            GraphViewer.DumpAndShowGraph(graph, "Organic"); // just render a graph in its current state
        }
    }
}
