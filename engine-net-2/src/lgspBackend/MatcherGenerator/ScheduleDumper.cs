/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;

using System.IO;
using de.unika.ipd.grGen.libGr;


namespace de.unika.ipd.grGen.lgsp
{
    public static class ScheduleDumper
    {
        private static String GetDumpName(SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root)
                return "root";
            else if(node.NodeType == PlanNodeType.Node)
                return "node_" + node.PatternElement.Name;
            else
                return "edge_" + node.PatternElement.Name;
        }

        private static void DumpNode(StreamWriter sw, SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
            {
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            }
            else
            {
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            }
        }

        private static void DumpEdge(StreamWriter sw, SearchOperationType opType, SearchPlanNode source, SearchPlanNode target, float cost, bool markRed)
        {
            String typeStr = " #";
            switch(opType)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.Incident: typeStr = "<->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Implicit: typeStr = "IM"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.ActionPreset: typeStr = " p"; break;
                case SearchOperationType.NegIdptPreset: typeStr = "np"; break;
                case SearchOperationType.SubPreset: typeStr = "sp"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00}\"{4}}}",
                GetDumpName(source), GetDumpName(target), typeStr, cost, markRed ? " color:red" : "");
        }

        private static void DumpScheduledSearchPlan(ScheduledSearchPlan ssp, IGraphModel model, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.txt", false);
            SourceBuilder sb = new SourceBuilder();
            ScheduleExplainer.Explain(ssp, sb, model);
            sb.Append("\n");
            sw.WriteLine(sb.ToString());
            sw.Close();
        }

        public static void DumpScheduledSearchPlanAsVcg(ScheduledSearchPlan ssp, IGraphModel model, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes: yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");
            SearchPlanNode root = new SearchPlanNode("root");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(SearchOperation op in ssp.Operations)
            {
                switch(op.Type)
                {
                case SearchOperationType.Lookup:
                case SearchOperationType.Incoming:
                case SearchOperationType.Outgoing:
                case SearchOperationType.ImplicitSource:
                case SearchOperationType.ImplicitTarget:
                    {
                        SearchPlanNode spnode = (SearchPlanNode)op.Element;
                        DumpNode(sw, spnode);
                        SearchPlanNode src;
                        switch(op.Type)
                        {
                        case SearchOperationType.Lookup:
                        case SearchOperationType.ActionPreset:
                        case SearchOperationType.NegIdptPreset:
                            src = root;
                            break;
                        default:
                            src = op.SourceSPNode;
                            break;
                        }
                        DumpEdge(sw, op.Type, src, spnode, op.CostToEnd, false);
                        break;
                    }
                case SearchOperationType.Condition:
                    sw.WriteLine("node:{title:\"Condition\" label:\"CONDITION\"}\n");
                    break;
                case SearchOperationType.NegativePattern:
                    sw.WriteLine("node:{title:\"NAC\" label:\"NAC\"}\n");
                    break;
                }
            }
        }
    }
}
