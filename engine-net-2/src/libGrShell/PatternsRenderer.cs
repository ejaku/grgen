// by Claude Code with Edgar Jakumeit

using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using de.unika.ipd.grGen.libGr;
using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    /// <summary>
    /// Renders the patterns (pattern graphs) of actions and subpatterns as a visual graph.
    /// Pattern nodes are rendered as nodes.
    /// Pattern edges are rendered as nodes with tentacle edges from source and to target.
    /// Variables are rendered as nodes with var/ref prefix.
    /// Nested patterns (alternatives, iterateds, negatives, independents) are visualized using group nodes.
    /// Subpattern usages are rendered as nodes with edges pointing to their subpattern definition.
    /// </summary>
    public class PatternsRenderer
    {
        // Realizer names for different pattern element categories
        private const string PatternNodeRealizer = "pattern_node";
        private const string PatternEdgeNodeRealizer = "pattern_edge_node";
        private const string PatternVariableRealizer = "pattern_variable";
        private const string TentacleEdgeRealizer = "tentacle_edge";
        private const string ActionGroupRealizer = "action_group";
        private const string SubpatternGroupRealizer = "subpattern_group";
        private const string PackageRealizer = "package_group";
        private const string AlternativeGroupRealizer = "alternative_group";
        private const string AlternativeCaseGroupRealizer = "alternative_case_group";
        private const string IteratedGroupRealizer = "iterated_group";
        private const string NegativeGroupRealizer = "negative_group";
        private const string IndependentGroupRealizer = "independent_group";
        private const string SubpatternUsageRealizer = "subpattern_usage";
        private const string SubpatternReferenceEdgeRealizer = "subpattern_reference_edge";

        private HashSet<string> createdSubpatternGroups;
        private HashSet<string> createdPackages;
        private HashSet<string> createdNodeNames;
        private int uniqueCounter;

        public void RenderPatternsAsGraph(IActions actions, IBasicGraphViewerClient basicGraphViewer)
        {
            createdSubpatternGroups = new HashSet<string>();
            createdPackages = new HashSet<string>();
            createdNodeNames = new HashSet<string>();
            uniqueCounter = 0;

            RegisterRealizers(basicGraphViewer);

            // Render all actions
            foreach(IAction action in actions.Actions)
            {
                RenderAction(action, basicGraphViewer);
            }

            // The subpatterns are rendered as they are encountered via embeddings
        }

        private void RegisterRealizers(IBasicGraphViewerClient basicGraphViewer)
        {
            // Pattern nodes: yellow rectangle
            basicGraphViewer.AddNodeRealizer(PatternNodeRealizer,
                GrColor.DarkYellow, GrColor.Yellow, GrColor.Black, GrNodeShape.Box);

            // Pattern edges rendered as nodes: khaki rhomb shape
            basicGraphViewer.AddNodeRealizer(PatternEdgeNodeRealizer,
                GrColor.DarkYellow, GrColor.Khaki, GrColor.Black, GrNodeShape.Rhomb);

            // Pattern variables: brown
            basicGraphViewer.AddNodeRealizer(PatternVariableRealizer,
                GrColor.DarkRed, GrColor.Brown, GrColor.Black, GrNodeShape.Box);

            // Tentacle edges: dark yellow continuous line connecting edge nodes to their endpoints
            basicGraphViewer.AddEdgeRealizer(TentacleEdgeRealizer,
                GrColor.DarkYellow, GrColor.Black, 1, GrLineStyle.Continuous);

            // Action group: white
            basicGraphViewer.AddNodeRealizer(ActionGroupRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Subpattern group: white
            basicGraphViewer.AddNodeRealizer(SubpatternGroupRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Package group: light grey
            basicGraphViewer.AddNodeRealizer(PackageRealizer,
                GrColor.Grey, GrColor.LightGrey, GrColor.Black, GrNodeShape.Box);

            // Alternative group: white
            basicGraphViewer.AddNodeRealizer(AlternativeGroupRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Alternative case group: light grey
            basicGraphViewer.AddNodeRealizer(AlternativeCaseGroupRealizer,
                GrColor.Grey, GrColor.LightGrey, GrColor.Black, GrNodeShape.Box);

            // Iterated group: light grey
            basicGraphViewer.AddNodeRealizer(IteratedGroupRealizer,
                GrColor.Grey, GrColor.LightGrey, GrColor.Black, GrNodeShape.Box);

            // Negative group: light red (NAC)
            basicGraphViewer.AddNodeRealizer(NegativeGroupRealizer,
                GrColor.DarkRed, GrColor.LightRed, GrColor.Black, GrNodeShape.Box);

            // Independent group: green (PAC)
            basicGraphViewer.AddNodeRealizer(IndependentGroupRealizer,
                GrColor.DarkGreen, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box);

            // Subpattern usage node: white with grey border
            basicGraphViewer.AddNodeRealizer(SubpatternUsageRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Subpattern reference edges: grey dashed line
            basicGraphViewer.AddEdgeRealizer(SubpatternReferenceEdgeRealizer,
                GrColor.Grey, GrColor.Black, 1, GrLineStyle.Dashed);
        }

        private void EnsurePackageNode(string package, IBasicGraphViewerClient basicGraphViewer)
        {
            if(package != null && createdPackages.Add(package))
            {
                basicGraphViewer.AddSubgraphNode("pkg_" + package, PackageRealizer, "<<package>>\\n" + package);
            }
        }

        private void MoveToPackageIfNeeded(string nodeName, string package, IBasicGraphViewerClient basicGraphViewer)
        {
            if(package != null)
                basicGraphViewer.MoveNode(nodeName, "pkg_" + package);
        }

        private void RenderAction(IAction action, IBasicGraphViewerClient basicGraphViewer)
        {
            IRulePattern rulePattern = action.RulePattern;
            IPatternGraph patternGraph = rulePattern.PatternGraph;

            // Ensure package node exists if action is in a package
            EnsurePackageNode(patternGraph.Package, basicGraphViewer);

            // Create a group node for the action
            string actionGroupName = "action_" + patternGraph.PackagePrefixedName;
            string actionLabel = "<<rule>>\\n" + patternGraph.Name;
            basicGraphViewer.AddSubgraphNode(actionGroupName, ActionGroupRealizer, actionLabel);

            // Move to package if needed
            MoveToPackageIfNeeded(actionGroupName, patternGraph.Package, basicGraphViewer);

            // Render the pattern graph contents inside the action group (top level)
            Dictionary<IPatternNode, string> nodeNameMap = new Dictionary<IPatternNode, string>();
            RenderPatternGraph(patternGraph, actionGroupName, true, nodeNameMap, basicGraphViewer);
        }

        private void RenderPatternGraph(IPatternGraph patternGraph, string containerGroupName, bool isTopLevel,
            Dictionary<IPatternNode, string> nodeNameMap, IBasicGraphViewerClient basicGraphViewer)
        {
            string prefix = containerGroupName + "_";

            // Render pattern nodes (only those defined in this pattern)
            // nodeNameMap is passed down from parent patterns so edges can reference nodes from ancestor patterns
            foreach(IPatternNode node in patternGraph.Nodes)
            {
                // Only render elements in their PointOfDefinition pattern
                // PointOfDefinition == null means it's a top-level parameter, only render at top level
                if(node.PointOfDefinition == null && !isTopLevel)
                    continue;
                if(node.PointOfDefinition != null && node.PointOfDefinition != patternGraph)
                    continue;

                string nodeName = GetUniqueNodeName(prefix + "n_" + node.UnprefixedName);
                nodeNameMap[node] = nodeName;
                string nodeLabel = BuildPatternNodeLabel(node);
                basicGraphViewer.AddNode(nodeName, PatternNodeRealizer, nodeLabel);
                basicGraphViewer.MoveNode(nodeName, containerGroupName);
            }

            // Render pattern variables (only those defined in this pattern)
            foreach(IPatternVariable variable in patternGraph.Variables)
            {
                // Only render elements in their PointOfDefinition pattern
                // PointOfDefinition == null means it's a top-level parameter, only render at top level
                if(variable.PointOfDefinition == null && !isTopLevel)
                    continue;
                if(variable.PointOfDefinition != null && variable.PointOfDefinition != patternGraph)
                    continue;

                string varNodeName = GetUniqueNodeName(prefix + "v_" + variable.UnprefixedName);
                string varLabel = BuildPatternVariableLabel(variable);
                basicGraphViewer.AddNode(varNodeName, PatternVariableRealizer, varLabel);
                basicGraphViewer.MoveNode(varNodeName, containerGroupName);
            }

            // Render pattern edges as nodes with tentacle edges (only those defined in this pattern)
            foreach(IPatternEdge edge in patternGraph.Edges)
            {
                // Only render elements in their PointOfDefinition pattern
                // PointOfDefinition == null means it's a top-level parameter, only render at top level
                if(edge.PointOfDefinition == null && !isTopLevel)
                    continue;
                if(edge.PointOfDefinition != null && edge.PointOfDefinition != patternGraph)
                    continue;

                IPatternNode source = patternGraph.GetSource(edge);
                IPatternNode target = patternGraph.GetTarget(edge);

                // Create a node for the edge
                string edgeNodeName = GetUniqueNodeName(prefix + "e_" + edge.UnprefixedName);
                string edgeLabel = BuildPatternEdgeLabel(edge);
                basicGraphViewer.AddNode(edgeNodeName, PatternEdgeNodeRealizer, edgeLabel);
                basicGraphViewer.MoveNode(edgeNodeName, containerGroupName);

                // Create tentacle edge from source to edge node (if source exists)
                if(source != null && nodeNameMap.ContainsKey(source))
                {
                    string srcTentacleName = GetUniqueNodeName(prefix + "src_" + edge.UnprefixedName);
                    basicGraphViewer.AddEdge(srcTentacleName, nodeNameMap[source], edgeNodeName, TentacleEdgeRealizer, "");
                }

                // Create tentacle edge from edge node to target (if target exists)
                if(target != null && nodeNameMap.ContainsKey(target))
                {
                    string tgtTentacleName = GetUniqueNodeName(prefix + "tgt_" + edge.UnprefixedName);
                    basicGraphViewer.AddEdge(tgtTentacleName, edgeNodeName, nodeNameMap[target], TentacleEdgeRealizer, "");
                }
            }

            // Render subpattern usages (embedded graphs)
            foreach(IPatternGraphEmbedding embedding in patternGraph.EmbeddedGraphs)
            {
                RenderSubpatternUsage(embedding, prefix, containerGroupName, basicGraphViewer);
            }

            // Render alternatives
            int altIndex = 0;
            foreach(IAlternative alternative in patternGraph.Alternatives)
            {
                RenderAlternative(alternative, prefix, altIndex, containerGroupName, nodeNameMap, basicGraphViewer);
                altIndex++;
            }

            // Render iterateds
            int iterIndex = 0;
            foreach(IIterated iterated in patternGraph.Iterateds)
            {
                RenderIterated(iterated, prefix, iterIndex, containerGroupName, nodeNameMap, basicGraphViewer);
                iterIndex++;
            }

            // Render negative pattern graphs (NACs)
            int negIndex = 0;
            foreach(IPatternGraph negativePattern in patternGraph.NegativePatternGraphs)
            {
                RenderNegativePattern(negativePattern, prefix, negIndex, containerGroupName, nodeNameMap, basicGraphViewer);
                negIndex++;
            }

            // Render independent pattern graphs (PACs)
            int indIndex = 0;
            foreach(IPatternGraph independentPattern in patternGraph.IndependentPatternGraphs)
            {
                RenderIndependentPattern(independentPattern, prefix, indIndex, containerGroupName, nodeNameMap, basicGraphViewer);
                indIndex++;
            }
        }

        private void RenderSubpatternUsage(IPatternGraphEmbedding embedding, string prefix, string containerGroupName, IBasicGraphViewerClient basicGraphViewer)
        {
            // Create a node representing the subpattern usage
            // Label format: usage-name:subpattern-name
            string usageName = GetUniqueNodeName(prefix + "usage_" + embedding.Name);
            string subpatternName = embedding.EmbeddedGraph.Name;
            string usageLabel = embedding.Name + ":" + subpatternName;
            basicGraphViewer.AddNode(usageName, SubpatternUsageRealizer, usageLabel);
            basicGraphViewer.MoveNode(usageName, containerGroupName);

            // Ensure the subpattern definition is rendered
            IPatternGraph embeddedGraph = embedding.EmbeddedGraph;
            string subpatternGroupName = EnsureSubpatternGroup(embeddedGraph, basicGraphViewer);

            // Create an edge from the usage to the subpattern definition
            string refEdgeName = GetUniqueNodeName(prefix + "ref_" + embedding.Name);
            basicGraphViewer.AddEdge(refEdgeName, usageName, subpatternGroupName, SubpatternReferenceEdgeRealizer, "");
        }

        private string EnsureSubpatternGroup(IPatternGraph subpatternGraph, IBasicGraphViewerClient basicGraphViewer)
        {
            string subpatternGroupName = "subpattern_" + subpatternGraph.PackagePrefixedName;

            if(createdSubpatternGroups.Add(subpatternGroupName))
            {
                // Ensure package node exists if subpattern is in a package
                EnsurePackageNode(subpatternGraph.Package, basicGraphViewer);

                // Create the subpattern group node
                string subpatternLabel = "<<subpattern>>\\n" + subpatternGraph.Name;
                basicGraphViewer.AddSubgraphNode(subpatternGroupName, SubpatternGroupRealizer, subpatternLabel);

                // Move to package if needed
                MoveToPackageIfNeeded(subpatternGroupName, subpatternGraph.Package, basicGraphViewer);

                // Render the subpattern's pattern graph contents (new nodeNameMap for subpattern)
                Dictionary<IPatternNode, string> subpatternNodeNameMap = new Dictionary<IPatternNode, string>();
                RenderPatternGraph(subpatternGraph, subpatternGroupName, true, subpatternNodeNameMap, basicGraphViewer);
            }

            return subpatternGroupName;
        }

        private void RenderAlternative(IAlternative alternative, string prefix, int altIndex, string containerGroupName,
            Dictionary<IPatternNode, string> nodeNameMap, IBasicGraphViewerClient basicGraphViewer)
        {
            // Create a group node for the alternative
            string altGroupName = GetUniqueNodeName(prefix + "alt_" + altIndex);
            string altLabel = "<<alternative>>";
            basicGraphViewer.AddSubgraphNode(altGroupName, AlternativeGroupRealizer, altLabel);
            basicGraphViewer.MoveNode(altGroupName, containerGroupName);

            // Render each alternative case as a nested group
            // Each case gets a copy of the parent's nodeNameMap so siblings don't see each other's nodes
            int caseIndex = 0;
            foreach(IPatternGraph altCase in alternative.AlternativeCases)
            {
                string caseGroupName = GetUniqueNodeName(altGroupName + "_case_" + caseIndex);
                string caseLabel = altCase.Name;
                basicGraphViewer.AddSubgraphNode(caseGroupName, AlternativeCaseGroupRealizer, caseLabel);
                basicGraphViewer.MoveNode(caseGroupName, altGroupName);

                Dictionary<IPatternNode, string> caseNodeNameMap = new Dictionary<IPatternNode, string>(nodeNameMap);
                RenderPatternGraph(altCase, caseGroupName, false, caseNodeNameMap, basicGraphViewer);
                caseIndex++;
            }
        }

        private void RenderIterated(IIterated iterated, string prefix, int iterIndex, string containerGroupName,
            Dictionary<IPatternNode, string> nodeNameMap, IBasicGraphViewerClient basicGraphViewer)
        {
            IPatternGraph iteratedPattern = iterated.IteratedPattern;

            // Create a group node for the iterated
            string iterGroupName = GetUniqueNodeName(prefix + "iter_" + iterIndex);
            string iterLabel = BuildIteratedLabel(iterated);
            basicGraphViewer.AddSubgraphNode(iterGroupName, IteratedGroupRealizer, iterLabel);
            basicGraphViewer.MoveNode(iterGroupName, containerGroupName);

            // Render the iterated pattern graph contents
            // Copy the parent's nodeNameMap so this nested pattern doesn't affect siblings
            Dictionary<IPatternNode, string> iterNodeNameMap = new Dictionary<IPatternNode, string>(nodeNameMap);
            RenderPatternGraph(iteratedPattern, iterGroupName, false, iterNodeNameMap, basicGraphViewer);
        }

        private void RenderNegativePattern(IPatternGraph negativePattern, string prefix, int negIndex, string containerGroupName,
            Dictionary<IPatternNode, string> nodeNameMap, IBasicGraphViewerClient basicGraphViewer)
        {
            // Create a group node for the negative pattern (NAC)
            string negGroupName = GetUniqueNodeName(prefix + "neg_" + negIndex);
            string negLabel = "<<negative>>\\n" + negativePattern.Name;
            basicGraphViewer.AddSubgraphNode(negGroupName, NegativeGroupRealizer, negLabel);
            basicGraphViewer.MoveNode(negGroupName, containerGroupName);

            // Render the negative pattern graph contents
            // Copy the parent's nodeNameMap so this nested pattern doesn't affect siblings
            Dictionary<IPatternNode, string> negNodeNameMap = new Dictionary<IPatternNode, string>(nodeNameMap);
            RenderPatternGraph(negativePattern, negGroupName, false, negNodeNameMap, basicGraphViewer);
        }

        private void RenderIndependentPattern(IPatternGraph independentPattern, string prefix, int indIndex, string containerGroupName,
            Dictionary<IPatternNode, string> nodeNameMap, IBasicGraphViewerClient basicGraphViewer)
        {
            // Create a group node for the independent pattern (PAC)
            string indGroupName = GetUniqueNodeName(prefix + "ind_" + indIndex);
            string indLabel = "<<independent>>\\n" + independentPattern.Name;
            basicGraphViewer.AddSubgraphNode(indGroupName, IndependentGroupRealizer, indLabel);
            basicGraphViewer.MoveNode(indGroupName, containerGroupName);

            // Render the independent pattern graph contents
            // Copy the parent's nodeNameMap so this nested pattern doesn't affect siblings
            Dictionary<IPatternNode, string> indNodeNameMap = new Dictionary<IPatternNode, string>(nodeNameMap);
            RenderPatternGraph(independentPattern, indGroupName, false, indNodeNameMap, basicGraphViewer);
        }

        private string GetUniqueNodeName(string baseName)
        {
            string name = baseName;
            while(createdNodeNames.Contains(name))
            {
                name = baseName + "_" + (++uniqueCounter);
            }
            createdNodeNames.Add(name);
            return name;
        }

        private string BuildPatternNodeLabel(IPatternNode node)
        {
            StringBuilder sb = new StringBuilder();
            // Add "def" prefix for def-to-be-yielded elements
            if(node.DefToBeYieldedTo)
                sb.Append("def ");
            sb.Append(node.UnprefixedName);
            sb.Append(":");
            sb.Append(node.Type.PackagePrefixedName);
            return sb.ToString();
        }

        private string BuildPatternEdgeLabel(IPatternEdge edge)
        {
            StringBuilder sb = new StringBuilder();
            // Add "def" prefix for def-to-be-yielded elements
            if(edge.DefToBeYieldedTo)
                sb.Append("def ");
            sb.Append(edge.UnprefixedName);
            sb.Append(":");
            sb.Append(edge.Type.PackagePrefixedName);
            return sb.ToString();
        }

        private string BuildPatternVariableLabel(IPatternVariable variable)
        {
            StringBuilder sb = new StringBuilder();
            // Add "def" prefix for def-to-be-yielded elements
            if(variable.DefToBeYieldedTo)
                sb.Append("def ");
            // Use "ref" for reference types, "var" for basic types
            if(TypesHelper.IsRefType(variable.Type))
                sb.Append("ref ");
            else
                sb.Append("var ");
            sb.Append(variable.UnprefixedName);
            sb.Append(":");
            sb.Append(TypesHelper.DotNetTypeToXgrsType(variable.Type));
            return sb.ToString();
        }

        private string BuildIteratedLabel(IIterated iterated)
        {
            StringBuilder sb = new StringBuilder();
            // Use appropriate stereotype for common cases, [min..max] for others
            if(iterated.MinMatches == 0 && iterated.MaxMatches == 1)
            {
                sb.Append("<<optional>>\\n");
                sb.Append(iterated.IteratedPattern.Name);
            }
            else if(iterated.MinMatches == 1 && iterated.MaxMatches == 0)
            {
                sb.Append("<<multiple>>\\n");
                sb.Append(iterated.IteratedPattern.Name);
            }
            else if(iterated.MinMatches == 0 && iterated.MaxMatches == 0)
            {
                sb.Append("<<iterated>>\\n");
                sb.Append(iterated.IteratedPattern.Name);
            }
            else
            {
                sb.Append(iterated.IteratedPattern.Name);
                sb.Append("\\n[");
                sb.Append(iterated.MinMatches);
                sb.Append("..");
                if(iterated.MaxMatches == 0)
                    sb.Append("*");
                else
                    sb.Append(iterated.MaxMatches);
                sb.Append("]");
            }
            return sb.ToString();
        }
    }
}
