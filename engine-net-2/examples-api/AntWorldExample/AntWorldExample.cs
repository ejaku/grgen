/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_AntWorld_NoGammel;
using de.unika.ipd.grGen.Action_AntWorld_ExtendAtEndOfRound_NoGammel;
using de.unika.ipd.grGen.libGr;

namespace AntWorldExample
{
    class AntWorldExample
    {
        static void PrintResults(int duration, LGSPGraph graph)
        {
            Console.WriteLine("AntWorld benchmark: " + duration + " ms");
            Console.WriteLine("Number of nodes: " + graph.NumNodes);
            Console.WriteLine("Number of edges: " + graph.NumEdges);
            Console.WriteLine("Number of Process nodes: " + graph.GetNumExactNodes(Ant.TypeInstance));
            Console.WriteLine("Number of Resource nodes: " + graph.GetNumExactNodes(GridNode.TypeInstance));
            Console.WriteLine("Number of Process nodes: " + graph.GetNumExactNodes(GridCornerNode.TypeInstance));
            Console.WriteLine("Number of Resource nodes: " + graph.GetNumExactNodes(AntHill.TypeInstance));
            Console.WriteLine("Number of next edges: " + graph.GetNumExactEdges(NextAnt.TypeInstance));
            Console.WriteLine("Number of request edges: " + graph.GetNumExactEdges(GridEdge.TypeInstance));
            Console.WriteLine("Number of token edges: " + graph.GetNumExactEdges(PathToHill.TypeInstance));
            Console.WriteLine("Number of token edges: " + graph.GetNumExactEdges(AntPosition.TypeInstance));
        }

        /// <summary>
        /// Executes the antWorld test/benchmark
        /// </summary>
        /// <param name="n">The number of doAntWorld rounds to execute</param>
        static void AlternativeOne(int n)
        {
            int startTime = Environment.TickCount;

            AntWorld_NoGammel graph = new AntWorld_NoGammel();
            AntWorld_ExtendAtEndOfRound_NoGammelActions actions = new AntWorld_ExtendAtEndOfRound_NoGammelActions(graph);

            Action_InitWorld initWorld = Action_InitWorld.Instance;
            IAnt firstAnt = null;
            initWorld.Apply(graph, ref firstAnt);

            int endTime = Environment.TickCount;
            PrintResults(endTime - startTime, graph);

            for (int i = 0; i < n; ++i)
            {
                Action_doAntWorld doAntWorld = Action_doAntWorld.Instance;
                doAntWorld.Apply(graph, firstAnt);

                endTime = Environment.TickCount;
                PrintResults(endTime - startTime, graph);
            }
        }

        /// <summary>
        /// Executes the antWorld test/benchmark
        /// </summary>
        /// <param name="n">The number of doAntWorld rounds to execute</param>
        static void AlternativeTwo(int n)
        {
            int startTime = Environment.TickCount;

            AntWorld_NoGammel graph = new AntWorld_NoGammel();
            AntWorld_ExtendAtEndOfRound_NoGammelActions actions = new AntWorld_ExtendAtEndOfRound_NoGammelActions(graph);

            Action_InitWorld initWorld = Action_InitWorld.Instance;
            IAnt firstAnt = null;
            initWorld.Apply(graph, ref firstAnt);

            // TODO

            int endTime = Environment.TickCount;

            PrintResults(endTime - startTime, graph);
        }


        static void PrintUsage()
        {
            Console.WriteLine("Usage: MutexDirectExample <n> [<alt>]\nwhere <n> is the number of doAntWorld runs"
                + "\nand <alt> a number between 1 and 2");
        }

        static void Main(string[] args)
        {
            if(args.Length < 1 || args.Length > 2)
            {
                PrintUsage();
                return;
            }

            int n;
            if(!Int32.TryParse(args[0], out n))
            {
                Console.WriteLine("Illegal value for n: " + args[0]);
                PrintUsage();
                return;
            }

            int alt = 1;
            if(args.Length == 2)
            {
                if(!Int32.TryParse(args[1], out alt) || alt < 1 || alt > 2)
                {
                    Console.WriteLine("Illegal value for alt: " + args[0]);
                    PrintUsage();
                    return;
                }
            }

            switch(alt)
            {
                case 1:
                    AlternativeOne(n);
                    break;
                case 2:
                    AlternativeTwo(n);
                    break;
            }
        }
    }
}
