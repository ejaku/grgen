/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

using de.unika.ipd.grGen.libGr;


namespace de.unika.ipd.grGen.lgsp
{
    public static class ScheduleExplainer
    {
        public static void Explain(SearchOperation so, SourceBuilder sb, IGraphModel model)
        {
            SearchPlanNode src = so.SourceSPNode as SearchPlanNode;
            SearchPlanNode tgt = so.Element as SearchPlanNode;
            String connectednessCheck = "";
            if(so.ConnectednessCheck.PatternElementName != null)
            {
                connectednessCheck = " check " + so.ConnectednessCheck.PatternNodeName 
                    + (so.ConnectednessCheck.TheOtherPatternNodeName != null ? " or " + so.ConnectednessCheck.TheOtherPatternNodeName : "")
                    + " connected to " + so.ConnectednessCheck.PatternEdgeName;
            }

            switch(so.Type)
            {
            case SearchOperationType.Outgoing:
                sb.AppendFront("from " + src.PatternElement.UnprefixedName + " outgoing -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->" + connectednessCheck + "\n");
                break;
            case SearchOperationType.Incoming:
                sb.AppendFront("from " + src.PatternElement.UnprefixedName + " incoming <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-" + connectednessCheck + "\n");
                break;
            case SearchOperationType.Incident:
                sb.AppendFront("from " + src.PatternElement.UnprefixedName + " incident <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->" + connectednessCheck + "\n"); 
                break;
            case SearchOperationType.ImplicitSource:
                sb.AppendFront("from <-" + src.PatternElement.UnprefixedName + "- get source " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + connectednessCheck + "\n");
                break;
            case SearchOperationType.ImplicitTarget:
                sb.AppendFront("from -" + src.PatternElement.UnprefixedName + "-> get target " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + connectednessCheck + "\n");
                break;
            case SearchOperationType.Implicit:
                sb.AppendFront("from <-" + src.PatternElement.UnprefixedName + "-> get implicit " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + connectednessCheck + "\n"); 
                break;
            case SearchOperationType.Lookup:
                if(tgt.PatternElement is PatternNode)
                    sb.AppendFront("lookup " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + " in graph" + connectednessCheck + "\n");
                else
                    sb.AppendFront("lookup -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-> in graph" + connectednessCheck + "\n");
                break;
            case SearchOperationType.ActionPreset:
                sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + connectednessCheck + ")\n");
                break;
            case SearchOperationType.NegIdptPreset:
                sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + (tgt.PatternElement.PresetBecauseOfIndependentInlining ? " after independent inlining" : "") + connectednessCheck + ")\n");
                break;
            case SearchOperationType.SubPreset:
                sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + connectednessCheck + ")\n");
                break;
            case SearchOperationType.Condition:
                sb.AppendFront("if { depending on " + String.Join(",", ((PatternCondition)so.Element).NeededNodeNames)
                    + (((PatternCondition)so.Element).NeededNodeNames.Length != 0 && ((PatternCondition)so.Element).NeededEdgeNames.Length != 0 ? "," : "")
                    + String.Join(",", ((PatternCondition)so.Element).NeededEdgeNames) + " }\n");
                break;
            case SearchOperationType.NegativePattern:
                sb.AppendFront("negative {\n");
                sb.Indent();
                Explain(((ScheduledSearchPlan)so.Element), sb, model);
                sb.Append("\n");
                ((ScheduledSearchPlan)so.Element).PatternGraph.ExplainNested(sb, model);
                sb.Unindent();
                sb.AppendFront("}\n");
                break;
            case SearchOperationType.IndependentPattern:
                sb.AppendFront("independent {\n");
                sb.Indent();
                Explain(((ScheduledSearchPlan)so.Element), sb, model);
                sb.Append("\n");
                ((ScheduledSearchPlan)so.Element).PatternGraph.ExplainNested(sb, model);
                sb.Unindent();
                sb.AppendFront("}\n");
                break;
            case SearchOperationType.PickFromStorage:
            case SearchOperationType.PickFromStorageDependent:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + so.Storage.ToString() + "}" + connectednessCheck + "\n");
                break;
            case SearchOperationType.MapWithStorage:
            case SearchOperationType.MapWithStorageDependent:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + so.Storage.ToString() + "[" + so.StorageIndex.ToString() + "]}" + connectednessCheck + "\n");
                break;
            case SearchOperationType.PickFromIndex:
            case SearchOperationType.PickFromIndexDependent:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + so.IndexAccess.ToString() + "}" + connectednessCheck + "\n");
                break;
            case SearchOperationType.PickByName:
            case SearchOperationType.PickByNameDependent:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + so.NameLookup.ToString() + "}" + connectednessCheck + "\n");
                break;
            case SearchOperationType.PickByUnique:
            case SearchOperationType.PickByUniqueDependent:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + so.UniqueLookup.ToString() + "}" + connectednessCheck + "\n");
                break;
            case SearchOperationType.Cast:
                sb.AppendFront(tgt.PatternElement.UnprefixedName + "<" + src.PatternElement.UnprefixedName + ">" + connectednessCheck + "\n");
                break;
            case SearchOperationType.Assign:
                sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " = " + src.PatternElement.UnprefixedName + ")\n");
                break;
            case SearchOperationType.Identity:
                sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " == " + src.PatternElement.UnprefixedName + ")\n");
                break;
            case SearchOperationType.AssignVar:
                sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " = expr" + ")\n");
                break;
            case SearchOperationType.LockLocalElementsForPatternpath:
                sb.AppendFront("lock for patternpath\n");
                break;
            case SearchOperationType.DefToBeYieldedTo:
                if(so.Element is PatternVariable)
                    sb.AppendFront("def " + ((PatternVariable)so.Element).Name + "\n");
                else
                    sb.AppendFront("def " + ((SearchPlanNode)so.Element).PatternElement.Name + "\n");
                break;
            case SearchOperationType.ParallelLookup:
                if(tgt.PatternElement is PatternNode)
                    sb.AppendFront("parallelized lookup " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + " in graph\n");
                else
                    sb.AppendFront("parallelized lookup -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-> in graph\n");
                break;
            case SearchOperationType.ParallelPickFromStorage:
            case SearchOperationType.ParallelPickFromStorageDependent:
                sb.AppendFront("parallelized " + tgt.PatternElement.UnprefixedName + "{" + so.Storage.ToString() + "}\n");
                break;
            case SearchOperationType.ParallelOutgoing:
                sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " outgoing -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->\n");
                break;
            case SearchOperationType.ParallelIncoming:
                sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " incoming <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-\n");
                break;
            case SearchOperationType.ParallelIncident:
                sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " incident <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->\n");
                break;
            case SearchOperationType.WriteParallelPreset:
            case SearchOperationType.ParallelPreset:
            case SearchOperationType.WriteParallelPresetVar:
            case SearchOperationType.ParallelPresetVar:
            case SearchOperationType.SetupParallelLookup:
            case SearchOperationType.SetupParallelPickFromStorage:
            case SearchOperationType.SetupParallelPickFromStorageDependent:
            case SearchOperationType.SetupParallelOutgoing:
            case SearchOperationType.SetupParallelIncoming:
            case SearchOperationType.SetupParallelIncident:
                break; // uninteresting to the user
            }
        }

        public static void Explain(ScheduledSearchPlan ssp, SourceBuilder sb, IGraphModel model)
        {
            foreach(SearchOperation searchOp in ssp.Operations)
            {
                Explain(searchOp, sb, model);
            }
        }
    }
}
