/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Diagnostics;
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
            LGSPGraph graph = (LGSPGraph)Porter.Import("InstanceGraph.grs", backend, model);
            
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
            // get action, fill parameters, match action pattern, apply rewrite, get action returns
            LGSPAction getOperation = Action_getOperation.Instance;
            IGraphElement[] param = new LGSPNode[1];
            param[0] = mb;
            LGSPMatches matches = getOperation.Match(graph, 1, param);
            object[] returns;
            returns = getOperation.Modify(graph, (LGSPMatch)matches.GetMatch(0));
            Operation op = (Operation)returns[0];

            // iterated application of action marking the body of the expression
            // (shows second way of getting action)
            int visitedFlagId = graph.AllocateVisitedFlag();
            Debug.Assert(visitedFlagId==0);
            while((matches = actions.GetAction("markExpressionOfBody").Match(graph, 1, param)).Count==1)
            {
                actions.GetAction("markExpressionOfBody").Modify(graph, matches.matchesList.First);
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
            matches = Action_unmarkExpression.Instance.Match(graph, 0, null);
            Action_unmarkExpression.Instance.ModifyAll(graph, matches);
            graph.FreeVisitedFlag(visitedFlagId);

            // export changed graph (alternatively you may export it as InstanceGraphAfter.gxl)
            Porter.Export(graph, "InstanceGraphAfter.grs");
        }

        static void Main(string[] args)
        {
            JavaProgramGraphsExample jpge = new JavaProgramGraphsExample();
            jpge.DoIt();
        }
    }
}
