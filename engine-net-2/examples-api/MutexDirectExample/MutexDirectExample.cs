/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_Mutex;
using de.unika.ipd.grGen.Action_MutexPimped;
using de.unika.ipd.grGen.libGr;

namespace MutexExample
{
    class MutexDirectExample
    {
        static void PrintResults(int duration, LGSPGraph graph)
        {
            Console.WriteLine("Mutex benchmark: " + duration + " ms");
            Console.WriteLine("Number of nodes: " + graph.NumNodes);
            Console.WriteLine("Number of edges: " + graph.NumEdges);
            Console.WriteLine("Number of Process nodes: " + graph.GetNumExactNodes(Process.TypeInstance));
            Console.WriteLine("Number of Resource nodes: " + graph.GetNumExactNodes(Resource.TypeInstance));
            Console.WriteLine("Number of next edges: " + graph.GetNumExactEdges(next.TypeInstance));
            Console.WriteLine("Number of request edges: " + graph.GetNumExactEdges(request.TypeInstance));
            Console.WriteLine("Number of token edges: " + graph.GetNumExactEdges(token.TypeInstance));
        }

        /// <summary>
        /// Executes the Mutex benchmark with a problem size of n.
        /// This variant uses IAction.Match and IAction.Modify to apply the rules.
        /// This way you get access to the found matches and can select between them manually.
        /// </summary>
        /// <param name="n">The problem size, i.e. the number of Process nodes</param>
        static void AlternativeOne(int n)
        {
            int startTime = Environment.TickCount;

            MutexGraph graph = new MutexGraph();
            MutexPimpedActions actions = new MutexPimpedActions(graph);
            LGSPGraphProcessingEnvironment procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            LGSPNode p1 = graph.CreateNodeProcess();
            LGSPNode p2 = graph.CreateNodeProcess();
            LGSPEdge n1 = graph.CreateEdgenext(p1, p2);
            LGSPEdge n2 = graph.CreateEdgenext(p2, p1);

            IMatches matches;
            Action_newRule newRule = Action_newRule.Instance;
            for(int i = 0; i < n - 2; i++)
            {
                matches = newRule.Match(procEnv, 1);
                newRule.Modify(procEnv, matches.First);
            }

            matches = Action_mountRule.Instance.Match(procEnv, 1);
            Action_mountRule.Instance.Modify(procEnv, matches.First);

            Action_requestRule requestRule = Action_requestRule.Instance;
            for(int i = 0; i < n; i++)
            {
                matches = requestRule.Match(procEnv, 1);
                requestRule.Modify(procEnv, matches.First);
            }

            /**
             * The MutexPimped.grg file has been annotated with [prio] to generate very good static
             * searchplans, without the need of dynamically generating searchplans.
             * 
             * Otherwise you would use the following code to get better matcher programs:
             * 
             * graph.AnalyzeGraph();
             * LGSPAction[] newActions = actions.GenerateSearchPlans("takeRule", "releaseRule", "giveRule");
             * LGSPAction takeRule = newActions[0];
             * LGSPAction releaseRule = newActions[1];
             * LGSPAction giveRule = newActions[2];
             * 
             * Alternatively you could also write:
             * 
             * graph.AnalyzeGraph();
             * actions.GenerateSearchPlans("takeRule", "releaseRule", "giveRule");
             * LGSPAction takeRule = actions.GetAction("takeRule");
             * LGSPAction releaseRule = actions.GetAction("releaseRule");
             * LGSPAction giveRule = actions.GetAction("giveRule");
             */

            Action_takeRule takeRule = Action_takeRule.Instance;
            Action_releaseRule releaseRule = Action_releaseRule.Instance;
            Action_giveRule giveRule = Action_giveRule.Instance;
            for(int i = 0; i < n; i++)
            {
                matches = takeRule.Match(procEnv, 1);
                takeRule.Modify(procEnv, matches.First);
                matches = releaseRule.Match(procEnv, 1);
                releaseRule.Modify(procEnv, matches.First);
                matches = giveRule.Match(procEnv, 1);
                giveRule.Modify(procEnv, matches.First);
            }

            int endTime = Environment.TickCount;

            PrintResults(endTime - startTime, graph);

            // example for reading annotations at API level
            Action_annotationTestRule annotationTestRule = Action_annotationTestRule.Instance;
            foreach(KeyValuePair<string, string> annotation in annotationTestRule.rulePattern.Annotations)
            {
                Console.WriteLine("The rule " + annotationTestRule.Name + " is decorated with an annotation " + annotation.Key + " -> " + annotation.Value);
            }
            foreach(IPatternNode node in annotationTestRule.RulePattern.PatternGraph.Nodes)
            {
                foreach(KeyValuePair<string, string> annotation in node.Annotations)
                {
                    Console.WriteLine("The pattern node " + node.Name + " of rule " + annotationTestRule.Name + " is decorated with an annotation " + annotation.Key + " -> " + annotation.Value);
                }
            }
            IAnnotationTestNode testNode = graph.CreateNodeAnnotationTestNode();
            foreach(KeyValuePair<string, string> annotation in testNode.Type.Annotations)
            {
                Console.WriteLine("The node type " + testNode.Type.Name + " is decorated with an annotation " + annotation.Key + " -> " + annotation.Value);
            }
        }

