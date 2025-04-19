/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
            ConsoleUI.outWriter.WriteLine(text);
            ConsoleUI.outWriter.WriteLine("Press any key to continue...");
            ConsoleUI.consoleIn.ReadKey(true);
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
                ConsoleUI.errorOutWriter.WriteLine("Unable to create graph from specification: " + ex.Message);
                return;
            }

            GraphViewer graphViewer = new GraphViewer();
            graphViewer.ShowGraph(graph, GraphViewerTypes.YComp, "Organic", null); // Let yComp observe any changes to the graph
            // you could use MSAGL as graph viewer too, with e.g. layout MDS corresponding roughly to Organic, or SugiyamaScheme corresponding roughly to Hierarchic
            // but note that Application.DoEvents(); must be called in this case in order to get a responsive GUI (this is a console app project) (take a look at GraphViewer.ShowGraphWithMSAGL)

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
            ConsoleUI.outWriter.WriteLine("Do many changes slowing down too much with YComp (not in this example)...");
            procEnv.ApplyGraphRewriteSequence("(takeRule && releaseRule && giveRule)*");
            PrintAndWait("Nothing changed so far on the display.", null);

            graphViewer.ShowGraph(graph, GraphViewerTypes.YComp, "Organic", null);
            PrintAndWait("Graph newly uploaded to yComp.", graphViewer);

            graphViewer.UpdateDisplay = false;

            actions.GetAction("newRule").ApplyMinMax(procEnv, 4, 4);
            PrintAndWait("Added 4 processes in the ring.", graphViewer);

            graphViewer.EndShowGraph();

            GraphViewer.DumpAndShowGraph(graph, "Organic"); // just render a graph in its current state
        }
    }
}
