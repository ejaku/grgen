/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_ExternalFiltersAndSequences;
using de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;

namespace EFS
{
    class EFSExample
    {
        ExternalFiltersAndSequencesGraph graph;
        ExternalFiltersAndSequencesActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        // Have a look at the .gm + .grg, the ExternalFiltersAndSequencesActionsExternalFunctions.cs,
        // and the ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs files.
        // They show how to declare external match filters and external sequences in the rules file, 
        // and how to use them in the sequences in the rule file or the shell script file.
        // The generated XXXExternalFunctions.cs file contains the partial classes of the filters and sequences
        // and the manually coded XXXExternalFunctionsImpl.cs file exemplifies how to implement these external functions.
        void DoEFS()
        {
            graph = new ExternalFiltersAndSequencesGraph();
            actions = new ExternalFiltersAndSequencesActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            procEnv.PerformanceInfo = new PerformanceInfo();

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("(::n)=init");

            Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use new 2.5 exact interface
            IMatchesExact<Rule_r.IMatch_r> matchesExact = actions.r.Match(procEnv, 0);
            Console.WriteLine(matchesExact.Count + " matches found.");
            actions.r.Modify(procEnv, matchesExact.FirstExact);

            procEnv.ApplyGraphRewriteSequence("(::x,::y,::z,::u,::v)=foo(42, 3.141, Enu::hurz, \"S21-heiteitei\", true)");
            procEnv.ApplyGraphRewriteSequence("filterBase\\f1");
        }

        static void Main(string[] args)
        {
            EFSExample efs = new EFSExample();
            efs.DoEFS();
        }
    }
}
