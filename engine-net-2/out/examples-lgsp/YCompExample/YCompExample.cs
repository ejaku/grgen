/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.grShell;

namespace YCompExample
{
    class YCompExample
    {
        /// <summary>
        /// Uploads the graph to YComp, updates the display and makes a synchonisation
        /// </summary>
        public static void UploadGraph(IGraph graph, YCompClient ycompClient)
        {
            foreach(INode node in graph.Nodes)
                ycompClient.AddNode(node);
            foreach(IEdge edge in graph.Edges)
                ycompClient.AddEdge(edge);
            ycompClient.UpdateDisplay();
            ycompClient.Sync();
        }

        /// <summary>
        /// Ensures that the yComp display is up to date, prints out a message, and waits for a key press.
        /// </summary>
        /// <param name="text">The message to be printed.</param>
        private static void PrintAndWait(String text, YCompClient ycomp)
        {
            ycomp.UpdateDisplay();
            ycomp.Sync();
            Console.WriteLine(text);
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey(true);
        }

        public static void Main(string[] args)
        {
            LGSPGraph graph;
            LGSPActions actions;
            try
            {
                new LGSPBackend().CreateFromSpec("Mutex.grg", out graph, out actions);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to create graph from specification: " + ex.Message);
                return;
            }

            DumpInfo dumpInfo = new DumpInfo(graph.GetElementName);
            YCompClient ycomp = YCompClient.CreateYCompClient(graph, "Organic", dumpInfo);

            // Let yComp observe any changes to the graph
            ycomp.RegisterLibGrEvents();

            NodeType processType = graph.GetNodeType("Process");
            EdgeType nextType = graph.GetEdgeType("next");

            LGSPNode p1 = graph.AddNode(processType);
            LGSPNode p2 = graph.AddNode(processType);
            graph.AddEdge(nextType, p1, p2);
            graph.AddEdge(nextType, p2, p1);
            PrintAndWait("Initial 2-process ring constructed.", ycomp);

            actions.ApplyGraphRewriteSequence("newRule[5] && mountRule && requestRule[7]");
            PrintAndWait("Initialized 7-process ring with resource and requests.", ycomp);

            ycomp.UnregisterLibGrEvents();
            Console.WriteLine("Do many changes slowing down too much with YComp (not in this example)...");
            actions.ApplyGraphRewriteSequence("(takeRule && releaseRule && giveRule)*");
            PrintAndWait("Nothing changed so far on the display.", ycomp);

            ycomp.ClearGraph();
            UploadGraph(graph, ycomp);
            PrintAndWait("Graph newly uploaded to yComp.", ycomp);

            ycomp.RegisterLibGrEvents();

            actions.GetAction("newRule").ApplyMinMax(graph, 4, 4);
            PrintAndWait("Added 4 processes in the ring.", ycomp);

            ycomp.Close();
        }
    }
}