        /// <summary>
        /// Executes the Mutex benchmark with a problem size of n.
        /// This variant uses IAction.Apply and IAction.MinMax to apply the rules
        /// (also available IAction.ApplyStar and IAction.ApplyPlus).
        /// These functions make it very easy to program complex rule applications,
        /// while providing the same speed as using IAction.Match and IAction.Modify
        /// (this example is not a complex scenario).
        /// </summary>
        /// <param name="n">The problem size, i.e. the number of Process nodes</param>
        static void AlternativeTwo(int n)
        {
            int startTime = Environment.TickCount;

			MutexGraph graph = new MutexGraph();
			MutexPimpedActions actions = new MutexPimpedActions(graph);
            LGSPGraphProcessingEnvironment procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

			LGSPNode p1 = graph.CreateNodeProcess();
			LGSPNode p2 = graph.CreateNodeProcess();
			LGSPEdge n1 = graph.CreateEdgenext(p1, p2);
			LGSPEdge n2 = graph.CreateEdgenext(p2, p1);

            Action_newRule.Instance.ApplyMinMax(procEnv, n - 2, n - 2);
            Action_mountRule.Instance.Apply(procEnv);
            Action_requestRule.Instance.ApplyMinMax(procEnv, n, n);

            Action_takeRule takeRule = Action_takeRule.Instance;
            Action_releaseRule releaseRule = Action_releaseRule.Instance;
            Action_giveRule giveRule = Action_giveRule.Instance;
            for(int i = 0; i < n; i++)
            {
                takeRule.Apply(procEnv);
                releaseRule.Apply(procEnv);
                giveRule.Apply(procEnv);
            }

            int endTime = Environment.TickCount;

            PrintResults(endTime - startTime, graph);
        }

        /// <summary>
        /// Executes the Mutex benchmark with a problem size of n.
        /// This variant uses BaseActions.ApplyGraphRewriteSequence.
        /// Although this provides the fastest way to program rule applications
        /// for simple scenarios, it needs aprox. 12% more running time than
        /// via Apply or Match/Modify.
        /// </summary>
        /// <param name="n">The problem size, i.e. the number of Process nodes</param>
        static void AlternativeThree(int n)
        {
            int startTime = Environment.TickCount;

			MutexGraph graph = new MutexGraph();
			MutexPimpedActions actions = new MutexPimpedActions(graph);
            LGSPGraphProcessingEnvironment procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

			LGSPNode p1 = graph.CreateNodeProcess();
			LGSPNode p2 = graph.CreateNodeProcess();
			LGSPEdge n1 = graph.CreateEdgenext(p1, p2);
			LGSPEdge n2 = graph.CreateEdgenext(p2, p1);

            procEnv.ApplyGraphRewriteSequence("newRule[" + (n - 2) + "] && mountRule && requestRule[" + n
                + "] && (takeRule && releaseRule && giveRule)[" + n + "]");

            int endTime = Environment.TickCount;

            PrintResults(endTime - startTime, graph);
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: MutexDirectExample <n> [<alt>]\nwhere <n> is the number of process nodes"
                + "\nand <alt> a number between 1 and 3");
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
                if(!Int32.TryParse(args[1], out alt) || alt < 1 || alt > 3)
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
                case 3:
                    AlternativeThree(n);
                    break;
            }

            Console.ReadKey();
        }
    }
}
