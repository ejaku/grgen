/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_JavaProgramGraphs;
using de.unika.ipd.grGen.Model_JavaProgramGraphs;

namespace JavaProgramGraphs
{
    class JavaProgramGraphsExample
    {
        void DoIt()
        {
            // create the LibGr Search Plan backend we want to use
            LGSPBackend backend = new LGSPBackend();

            // the graph model we'll use
            JavaProgramGraphsGraphModel model = new JavaProgramGraphsGraphModel();
            
            // import the instance graph we created with gxl2grs, using the the .gm we created by hand
            // (can't use import gxl for the program graph, because the given .gxl is severly rotten)
            // we throw away the named graph cause we don't need names here and they require about the same amount of memory as the graph itself; 
            // otherwise we would set the NamedGraph property of the lgsp graph to the named graph to get the nameof operator working
            NamedGraph importedNamedGraph = (NamedGraph)Porter.Import("InstanceGraph.grs", backend, model);
            LGSPGraph graph = (LGSPGraph)(importedNamedGraph.WrappedGraph);
            importedNamedGraph = null;
            
            // get the actions object for the rules we want to use
            JavaProgramGraphsActions actions = new JavaProgramGraphsActions(graph);

            // the instance graph script uses variables to build up the graph,
            // we query some of them here, to get the elements to refactor
            // (instead of variables one could use the element names)
            Class src = (Class)graph.GetNodeVarValue("I176"); // class Node
            Class tgt = (Class)graph.GetNodeVarValue("I194"); // class Packet - use if parameter is used in moving method
            //Class tgt = (Class)graph.GetNodeVarValue("I617"); // class String - use if instance variable is used in moving method
            MethodBody mb = (MethodBody)graph.GetNodeVarValue("I409"); // method body of send

            // get operation for method body by: 
            // get action, match action pattern with given parameters, apply rewrite filling given out parameters
            IMatchesExact<Rule_getOperation.IMatch_getOperation> matches = actions.getOperation.Match(graph, 1, mb);
            IOperation op;
            actions.getOperation.Modify(graph, matches.FirstExact, out op);

            // iterated application of action marking the body of the expression
            // (shows second way of getting action)
            int visitedFlagId = graph.AllocateVisitedFlag();
            Debug.Assert(visitedFlagId==0);
            IGraphElement[] param = new LGSPNode[1];
            param[0] = mb;
            IMatches matchesInexact;
            while((matchesInexact = actions.GetAction("markExpressionOfBody").Match(graph, 1, param)).Count==1)
            {
                actions.GetAction("markExpressionOfBody").Modify(graph, matchesInexact.First);
            }

            graph.PerformanceInfo = new PerformanceInfo();

            // application of a graph rewrite sequence
            graph.SetVariableValue("src", src);
            graph.SetVariableValue("tgt", tgt);
            graph.SetVariableValue("mb", mb);
            graph.SetVariableValue("op", op);
            actions.ApplyGraphRewriteSequence(
                @"(p)=someParameterOfTargetType(mb,tgt) 
                    && !callToSuperExists && !isStatic(mb) && !methodNameExists(mb,tgt) 
                    && (!thisIsAccessed || thisIsAccessed && (srcparam)=addSourceParameter(op,src) && useSourceParameter(srcparam)*) 
                    && relinkOperationAndMethodBody(op,mb,src,tgt) 
                    && ( (call,pe)=getUnprocessedCallWithActualParameter(op,p) 
                         && ((def(srcparam) && addSourceToCall(call,srcparam)) || true) 
                         && (replaceAccess_Parameter_AccessWithoutLink(c,pe) || replaceAccess_Parameter_AccessWithLinkToExpression(c,pe)) 
                       )*");

            Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
            graph.PerformanceInfo.Reset();

            // unmark the body of the expression by searching all occurences and modifying them
            actions.unmarkExpression.ApplyAll(0, graph);
            graph.FreeVisitedFlag(visitedFlagId);

            // export changed graph (alternatively you may export it as InstanceGraphAfter.gxl)
            // if we'd use a NamedGraph we'd get the graph exported with its persistent names; so we get it exported with some hash names
            List<String> exportParameters = new List<string>();
            exportParameters.Add("InstanceGraphAfter.grs");
            Porter.Export(graph, exportParameters);
        }

        static void Main(string[] args)
        {
            JavaProgramGraphsExample jpge = new JavaProgramGraphsExample();
            jpge.DoIt();
        }
    }
}
