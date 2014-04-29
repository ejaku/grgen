/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_MovieDatabase;
using de.unika.ipd.grGen.Model_MovieDatabaseModel;

namespace MovieDatabase
{
    class MovieDatabaseBenchmarker
    {
        static void Main(string[] args)
        {
            if(args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("usage: MovieDatabaseBenchmarker <name of rule to apply> <name of grs file to import or number of creation iterations of synthetic graph> [\"sequence to execute\"]");
                Console.WriteLine("example: MovieDatabaseBenchmarker findCouplesOpt imdb-0005000-50176.movies.xmi.grs");
                Console.WriteLine("example: MovieDatabaseBenchmarker findCliquesOf3Opt imdb-0130000-712130.movies.xmi.grs \"[cliques3WithRating\\orderDescendingBy<avgRating>\\keepFirst(15)] ;> [cliques3WithRating\\orderDescendingBy<numMovies>\\keepFirst(15)]\"");
                return;
            }

            // the graph we'll work on
            LGSPGraph graph;

            // the actions we'll use
            MovieDatabaseActions actions;

            // the graph processing environment we'll use
            LGSPGraphProcessingEnvironment procEnv;

            int dummy;
            if(Int32.TryParse(args[1], out dummy))
            {
                Console.WriteLine("Synthesizing test graph with iteration count " + args[1] + " ...");

                graph = new MovieDatabaseModelGraph();
                actions = new MovieDatabaseActions(graph);
                procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

                procEnv.ApplyGraphRewriteSequence("createExample(" + args[1] + ")");
            }
            else
            {
                Console.WriteLine("Importing " + args[1] + " ...");

                // the libGr search plan backend we'll use
                LGSPBackend backend = new LGSPBackend();

                // the graph model we'll use
                MovieDatabaseModelGraphModel model = new MovieDatabaseModelGraphModel();

                // import the graph, result (of grs import) will be a named graph
                IActions ba;
                INamedGraph importedNamedGraph = (INamedGraph)Porter.Import(args[1], backend, model, out ba);

                // we throw away the named graph cause we don't need names here and they require a huge amount of memory
                graph = new LGSPGraph((LGSPNamedGraph)importedNamedGraph, "unnamed");
                importedNamedGraph = null;
                GC.Collect();

                actions = ba != null ? (MovieDatabaseActions)ba : new MovieDatabaseActions(graph);
                procEnv = new LGSPGraphProcessingEnvironment(graph, actions);
            }

            // calculate search plans to optimize performance (I'm not going to fiddle with loading saved analysis data here)
            graph.AnalyzeGraph();
            actions.GenerateActions(args[0]);

            Console.WriteLine("Number of Movie: " + graph.nodesByTypeCounts[graph.Model.NodeModel.GetType("Movie").TypeID]);
            Console.WriteLine("Number of Actor: " + graph.nodesByTypeCounts[graph.Model.NodeModel.GetType("Actor").TypeID]);
            Console.WriteLine("Number of Actress: " + graph.nodesByTypeCounts[graph.Model.NodeModel.GetType("Actress").TypeID]);
            Console.WriteLine("Number of personToMovie: " + graph.edgesByTypeCounts[graph.Model.EdgeModel.GetType("personToMovie").TypeID]);

            Console.WriteLine("Start matching " + args[0] + " ...");

            int startTime = Environment.TickCount;

            // get action, search for all matches, apply rewrite
            IAction ruleToApply = actions.GetAction(args[0]);
            IMatches matches = ruleToApply.Match(procEnv, 0, new object[0]);

            Console.WriteLine("...needed " + (Environment.TickCount - startTime) + "ms for finding the matches");

            Console.WriteLine("...continue with rewriting...");

            ruleToApply.ModifyAll(procEnv, matches);

            Console.WriteLine("...needed " + (Environment.TickCount - startTime) + "ms for finding the matches and adding the couples/cliques");

            Console.WriteLine("Number of Couple: " + graph.nodesByTypeCounts[graph.Model.NodeModel.GetType("Couple").TypeID]);
            Console.WriteLine("Number of Clique: " + graph.nodesByTypeCounts[graph.Model.NodeModel.GetType("Clique").TypeID]);
            Console.WriteLine("Number of commonMovies: " + graph.edgesByTypeCounts[graph.Model.EdgeModel.GetType("commonMovies").TypeID]);

            if(args.Length == 3)
            {
                procEnv.ApplyGraphRewriteSequence(args[2]);
            }
        }
    }
}
