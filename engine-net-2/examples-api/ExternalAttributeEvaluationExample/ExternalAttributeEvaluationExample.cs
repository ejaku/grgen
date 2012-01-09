/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_ExternalAttributeEvaluation;
using de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

namespace EAE
{
    class EAEExample
    {
        ExternalAttributeEvaluationGraph graph;
        ExternalAttributeEvaluationActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        // Have a look at the .gm + .grg, the ExternalAttributeEvaluationModelExternalFunctions.cs,
        // and the ExternalAttributeEvaluationModelExternalFunctionsImpl.cs files.
        // They show how to declare external classes and actions in the model file, 
        // and how to use them in the attribute calculations in the rule file.
        // The generated XXXExternalFunctions.cs file contains the partial classes of the data types and functions
        // and the manually coded XXXExternalFunctionsImpl.cs file exemplifies how to implement these external functions.
        void DoEAE()
        {
            graph = new ExternalAttributeEvaluationGraph();
            actions = new ExternalAttributeEvaluationActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            procEnv.PerformanceInfo = new PerformanceInfo();

            de.unika.ipd.grGen.expression.ExternalFunctions.setGraph(graph);

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("init");

            Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use new 2.5 exact interface
            IMatchesExact<Rule_r.IMatch_r> matchesExact = actions.r.Match(procEnv, 0);
            Console.WriteLine(matchesExact.Count + " matches found.");
            actions.r.Modify(procEnv, matchesExact.FirstExact);
        }

        static void Main(string[] args)
        {
            EAEExample eae = new EAEExample();
            eae.DoEAE();
        }
    }
}
