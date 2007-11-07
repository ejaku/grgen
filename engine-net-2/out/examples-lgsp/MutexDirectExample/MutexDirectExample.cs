using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.MutexPimped;
using de.unika.ipd.grGen.actions.MutexPimped;
using de.unika.ipd.grGen.libGr;

namespace Mutex
{
    class MutexDirectExample
    {
        static void PrintResults(int duration, LGSPGraph graph)
        {
            Console.WriteLine("Mutex benchmark: " + duration + " ms");
            Console.WriteLine("Number of nodes: " + graph.NumNodes);
            Console.WriteLine("Number of edges: " + graph.NumEdges);
            Console.WriteLine("Number of Process nodes: " + graph.GetNumExactNodes(NodeType_Process.typeVar));
            Console.WriteLine("Number of Resource nodes: " + graph.GetNumExactNodes(NodeType_Resource.typeVar));
            Console.WriteLine("Number of next edges: " + graph.GetNumExactEdges(EdgeType_next.typeVar));
            Console.WriteLine("Number of request edges: " + graph.GetNumExactEdges(EdgeType_request.typeVar));
            Console.WriteLine("Number of token edges: " + graph.GetNumExactEdges(EdgeType_token.typeVar));
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

            LGSPGraph graph = new LGSPGraph(new MutexPimpedGraphModel());
            MutexPimpedActions actions = new MutexPimpedActions(graph);

            LGSPNode p1 = graph.AddNode(NodeType_Process.typeVar);
            LGSPNode p2 = graph.AddNode(NodeType_Process.typeVar);
            LGSPEdge n1 = graph.AddEdge(EdgeType_next.typeVar, p1, p2);
            LGSPEdge n2 = graph.AddEdge(EdgeType_next.typeVar, p2, p1);

            LGSPMatches matches;
            LGSPAction newRule = Action_newRule.Instance;
            for(int i = 0; i < n - 2; i++)
            {
                matches = newRule.Match(graph, 1, null);
                newRule.Modify(graph, matches.matches.First);
            }

            matches = Action_mountRule.Instance.Match(graph, 1, null);
            Action_mountRule.Instance.Modify(graph, matches.matches.First);

            LGSPAction requestRule = Action_requestRule.Instance;
            for(int i = 0; i < n; i++)
            {
                matches = requestRule.Match(graph, 1, null);
                requestRule.Modify(graph, matches.matches.First);
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

            LGSPAction takeRule = Action_takeRule.Instance;
            LGSPAction releaseRule = Action_releaseRule.Instance;
            LGSPAction giveRule = Action_giveRule.Instance;
            for(int i = 0; i < n; i++)
            {
                matches = takeRule.Match(graph, 1, null);
                takeRule.Modify(graph, matches.matches.First);
                matches = releaseRule.Match(graph, 1, null);
                releaseRule.Modify(graph, matches.matches.First);
                matches = giveRule.Match(graph, 1, null);
                giveRule.Modify(graph, matches.matches.First);
            }

            int endTime = Environment.TickCount;

            PrintResults(endTime - startTime, graph);
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

            LGSPGraph graph = new LGSPGraph(new MutexPimpedGraphModel());
            MutexPimpedActions actions = new MutexPimpedActions(graph);

            LGSPNode p1 = graph.AddNode(NodeType_Process.typeVar);
            LGSPNode p2 = graph.AddNode(NodeType_Process.typeVar);
            LGSPEdge n1 = graph.AddEdge(EdgeType_next.typeVar, p1, p2);
            LGSPEdge n2 = graph.AddEdge(EdgeType_next.typeVar, p2, p1);

            Action_newRule.Instance.ApplyMinMax(graph, n - 2, n - 2);
            Action_mountRule.Instance.Apply(graph);
            Action_requestRule.Instance.ApplyMinMax(graph, n, n);

            LGSPAction takeRule = Action_takeRule.Instance;
            LGSPAction releaseRule = Action_releaseRule.Instance;
            LGSPAction giveRule = Action_giveRule.Instance;
            for(int i = 0; i < n; i++)
            {
                takeRule.Apply(graph);
                releaseRule.Apply(graph);
                giveRule.Apply(graph);
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

            LGSPGraph graph = new LGSPGraph(new MutexPimpedGraphModel());
            MutexPimpedActions actions = new MutexPimpedActions(graph);

            LGSPNode p1 = graph.AddNode(NodeType_Process.typeVar);
            LGSPNode p2 = graph.AddNode(NodeType_Process.typeVar);
            LGSPEdge n1 = graph.AddEdge(EdgeType_next.typeVar, p1, p2);
            LGSPEdge n2 = graph.AddEdge(EdgeType_next.typeVar, p2, p1);

            actions.ApplyGraphRewriteSequence("newRule[" + (n - 2) + "] && mountRule && requestRule[" + n
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
        }
    }
}
