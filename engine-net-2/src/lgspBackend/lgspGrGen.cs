/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define NO_EDGE_LOOKUP
//#define DUMP_PATTERNS
//#define USE_NET_3_5 // use .NET 3.5 for compiling the generated code (not needed) and the user extensions (maybe needed there)

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The C#-part of the GrGen.NET frontend.
    /// It is responsible for generating initial actions with static search plans.
    /// (and compiling the XGRSs of the exec statements vie LGSPSequenceGenerator)
    /// </summary>
    public class LGSPGrGen
    {
        private Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        private bool assemblyHandlerInstalled = false;

        private ProcessSpecFlags flags;

        /// <summary>
        /// Constructs an LGSPGrGen object.
        /// </summary>
        /// <param name="flags">Flags specifying how the specification should be processed.</param>
        public LGSPGrGen(ProcessSpecFlags flags)
        {
            this.flags = flags;
        }

        internal bool FireEvents { get { return (flags & ProcessSpecFlags.NoEvents) == 0; } }

        /// <summary>
        /// Returns a string where all "wrong" directory separator chars are replaced by the ones used by the system 
        /// </summary>
        /// <param name="path">The original path string potentially with wrong chars</param>
        /// <returns>The corrected path string</returns>
        static String FixDirectorySeparators(String path)
        {
            if(Path.DirectorySeparatorChar != '\\')
                path = path.Replace('\\', Path.DirectorySeparatorChar);
            if(Path.DirectorySeparatorChar != '/')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            loadedAssemblies.TryGetValue(args.Name, out assembly);
            return assembly;
        }

        void AddAssembly(Assembly assembly)
        {
            loadedAssemblies.Add(assembly.FullName, assembly);
            if(!assemblyHandlerInstalled)
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                assemblyHandlerInstalled = true;
            }
        }

        bool ProcessModel(CompileConfiguration cc)
        {
            String modelName = Path.GetFileNameWithoutExtension(cc.modelFilename);
            String modelExtension = Path.GetExtension(cc.modelFilename);

            CSharpCodeProvider compiler;
            CompilerParameters compParams;
            SetupCompiler(null, out compiler, out compParams);
            compParams.ReferencedAssemblies.AddRange(cc.externalAssemblies);
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.OutputAssembly = cc.destDir + "lgsp-" + modelName + ".dll";

            CompilerResults compResults;
            try
            {
                if(File.Exists(cc.destDir + modelName + "ExternalFunctions.cs"))
                {
                    String externalFunctionsFile = cc.destDir + modelName + "ExternalFunctions.cs";
                    String externalFunctionsImplFile = cc.destDir + modelName + "ExternalFunctionsImpl.cs";
                    if(cc.modelStubFilename != null)
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.modelFilename, cc.modelStubFilename, externalFunctionsFile, externalFunctionsImplFile);
                    else
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.modelFilename, externalFunctionsFile, externalFunctionsImplFile);
                }
                else
                {
                    if(cc.modelStubFilename != null)
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.modelFilename, cc.modelStubFilename);
                    else
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.modelFilename);
                }
                if(compResults.Errors.HasErrors)
                {
                    Console.Error.WriteLine("Illegal model C# source code: " + compResults.Errors.Count + " Errors:");
                    foreach(CompilerError error in compResults.Errors)
                        Console.Error.WriteLine("Line: " + error.Line + " - " + error.ErrorText + " @ " + error.FileName);
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Unable to compile model: {0}", ex.Message);
                return false;
            }

            cc.modelAssembly = compResults.CompiledAssembly;
            cc.modelAssemblyName = compParams.OutputAssembly;
            AddAssembly(cc.modelAssembly);

            Console.WriteLine(" - Model assembly \"{0}\" generated.", cc.modelAssemblyName);
            return true;
        }

        IGraphModel GetGraphModel(Assembly modelAssembly)
        {
            Type modelType = null;
            try
            {
                foreach(Type type in modelAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.GetInterface("IGraphModel") != null && type.GetInterface("IGraph") == null)
                    {
                        if(modelType != null)
                        {
                            Console.Error.WriteLine("The given model contains more than one IGraphModel implementation: '"
                                + modelType + "' and '" + type + "'");
                            return null;
                        }
                        modelType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                Console.WriteLine(e);
            }
            if(modelType == null)
            {
                Console.Error.WriteLine("The given model does not contain an IGraphModel implementation!");
                return null;
            }

            return (IGraphModel) Activator.CreateInstance(modelType);
        }

        /// <summary>
        /// Generate plan graph for given pattern graph with costs from initial static schedule handed in with graph elements.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        private static PlanGraph GenerateStaticPlanGraph(PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike)
        {
            //
            // If you change this method, chances are high you also want to change GeneratePlanGraph in LGSPMatcherGenerator
            // todo: unify it with GeneratePlanGraph in LGSPMatcherGenerator
            //

            // the frontend delivers costs in between 1.0 and 10.0
            // the highest prio gets 1.0, zero prio gets 10.0, the others about in between
            // the default if no prio is given is cost 5.5

            // here we assign to a lookup the node.cost or edge.cost from frontend
            // for implicit source/target cost 0
            // and for following incoming/outgoing edges cost (5.5 + edge.cost) / 2
            // this way high prio edges tend to be looked up

            // Create root node
            // Create plan graph nodes for all pattern graph nodes and all pattern graph edges
            // Create "lookup" plan graph edge from root node to each plan graph node
            // Create "implicit source" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the source node of the pattern graph edge
            // Create "implicit target" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the target node of the pattern graph edge
            // Create "incoming" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the incoming edges of the pattern node
            // Create "outgoing" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the outgoing edges of the pattern node
            // Ensured: there's no plan graph edge with a preset element as target besides the lookup,
            //     so presets are only search operation sources
            // Create "pick from storage" plan graph edge for plan graph nodes which are to be picked from a storage,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from storage attribute" plan graph edge from storage attribute owner to storage picking result,
            //     no lookup, no other plan graph edge having this node as target
            // Create "map" by storage plan graph edge from accessor to storage mapping result
            //     no lookup, no other plan graph edge having this node as target
            // Create "pick from index" plan graph edge for plan graph nodes which are to be picked from an index,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from index depending" plan graph edge from node the index expressions depend on,
            //     no lookup, no other plan graph edge having this node as target
            // Create "cast" plan graph edge from element before casting to cast result,
            //     no lookup, no other plan graph edge having this node as target

            PlanNode[] planNodes = new PlanNode[patternGraph.nodesPlusInlined.Length + patternGraph.edgesPlusInlined.Length];
            List<PlanEdge> planEdges = new List<PlanEdge>(patternGraph.nodesPlusInlined.Length + 5 * patternGraph.edgesPlusInlined.Length);   // upper bound for num of edges

            int nodesIndex = 0;

            // create plan nodes and lookup plan edges for all pattern graph nodes
            PlanNode planRoot = new PlanNode("root");
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.DefToBeYieldedTo)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.DefToBeYieldedTo;
                }
                else if(node.PointOfDefinition == null)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if (node.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(node.Storage != null)
                {
                    if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(node.IndexAccess != null)
                {
                    if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                    }
                }
                else if(node.NameLookup != null)
                {
                    if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                    }
                }
                else if(node.UniqueLookup != null)
                {
                    if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                    }
                }
                else if(node.ElementBeforeCasting != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
                }
                else
                {
                    cost = node.Cost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                planNodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                if(searchOperationType != SearchOperationType.Void)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }

                node.TempPlanMapping = planNodes[nodesIndex];
                ++nodesIndex;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(edge.DefToBeYieldedTo)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.DefToBeYieldedTo;
                } 
                else if(edge.PointOfDefinition == null)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if(edge.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(edge.Storage != null)
                {
                    if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(edge.IndexAccess != null)
                {
                    if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                    }
                }
                else if(edge.NameLookup != null)
                {
                    if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                    }
                }
                else if(edge.UniqueLookup != null)
                {
                    if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                    }
                }
                else if(edge.ElementBeforeCasting != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
                }
                else
                {
                    cost = edge.Cost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    patternGraph.GetSourcePlusInlined(edge)!=null ? patternGraph.GetSourcePlusInlined(edge).TempPlanMapping : null,
                    patternGraph.GetTargetPlusInlined(edge)!=null ? patternGraph.GetTargetPlusInlined(edge).TempPlanMapping : null);
#if NO_EDGE_LOOKUP
                if(isPreset)
                {
#endif
                if(searchOperationType != SearchOperationType.Void)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }
#if NO_EDGE_LOOKUP
                }
#endif

                // only add implicit source operation if edge source is needed and the edge source is 
                // not a preset node and not a storage node and not an index node and not a cast node
                if(patternGraph.GetSourcePlusInlined(edge) != null 
                    && !patternGraph.GetSourcePlusInlined(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetSourcePlusInlined(edge).Storage == null
                    && patternGraph.GetSourcePlusInlined(edge).IndexAccess == null
                    && patternGraph.GetSourcePlusInlined(edge).NameLookup == null
                    && patternGraph.GetSourcePlusInlined(edge).UniqueLookup == null
                    && patternGraph.GetSourcePlusInlined(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ? 
                        SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                    PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetSourcePlusInlined(edge).TempPlanMapping, 0);
                    planEdges.Add(implSrcPlanEdge);
                    patternGraph.GetSourcePlusInlined(edge).TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is
                // not a preset node and not a storage node and not an index node and not a cast node
                if(patternGraph.GetTargetPlusInlined(edge) != null
                    && !patternGraph.GetTargetPlusInlined(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetTargetPlusInlined(edge).Storage == null
                    && patternGraph.GetTargetPlusInlined(edge).IndexAccess == null
                    && patternGraph.GetTargetPlusInlined(edge).NameLookup == null
                    && patternGraph.GetTargetPlusInlined(edge).UniqueLookup == null
                    && patternGraph.GetTargetPlusInlined(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                    PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetTargetPlusInlined(edge).TempPlanMapping, 0);
                    planEdges.Add(implTgtPlanEdge);
                    patternGraph.GetTargetPlusInlined(edge).TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset and not storage determined and not index determined and not a cast
                if(!isPreset 
                    && edge.Storage == null
                    && edge.IndexAccess == null
                    && edge.NameLookup == null
                    && edge.UniqueLookup == null
                    && edge.ElementBeforeCasting == null)
                {
                    // no outgoing on source node if no source
                    if(patternGraph.GetSourcePlusInlined(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Outgoing : SearchOperationType.Incident;
                        PlanEdge outPlanEdge = new PlanEdge(operation, patternGraph.GetSourcePlusInlined(edge).TempPlanMapping,
                            planNodes[nodesIndex], (edge.Cost+5.5f)/2);
                        planEdges.Add(outPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(outPlanEdge);
                    }
                    // no incoming on target node if no target
                    if(patternGraph.GetTargetPlusInlined(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Incoming: SearchOperationType.Incident;
                        PlanEdge inPlanEdge = new PlanEdge(operation, patternGraph.GetTargetPlusInlined(edge).TempPlanMapping,
                            planNodes[nodesIndex], (edge.Cost+5.5f)/2);
                        planEdges.Add(inPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(inPlanEdge);
                    }
                }

                edge.TempPlanMapping = planNodes[nodesIndex];
                ++nodesIndex;
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling dependent storage and index picking (can't be done in first run due to dependencies between elements)

            // create map with storage plan edges for all pattern graph nodes
            // which are the result of a mapping/picking from attribute operation (with a storage or an index or the name map or the unique index) 
            // or element type casting or assignment
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];
                if(node.PointOfDefinition == patternGraph)
                {
                    if(node.Storage!=null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()!=null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(
                            node.StorageIndex != null ? SearchOperationType.MapWithStorageDependent : SearchOperationType.PickFromStorageDependent,
                            node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(node.IndexAccess != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge indexAccessPlanEdge = new PlanEdge(
                            SearchOperationType.PickFromIndexDependent,
                            node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, 1);
                        planEdges.Add(indexAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(indexAccessPlanEdge);
                    }
                    else if(node.NameLookup != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge nameLookupPlanEdge = new PlanEdge(
                            SearchOperationType.PickByNameDependent,
                            node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, 1);
                        planEdges.Add(nameLookupPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(nameLookupPlanEdge);
                    }
                    else if(node.UniqueLookup != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge uniqueLookupPlanEdge = new PlanEdge(
                            SearchOperationType.PickByUniqueDependent,
                            node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, 1);
                        planEdges.Add(uniqueLookupPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(uniqueLookupPlanEdge);
                    }
                    else if(node.ElementBeforeCasting != null)
                    {
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            node.ElementBeforeCasting.TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(castPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                    else if(node.AssignmentSource != null)
                    {
                        PlanEdge assignPlanEdge = new PlanEdge(SearchOperationType.Assign,
                            node.AssignmentSource.TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(assignPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);

                        if(!node.AssignmentSource.TempPlanMapping.IsPreset)
                        {
                            PlanEdge assignPlanEdgeOpposite = new PlanEdge(SearchOperationType.Assign,
                                node.TempPlanMapping, node.AssignmentSource.TempPlanMapping, 1);
                            planEdges.Add(assignPlanEdgeOpposite);
                            node.AssignmentSource.TempPlanMapping.IncomingEdges.Add(assignPlanEdgeOpposite);
                        }
                    }
                }
            }

            // create map with storage plan edges for all pattern graph edges 
            // which are the result of a mapping/picking from attribute operation (with a storage or an index or the name map or the unique index)
            // or element type casting or assignment
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];
                if(edge.PointOfDefinition == patternGraph)
                {
                    if(edge.Storage!=null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()!=null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(
                            edge.StorageIndex != null ? SearchOperationType.MapWithStorageDependent : SearchOperationType.PickFromStorageDependent,
                            edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(edge.IndexAccess != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge indexAccessPlanEdge = new PlanEdge(
                            SearchOperationType.PickFromIndexDependent,
                            edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, 1);
                        planEdges.Add(indexAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(indexAccessPlanEdge);
                    }
                    else if(edge.NameLookup != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge nameLookupPlanEdge = new PlanEdge(
                            SearchOperationType.PickByNameDependent,
                            edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, 1);
                        planEdges.Add(nameLookupPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(nameLookupPlanEdge);
                    }
                    else if(edge.UniqueLookup != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                    {
                        PlanEdge uniqueLookupPlanEdge = new PlanEdge(
                            SearchOperationType.PickByUniqueDependent,
                            edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, 1);
                        planEdges.Add(uniqueLookupPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(uniqueLookupPlanEdge);
                    }
                    else if(edge.ElementBeforeCasting != null)
                    {
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            edge.ElementBeforeCasting.TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(castPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                    else if(edge.AssignmentSource != null)
                    {
                        PlanEdge assignPlanEdge = new PlanEdge(SearchOperationType.Assign,
                            edge.AssignmentSource.TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(assignPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);
                        
                        if(!edge.AssignmentSource.TempPlanMapping.IsPreset)
                        {
                            PlanEdge assignPlanEdgeOpposite = new PlanEdge(SearchOperationType.Assign,
                                edge.TempPlanMapping, edge.AssignmentSource.TempPlanMapping, 1);
                            planEdges.Add(assignPlanEdgeOpposite);
                            edge.AssignmentSource.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);
                        }
                    }
                }
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }
       
        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of static schedule information given by rulePattern elements, 
        /// or out of statistics stemming from loading previously serialized statistics 
        /// utilizing code of the lgsp matcher generator.
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        internal static void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPGraphStatistics graphStatistics, 
            LGSPMatcherGenerator matcherGen,
            bool isSubpatternLike, bool isNegativeOrIndependent)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                PlanGraph planGraph;
                if(graphStatistics != null)
                    planGraph = matcherGen.GeneratePlanGraph(graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike);
                else
                    planGraph = GenerateStaticPlanGraph(patternGraph, isNegativeOrIndependent, isSubpatternLike);
                matcherGen.MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
                SearchPlanGraph searchPlanGraph = matcherGen.GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = matcherGen.ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent);
                matcherGen.AppendHomomorphyInformation(scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(neg, graphStatistics, matcherGen, isSubpatternLike, true);
                }

                foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(idpt, graphStatistics, matcherGen, isSubpatternLike, true);
                }

                foreach(Alternative alt in patternGraph.alternativesPlusInlined)
                {
                    foreach(PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, graphStatistics, matcherGen, true, false);
                    }
                }

                foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, graphStatistics, matcherGen, true, false);
                }
            }
        }

        public static bool ExecuteGrGenJava(String tmpDir, ProcessSpecFlags flags,
            out List<String> genModelFiles, out List<String> genModelStubFiles,
            out List<String> genActionsFiles, params String[] sourceFiles)
        {
            genModelFiles = new List<string>();
            genModelStubFiles = new List<string>();
            genActionsFiles = new List<string>();

            if(sourceFiles.Length == 0)
            {
                Console.Error.WriteLine("No GrGen.NET source files specified!");
                return false;
            }

            String binPath = FixDirectorySeparators(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    + Path.DirectorySeparatorChar;

            Process grGenJava = null;

            // The code handling CTRL+C makes sure, the Java process is killed even if CTRL+C was pressed at the very
            // begining of Process.Start without making the program hang in an indeterminate state.

            bool ctrlCPressed = false;
            bool delayCtrlC = true;    // between registering the handler and the end of Process.Start, delay actually handling of CTRL+C
            ConsoleCancelEventHandler ctrlCHandler = delegate(object sender, ConsoleCancelEventArgs e)
            {
                if(!delayCtrlC)
                {
                    if(grGenJava == null || grGenJava.HasExited) return;

                    Console.Error.WriteLine("Aborting...");
                    System.Threading.Thread.Sleep(100);     // a short delay to make sure the process is correctly started
                    if(!grGenJava.HasExited)
                        grGenJava.Kill();
                }
                ctrlCPressed = true;
                if(e != null)               // compare to null, as we also call this by ourself when handling has been delayed
                    e.Cancel = true;        // we handled the cancel event
            };

            try
            {
                String javaString;
                if(Environment.OSVersion.Platform == PlatformID.Unix) javaString = "java";
                else javaString = "javaw";

                String execStr = "-Xss1M -Xmx1024M -jar \"" + binPath + "grgen.jar\" "
                    + "-b de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2 "
                    + "-c \"" + tmpDir + Path.DirectorySeparatorChar + "printOutput.txt\" "
                    + "-o \"" + tmpDir + "\""
                    + ((flags & ProcessSpecFlags.NoEvents) != 0 ? " --noevents" : "")
                    + " \"" + String.Join("\" \"", sourceFiles) + "\"";
                ProcessStartInfo startInfo = new ProcessStartInfo(javaString, execStr);
                startInfo.CreateNoWindow = true;
                try
                {
                    Console.CancelKeyPress += ctrlCHandler;

                    grGenJava = Process.Start(startInfo);

                    delayCtrlC = false;
                    if(ctrlCPressed) ctrlCHandler(null, null);

                    grGenJava.WaitForExit();
                }
                finally
                {
                    Console.CancelKeyPress -= ctrlCHandler;
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Unable to process specification: " + e.Message);
                return false;
            }

            if(ctrlCPressed)
                return false;

            bool noError = true, doneFound = false;
            using(StreamReader sr = new StreamReader(tmpDir + Path.DirectorySeparatorChar + "printOutput.txt"))
            {
                String frontStr = "  generating the ";
                String backStr = " file...";
                String frontStubStr = "  writing the ";
                String backStubStr = " stub file...";

                String line;
                while((line = sr.ReadLine()) != null)
                {
                    if(line.Contains("ERROR"))
                    {
                        Console.Error.WriteLine(line);
                        noError = false;
                        continue;
                    }
                    if(line.Contains("WARNING"))
                    {
                        Console.Error.WriteLine(line);
                        continue;
                    }
                    if(line.StartsWith(frontStr) && line.EndsWith(backStr))
                    {
                        String filename = line.Substring(frontStr.Length, line.Length - frontStr.Length - backStr.Length);
                        if(filename.EndsWith("Model.cs"))
                            genModelFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                        else if(filename.EndsWith("Actions_intermediate.cs"))
                            genActionsFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                    }
                    else if(line.StartsWith(frontStubStr) && line.EndsWith(backStubStr))
                    {
                        String filename = line.Substring(frontStubStr.Length, line.Length - frontStubStr.Length - backStubStr.Length);
                        if(filename.EndsWith("ModelStub.cs"))
                            genModelStubFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                    }
                    else if(line.StartsWith("done!"))
                        doneFound = true;
                }
            }

            return noError && doneFound;
        }


        enum ErrorType { NoError, GrGenJavaError, GrGenNetError };

        class CompileConfiguration
        {
            public CompileConfiguration(String specFile, String destDir, String tmpDir, String[] externalAssemblies)
            {
                _specFile = specFile;
                _destDir = destDir;
                _tmpDir = tmpDir;
                _externalAssemblies = externalAssemblies;
            }

            public String specFile { get { return _specFile; } }
            public String destDir { get { return _destDir; } }
            public String tmpDir { get { return _tmpDir; } }
            public String[] externalAssemblies { get { return _externalAssemblies; } }

            public String modelFilename { get { return _modelFilename; } set { _modelFilename = value; } }
            public String modelStubFilename { get { return _modelStubFilename; } set { _modelStubFilename = value; } }
            public String modelAssemblyName { get { return _modelAssemblyName; } set { _modelAssemblyName = value; } }
            public Assembly modelAssembly { get { return _modelAssembly; } set { _modelAssembly = value; } }

            public String actionsName { get { return _actionsName; } }
            public String actionsFilename { get { return _actionsFilename; } 
                set { 
                    _actionsFilename = value;
                    _actionsName = Path.GetFileNameWithoutExtension(_actionsFilename); 
                    _actionsName = _actionsName.Substring(0, _actionsName.Length - 13); // remove "_intermediate" suffix
                    _baseName = _actionsName.Substring(0, _actionsName.Length - 7); // remove "Actions" suffix
                    _actionsOutputFilename = _tmpDir + Path.DirectorySeparatorChar + _actionsName + ".cs";
                }
            }
            public String actionsOutputFilename { get { return _actionsOutputFilename; } }
            public String baseName { get { return _baseName; } }

            public String externalActionsExtensionFilename { get { return _externalActionsExtensionFilename; } set { _externalActionsExtensionFilename = value; } }
            public String externalActionsExtensionOutputFilename { get { return _externalActionsExtensionOutputFilename; } set { _externalActionsExtensionOutputFilename = value; } }


            private String _specFile;
            private String _destDir;
            private String _tmpDir;
            private String[] _externalAssemblies;

            private String _modelFilename;
            private String _modelStubFilename;
            private String _modelAssemblyName;
            private Assembly _modelAssembly;

            private String _actionsName;
            private String _actionsFilename;
            private String _actionsOutputFilename;
            private String _baseName;

            private String _externalActionsExtensionFilename;
            private String _externalActionsExtensionOutputFilename;
        }

        ErrorType ProcessSpecificationImpl(String specFile, String destDir, String tmpDir, String statisticsPath, String[] externalAssemblies)
        {
            Console.WriteLine("Building libraries...");

            CompileConfiguration cc = new CompileConfiguration(specFile, destDir, tmpDir, externalAssemblies);

            ///////////////////////////////////////////////
            // use java frontend to build the model and intermediate action source files

            ErrorType res = GenerateModelAndIntermediateActions(cc);
            if(res != ErrorType.NoError)
                return res;

            ///////////////////////////////////////////////
            // compile the model 

            if(!ProcessModel(cc))
                return ErrorType.GrGenNetError;

            IGraphModel model = GetGraphModel(cc.modelAssembly);
            if(model == null)
                return ErrorType.GrGenNetError;

            if((flags & ProcessSpecFlags.NoProcessActions) != 0)
                return ErrorType.NoError;

            ///////////////////////////////////////////////
            // get the actions source code

            String actionsOutputSource;
            res = GetActionsSourceCode(cc, model, statisticsPath, out actionsOutputSource);
            if(res != ErrorType.NoError)
                return res;

            if((flags & ProcessSpecFlags.NoCreateActionsAssembly) != 0)
                return ErrorType.NoError;

            ///////////////////////////////////////////////
            // finally compile the actions source file into an action assembly

            res = CompileActions(cc, actionsOutputSource);
            if(res != ErrorType.NoError)
                return res;

            return ErrorType.NoError;
        }

        private ErrorType GetActionsSourceCode(CompileConfiguration cc, IGraphModel model, 
            String statisticsPath, out String actionsOutputSource)
        {
            ErrorType res;
            if((flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseAllGeneratedFiles)
                res = ReuseExistingActionsSourceCode(cc.actionsOutputFilename, out actionsOutputSource);
            else
                res = GenerateActionsSourceCode(cc, model, statisticsPath, out actionsOutputSource);
            return res;
        }

        private static void SetupCompiler(String modelAssemblyName, 
            out CSharpCodeProvider compiler, out CompilerParameters compParams)
        {
#if USE_NET_3_5
            Dictionary<string,string> providerOptions = new Dictionary<string,string>();
            providerOptions.Add("CompilerVersion", "v3.5");
            compiler = new CSharpCodeProvider(providerOptions);
#else
            compiler = new CSharpCodeProvider();
#endif
            compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
#if USE_NET_3_5
            compParams.ReferencedAssemblies.Add("System.Core.dll");
#endif
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
            if(modelAssemblyName!=null)
                compParams.ReferencedAssemblies.Add(modelAssemblyName);
        }

        private static ErrorType CompileIntermediateActions(String modelAssemblyName, String actionsFilename, 
            out Assembly initialAssembly)
        {
            CSharpCodeProvider compiler;
            CompilerParameters compParams;
            SetupCompiler(modelAssemblyName, out compiler, out compParams);
            compParams.GenerateInMemory = true;
            compParams.CompilerOptions = "/optimize /d:INITIAL_WARMUP";
            compParams.TreatWarningsAsErrors = false;

            initialAssembly = null;
            CompilerResults compResultsWarmup;
            try
            {
                compResultsWarmup = compiler.CompileAssemblyFromFile(compParams, actionsFilename);
                if(compResultsWarmup.Errors.HasErrors)
                {
                    String errorMsg = compResultsWarmup.Errors.Count + " Errors:";
                    foreach(CompilerError error in compResultsWarmup.Errors)
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    Console.Error.WriteLine("Illegal actions C# input source code: " + errorMsg);
                    return ErrorType.GrGenNetError;
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Unable to compile initial actions: {0}", ex.Message);
                return ErrorType.GrGenNetError;
            }

            initialAssembly = compResultsWarmup.CompiledAssembly;
            return ErrorType.NoError;
        }

        private static ErrorType ReuseExistingActionsSourceCode(String actionsOutputFilename,
            out String actionsOutputSource)
        {
            try
            {
                using(StreamReader reader = new StreamReader(actionsOutputFilename))
                    actionsOutputSource = reader.ReadToEnd();
                return ErrorType.NoError;
            }
            catch(Exception)
            {
                Console.Error.WriteLine("Unable to read from file \"" + actionsOutputFilename + "\"!");
                actionsOutputSource = null;
                return ErrorType.GrGenNetError;
            }
        }

        private ErrorType GenerateActionsSourceCode(CompileConfiguration cc, IGraphModel model,
            String statisticsPath, out String actionsOutputSource)
        {
            ///////////////////////////////////////////////
            // compile the intermediate action files generated by the java frontend
            // to gain access via reflection to their content needed for matcher code generation
            // and collect that content

            actionsOutputSource = null;

            Assembly initialAssembly;
            ErrorType result = CompileIntermediateActions(cc.modelAssemblyName, cc.actionsFilename, 
                out initialAssembly);
            if(result != ErrorType.NoError)
                return result;

            Dictionary<String, Type> actionTypes;
            Dictionary<String, Type> proceduresTypes;
            LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns;
            CollectActionTypes(initialAssembly, out actionTypes, out proceduresTypes, out ruleAndMatchingPatterns);

            Dictionary<String, List<IFilter>> rulesToFilters;
            Dictionary<String, List<String>> filterFunctionsToInputTypes;
            Dictionary<String, List<String>> rulesToInputTypes;
            Dictionary<String, List<String>> rulesToOutputTypes;
            Dictionary<String, List<String>> sequencesToInputTypes;
            Dictionary<String, List<String>> sequencesToOutputTypes;
            Dictionary<String, List<String>> proceduresToInputTypes;
            Dictionary<String, List<String>> proceduresToOutputTypes;
            Dictionary<String, List<String>> functionsToInputTypes;
            Dictionary<String, String> functionsToOutputType;
            Dictionary<String, List<String>> rulesToTopLevelEntities;
            Dictionary<String, List<String>> rulesToTopLevelEntityTypes;
            CollectActionParameterTypes(ruleAndMatchingPatterns, model,
                out rulesToFilters, out filterFunctionsToInputTypes,
                out rulesToInputTypes, out rulesToOutputTypes,
                out rulesToTopLevelEntities, out rulesToTopLevelEntityTypes,
                out sequencesToInputTypes, out sequencesToOutputTypes,
                out proceduresToInputTypes, out proceduresToOutputTypes,
                out functionsToInputTypes, out functionsToOutputType);

            LGSPSequenceGenerator seqGen = new LGSPSequenceGenerator(this, model,
                rulesToFilters, filterFunctionsToInputTypes,
                rulesToInputTypes, rulesToOutputTypes,
                rulesToTopLevelEntities, rulesToTopLevelEntityTypes,
                sequencesToInputTypes, sequencesToOutputTypes,
                proceduresToInputTypes, proceduresToOutputTypes,
                functionsToInputTypes, functionsToOutputType);

            ///////////////////////////////////////////////
            // generate external extension source if needed (cause there are external action extension)

            bool isAutoGeneratedFilterExisting;
            bool isExternalFilterFunctionExisting;
            bool isExternalSequenceExisting;
            DetermineWhetherExternalActionsFileIsNeeded(ruleAndMatchingPatterns,
                out isAutoGeneratedFilterExisting, out isExternalFilterFunctionExisting, out isExternalSequenceExisting);

            SourceBuilder externalSource = null;
            if(isAutoGeneratedFilterExisting || isExternalFilterFunctionExisting || isExternalSequenceExisting)
            {
                EmitExternalActionsFileHeader(cc, model, isExternalFilterFunctionExisting || isExternalSequenceExisting,
                    ref externalSource);
            }

            ///////////////////////////////////////////////
            // take action intermediate file until action insertion point as base for action file 

            SourceBuilder source = new SourceBuilder((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0);
            source.Indent();
            source.Indent();

            bool actionPointFound;
            String actionsNamespace;
            result = CopyIntermediateCodeInsertingSequencesCode(cc.actionsFilename, 
                actionTypes, proceduresTypes,
                seqGen, source, out actionPointFound, out actionsNamespace);
            if(result != ErrorType.NoError)
                return result;

            if(!actionPointFound)
            {
                Console.Error.WriteLine("Illegal actions C# input source code: Actions insertion point not found!");
                return ErrorType.GrGenJavaError;
            }

            source.Unindent();
            source.Append("\n");

            ///////////////////////////////////////////////
            // generate and insert the matcher source code into the action file
            // already filled with the content of the action intermediate file until the action insertion point

            String unitName;
            int lastDot = actionsNamespace.LastIndexOf(".");
            if(lastDot == -1) unitName = "";
            else unitName = actionsNamespace.Substring(lastDot + 8);  // skip ".Action_"

            LGSPGraphStatistics graphStatistics = null;
            if(statisticsPath != null)
            {
                Console.WriteLine("Reading graph statistics from {0}", statisticsPath);
                graphStatistics = new LGSPGraphStatistics(model);
                graphStatistics.Parse(statisticsPath);
            }

            GenerateAndInsertMatcherSourceCode(model, cc.actionsName, unitName,
                cc.externalActionsExtensionFilename, ruleAndMatchingPatterns, seqGen,
                isAutoGeneratedFilterExisting, isExternalFilterFunctionExisting, 
                graphStatistics, statisticsPath,
                externalSource, source);

            actionsOutputSource = WriteSourceAndExternalSource(externalSource, source,
                cc.actionsOutputFilename, cc.externalActionsExtensionOutputFilename);
            return ErrorType.NoError;
        }

        private ErrorType CompileActions(CompileConfiguration cc, String actionsOutputSource)
        {
            CSharpCodeProvider compiler;
            CompilerParameters compParams;
            SetupCompiler(cc.modelAssemblyName, out compiler, out compParams);
            compParams.ReferencedAssemblies.AddRange(cc.externalAssemblies); 
            compParams.GenerateInMemory = false;
            compParams.IncludeDebugInformation = (flags & ProcessSpecFlags.CompileWithDebug) != 0;
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = cc.destDir + "lgsp-" + cc.actionsName + ".dll";

            CompilerResults compResults;
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
            {
                if(cc.externalActionsExtensionOutputFilename != null)
                {
                    if(cc.externalActionsExtensionFilename != null)
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.actionsOutputFilename, cc.externalActionsExtensionOutputFilename, cc.externalActionsExtensionFilename);
                    else
                        compResults = compiler.CompileAssemblyFromFile(compParams, cc.actionsOutputFilename, cc.externalActionsExtensionOutputFilename);
                }
                else
                    compResults = compiler.CompileAssemblyFromFile(compParams, cc.actionsOutputFilename);
            }
            else
            {
                if(cc.externalActionsExtensionOutputFilename != null)
                {
                    String externalActionsExtensionOutputSource;
                    String externalActionsExtensionSource;
                    ErrorType result = ReadExternalActionExtensionSources(cc.externalActionsExtensionOutputFilename, cc.externalActionsExtensionFilename,
                        out externalActionsExtensionOutputSource, out externalActionsExtensionSource);
                    if(result != ErrorType.NoError)
                        return result;
                    if(cc.externalActionsExtensionFilename != null)
                        compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource, externalActionsExtensionOutputSource, externalActionsExtensionSource);
                    else
                        compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource, externalActionsExtensionOutputSource);
                }
                else
                    compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource);
            }
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += String.Format("\r\n{0} at line {1} of {2}: {3}", error.IsWarning ? "Warning" : "ERROR", error.Line, error.FileName, error.ErrorText);
                Console.Error.WriteLine("Illegal generated actions C# source code (or erroneous programmed extension), " + errorMsg);
                return ErrorType.GrGenNetError;
            }

            Console.WriteLine(" - Actions assembly \"{0}\" generated.", compParams.OutputAssembly);
            return ErrorType.NoError;
        }

        private void GenerateAndInsertMatcherSourceCode(IGraphModel model, String actionsName, String unitName,
            string externalActionsExtensionFilename, LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, 
            LGSPSequenceGenerator seqGen, bool isAutoGeneratedFilterExisting, bool isExternalFilterFunctionExisting,
            LGSPGraphStatistics graphStatistics, string statisticsPath,
            SourceBuilder externalSource, SourceBuilder source)
        {
            // analyze the matching patterns, inline the subpatterns when expected to be benefitial
            // the analyzer must be run before the matcher generation
            AnalyzeAndInlineMatchingPatterns((flags & ProcessSpecFlags.Noinline) == 0, ruleAndMatchingPatterns);

            LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(model);
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0) matcherGen.CommentSourceCode = true;
            if((flags & ProcessSpecFlags.LazyNIC) != 0) matcherGen.LazyNegativeIndependentConditionEvaluation = true;
            if((flags & ProcessSpecFlags.Profile) != 0) matcherGen.Profile = true;

            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                GenerateScheduledSearchPlans(matchingPattern.patternGraph, graphStatistics, 
                    matcherGen, !(matchingPattern is LGSPRulePattern), false);

                matcherGen.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(matchingPattern.patternGraph);

                matcherGen.ParallelizeAsNeeded(matchingPattern);

                matcherGen.GenerateActionAndMatcher(source, matchingPattern, true);
            }

            GenerateDefinedSequencesAndFiltersAndFilterStubs(externalActionsExtensionFilename, 
                isAutoGeneratedFilterExisting, isExternalFilterFunctionExisting,
                ruleAndMatchingPatterns, seqGen, externalSource, source);

            // the actions class referencing the generated stuff is generated now into 
            // a source builder which is appended at the end of the other generated stuff
            SourceBuilder endSource = GenerateActionsClass(model, actionsName, unitName,
                statisticsPath, ruleAndMatchingPatterns, 
                matcherGen.LazyNegativeIndependentConditionEvaluation, matcherGen.Profile);
            source.Append(endSource.ToString());
            source.Append("}");
        }

        private static void AnalyzeAndInlineMatchingPatterns(bool inline,
            LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns)
        {
            PatternGraphAnalyzer analyzer = new PatternGraphAnalyzer();
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                analyzer.AnalyzeNestingOfPatternGraph(matchingPattern.patternGraph, false);
                PatternGraphAnalyzer.PrepareInline(matchingPattern.patternGraph);
                analyzer.RememberMatchingPattern(matchingPattern);
            }
            
            analyzer.ComputeInterPatternRelations(false);
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                // computes patternpath information, thus only in original pass
                analyzer.AnalyzeWithInterPatternRelationsKnown(matchingPattern.patternGraph);
            }

            if(inline)
            {
                foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
#if DUMP_PATTERNS
                        // dump patterns for debugging - first original version without inlining
                        SourceBuilder builder = new SourceBuilder(true);
                        matchingPattern.patternGraph.DumpOriginal(builder);
                        StreamWriter writer = new StreamWriter(matchingPattern.name + "_pattern_dump.txt");
                        writer.Write(builder.ToString());
#endif

                    analyzer.InlineSubpatternUsages(matchingPattern.patternGraph);

#if DUMP_PATTERNS
                        // - then inlined version
                        builder = new SourceBuilder(true);
                        matchingPattern.patternGraph.DumpInlined(builder);
                        writer.Write(builder.ToString());
                        writer.Close();
#endif
                }
            }

            // hardcore/ugly parameterization for inlined case, working on inlined members in inlined pass, and original members on original pass
            // working with accessors encapsulating the inlined versions and the original version behind a common interface to keep the analyze code without case distinctions gets terribly extensive
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                matchingPattern.patternGraph.maxIsoSpace = 0; // reset of max iso space for computation of max iso space of inlined patterns
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                analyzer.AnalyzeNestingOfPatternGraph(matchingPattern.patternGraph, true);
            }

            analyzer.ComputeInterPatternRelations(true);
        }

        private static void GenerateDefinedSequencesAndFiltersAndFilterStubs(string externalActionsExtensionFilename, 
            bool isAutoGeneratedFilterExisting, bool isExternalFilterFunctionExisting,
            LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, LGSPSequenceGenerator seqGen,
            SourceBuilder externalSource, SourceBuilder source)
        {
            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                if(sequence is ExternalDefinedSequenceInfo)
                    seqGen.GenerateExternalDefinedSequencePlaceholder(externalSource, (ExternalDefinedSequenceInfo)sequence, externalActionsExtensionFilename);
            }

            if(isAutoGeneratedFilterExisting || isExternalFilterFunctionExisting)
            {
                externalSource.Append("\n");

                if(isExternalFilterFunctionExisting)
                {
                    externalSource.AppendFrontFormat("// You must implement the following filter functions in the same partial class in ./{0}\n", externalActionsExtensionFilename);
                    externalSource.Append("\n");
                    foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                    {
                        seqGen.GenerateFilterStubs(externalSource, rulePattern);
                    }
                }

                if(isAutoGeneratedFilterExisting)
                {
                    if(isExternalFilterFunctionExisting)
                        externalSource.Append("\n").AppendFront("// ------------------------------------------------------\n\n");

                    externalSource.AppendFront("// The following filter functions are automatically generated, you don't need to supply any further implementation\n");
                    externalSource.Append("\n");
                    foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                    {
                        seqGen.GenerateFilters(externalSource, rulePattern);
                    }
                }
            }

            if(externalSource != null)
            {
                externalSource.Append("\n");
                externalSource.AppendFront("// ------------------------------------------------------\n");
            }

            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                if(sequence is ExternalDefinedSequenceInfo)
                    seqGen.GenerateExternalDefinedSequence(externalSource, (ExternalDefinedSequenceInfo)sequence);
                else
                    seqGen.GenerateDefinedSequence(source, sequence);
            }
        }

        private SourceBuilder GenerateActionsClass(IGraphModel model, String actionsName, String unitName,
            string statisticsPath, LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, 
            bool lazyNIC, bool profile)
        {
            SourceBuilder endSource = new SourceBuilder("\n");
            endSource.Indent();
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
            {
                endSource.AppendFront("// class which instantiates and stores all the compiled actions of the module,\n");
                endSource.AppendFront("// dynamic regeneration and compilation causes the old action to be overwritten by the new one\n");
                endSource.AppendFront("// matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available\n");
            }
            endSource.AppendFront("public class " + unitName + "Actions : GRGEN_LGSP.LGSPActions\n");
            endSource.AppendFront("{\n");
            endSource.Indent();
            endSource.AppendFront("public " + unitName + "Actions(GRGEN_LGSP.LGSPGraph lgspgraph, "
                    + "string modelAsmName, string actionsAsmName)\n");
            endSource.AppendFront("    : base(lgspgraph, modelAsmName, actionsAsmName)\n");
            endSource.AppendFront("{\n");
            endSource.AppendFront("    InitActions();\n");
            endSource.AppendFront("}\n\n");
            endSource.AppendFront("public " + unitName + "Actions(GRGEN_LGSP.LGSPGraph lgspgraph)\n");
            endSource.AppendFront("    : base(lgspgraph)\n");
            endSource.AppendFront("{\n");
            endSource.AppendFront("    InitActions();\n");
            endSource.AppendFront("}\n\n");
            endSource.AppendFront("private void InitActions()\n");
            endSource.AppendFront("{\n");
            endSource.Indent();

            endSource.AppendFrontFormat("packages = new string[{0}];\n", ruleAndMatchingPatterns.Packages.Length);
            for(int i=0; i<ruleAndMatchingPatterns.Packages.Length; ++i)
            {
                String packageName = ruleAndMatchingPatterns.Packages[i];
                endSource.AppendFrontFormat("packages[{0}] = \"{1}\";\n", i, packageName);
            }

            // we generate analyzer calls, so the runtime structures needed for dynamic matcher (re-)generation
            // are prepared in the same way as the compile time structures were by the analyzer calls above
            endSource.AppendFront("GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();\n");

            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Rule_{0}.Instance.patternGraph, false);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("analyzer.RememberMatchingPattern({1}Rule_{0}.Instance);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("actions.Add(\"{2}\", (GRGEN_LGSP.LGSPAction) "
                            + "{1}Action_{0}.Instance);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                            matchingPattern.PatternGraph.PackagePrefixedName);

                    endSource.AppendFrontFormat("@{2} = {1}Action_{0}.Instance;\n",
                        matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                        TypesHelper.PackagePrefixedNameUnderscore(matchingPattern.PatternGraph.Package, matchingPattern.PatternGraph.Name));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Pattern_{0}.Instance.patternGraph, false);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("analyzer.RememberMatchingPattern({1}Pattern_{0}.Instance);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }

            endSource.AppendFront("analyzer.ComputeInterPatternRelations(false);\n");
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.InlineSubpatternUsages({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.InlineSubpatternUsages({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("{1}Rule_{0}.Instance.patternGraph.maxIsoSpace = 0;\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("{1}Pattern_{0}.Instance.patternGraph.maxIsoSpace = 0;\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Rule_{0}.Instance.patternGraph, true);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Pattern_{0}.Instance.patternGraph, true);\n",
                            matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            endSource.AppendFront("analyzer.ComputeInterPatternRelations(true);\n");

            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                endSource.AppendFrontFormat("RegisterGraphRewriteSequenceDefinition("
                        + "{1}Sequence_{0}.Instance);\n", sequence.Name, 
                        TypesHelper.GetPackagePrefixDot(sequence.Package));

                endSource.AppendFrontFormat("@{2} = {1}Sequence_{0}.Instance;\n", sequence.Name, 
                    TypesHelper.GetPackagePrefixDot(sequence.Package),
                    TypesHelper.PackagePrefixedNameUnderscore(sequence.Package, sequence.Name));
            }

            foreach(FunctionInfo function in ruleAndMatchingPatterns.Functions)
            {
                endSource.AppendFrontFormat("namesToFunctionDefinitions.Add(\"{2}\", {1}FunctionInfo_{0}.Instance);\n",
                    function.name, TypesHelper.GetPackagePrefixDot(function.package), function.packagePrefixedName);
            }

            foreach(ProcedureInfo procedure in ruleAndMatchingPatterns.Procedures)
            {
                endSource.AppendFrontFormat("namesToProcedureDefinitions.Add(\"{2}\", {1}ProcedureInfo_{0}.Instance);\n",
                    procedure.name, TypesHelper.GetPackagePrefixDot(procedure.package), procedure.packagePrefixedName);
            }

            endSource.Unindent();
            endSource.AppendFront("}\n");
            endSource.AppendFront("\n");

            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                    endSource.AppendFrontFormat("public {1}IAction_{0} @{2};\n",
                        matchingPattern.name, TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                        TypesHelper.PackagePrefixedNameUnderscore(matchingPattern.PatternGraph.Package, matchingPattern.PatternGraph.Name));
            }
            endSource.AppendFront("\n");

            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                endSource.AppendFrontFormat("public {1}Sequence_{0} @{2};\n",
                    sequence.Name, TypesHelper.GetPackagePrefixDot(sequence.Package),
                    TypesHelper.PackagePrefixedNameUnderscore(sequence.Package, sequence.Name));
            }
            endSource.AppendFront("\n");

            endSource.AppendFront("public override string[] Packages { get { return packages; } }\n");
            endSource.AppendFront("private string[] packages;\n");
            endSource.AppendFront("\n");

            endSource.AppendFront("public override string Name { get { return \"" + actionsName + "\"; } }\n");
            endSource.AppendFront("public override string StatisticsPath { get { return " + (statisticsPath != null ? "@\"" + statisticsPath + "\"" : "null") + "; } }\n");
            endSource.AppendFront("public override bool LazyNIC { get { return " + (lazyNIC ? "true" : "false") + "; } }\n");
            endSource.AppendFront("public override bool Profile { get { return " + (profile ? "true" : "false") + "; } }\n\n");
            endSource.AppendFront("public override string ModelMD5Hash { get { return \"" + model.MD5Hash + "\"; } }\n");
            endSource.Unindent();
            endSource.AppendFront("}\n");
            return endSource;
        }

        private ErrorType GenerateModelAndIntermediateActions(CompileConfiguration cc)
        {
            if((flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
            {
                List<String> genModelFiles, genModelStubFiles, genActionsFiles;

                if(!ExecuteGrGenJava(cc.tmpDir, flags,
                    out genModelFiles, out genModelStubFiles,
                    out genActionsFiles, cc.specFile))
                {
                    return ErrorType.GrGenJavaError;
                }

                if(genModelFiles.Count == 1) cc.modelFilename = genModelFiles[0];
                else if(genModelFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple models are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genModelStubFiles.Count == 1) cc.modelStubFilename = genModelStubFiles[0];
                else if(genModelStubFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple model stubs are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genActionsFiles.Count == 1) cc.actionsFilename = genActionsFiles[0];
                else if(genActionsFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple action sets are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }
            }
            else
            {
                String[] producedFiles = Directory.GetFiles(cc.tmpDir);
                foreach(String file in producedFiles)
                {
                    if(file.EndsWith("Model.cs"))
                    {
                        if(cc.modelFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(cc.modelFilename))
                            cc.modelFilename = file;
                    }
                    else if(file.EndsWith("Actions_intermediate.cs"))
                    {
                        if(cc.actionsFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(cc.actionsFilename))
                            cc.actionsFilename = file;
                    }
                    else if(file.EndsWith("ModelStub.cs"))
                    {
                        if(cc.modelStubFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(cc.modelStubFilename))
                            cc.modelStubFilename = file;
                    }
                }
            }

            if(cc.modelFilename == null || cc.actionsFilename == null)
            {
                Console.Error.WriteLine("Not all required files have been generated!");
                return ErrorType.GrGenJavaError;
            }

            return ErrorType.NoError;
        }

        private static ErrorType ReadExternalActionExtensionSources(string externalActionsExtensionOutputFilename, string externalActionsExtensionFilename,
            out String externalActionsExtensionOutputSource, out String externalActionsExtensionSource)
        {
            externalActionsExtensionOutputSource = null;
            externalActionsExtensionSource = null;
            
            try
            {
                using(StreamReader reader = new StreamReader(externalActionsExtensionOutputFilename))
                    externalActionsExtensionOutputSource = reader.ReadToEnd();
            }
            catch(Exception)
            {
                Console.Error.WriteLine("Unable to read from file \"" + externalActionsExtensionOutputFilename + "\"!");
                return ErrorType.GrGenNetError;
            }

            if(externalActionsExtensionFilename != null)
            {
                try
                {
                    using(StreamReader reader = new StreamReader(externalActionsExtensionFilename))
                        externalActionsExtensionSource = reader.ReadToEnd();
                }
                catch(Exception)
                {
                    Console.Error.WriteLine("Unable to read from file \"" + externalActionsExtensionFilename + "\"!");
                    return ErrorType.GrGenNetError;
                }
            }

            return ErrorType.NoError;
        }

        private String WriteSourceAndExternalSource(SourceBuilder externalSource, SourceBuilder source,
            String actionsOutputFilename, string externalActionsExtensionOutputFilename)
        {
            String actionsOutputSource;
            actionsOutputSource = source.ToString();

            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
            {
                StreamWriter writer = new StreamWriter(actionsOutputFilename);
                writer.Write(actionsOutputSource);
                writer.Close();
            }

            if(externalSource != null)
            {
                externalSource.Unindent();
                externalSource.AppendFront("}\n");
                StreamWriter writer = new StreamWriter(externalActionsExtensionOutputFilename);
                writer.Write(externalSource.ToString());
                writer.Close();
            }
            return actionsOutputSource;
        }

        private static ErrorType CopyIntermediateCodeInsertingSequencesCode(String actionsFilename,
            Dictionary<String, Type> actionTypes, Dictionary<String, Type> proceduresTypes, 
            LGSPSequenceGenerator seqGen, SourceBuilder source, 
            out bool actionPointFound, out String actionsNamespace)
        {
            actionPointFound = false;
            actionsNamespace = null;
            using(StreamReader sr = new StreamReader(actionsFilename))
            {
                String line;
                while(!actionPointFound && (line = sr.ReadLine()) != null)
                {
                    if(actionsNamespace == null && line.StartsWith("namespace "))
                    {
                        actionsNamespace = line.Substring("namespace ".Length);
                        source.Append(line);
                        source.Append("\n");
                    }
                    else if(line.Length > 0 && line[0] == '#' 
                        && line.Contains("// GrGen imperative statement section")
                        && seqGen != null)
                    {
                        int lastSpace = line.LastIndexOf(' ');
                        String ruleName = line.Substring(lastSpace + 1);
                        Type ruleType = actionTypes[ruleName];
                        FieldInfo[] ruleFields = ruleType.GetFields();
                        for(int i = 0; i < ruleFields.Length; ++i)
                        {
                            if(ruleFields[i].Name.StartsWith("XGRSInfo_") && ruleFields[i].FieldType == typeof(EmbeddedSequenceInfo))
                            {
                                EmbeddedSequenceInfo xgrsInfo = (EmbeddedSequenceInfo)ruleFields[i].GetValue(null);
                                if(!seqGen.GenerateXGRSCode(ruleFields[i].Name.Substring("XGRSInfo_".Length), xgrsInfo.Package,
                                    xgrsInfo.XGRS, xgrsInfo.Parameters, xgrsInfo.ParameterTypes,
                                    xgrsInfo.OutParameters, xgrsInfo.OutParameterTypes, source, xgrsInfo.LineNr))
                                {
                                    return ErrorType.GrGenNetError;
                                }
                            }
                        }
                        while((line = sr.ReadLine()) != null)
                        {
                            if(line.StartsWith("#"))
                                break;
                        }
                    }
                    else if(line.Length > 0 && line[0] == '#'
                        && line.Contains("// GrGen procedure exec section")
                        && seqGen != null)
                    {
                        int lastSpace = line.LastIndexOf(' ');
                        String proceduresName = line.Substring(lastSpace + 1);
                        FieldInfo[] procedureFields = proceduresTypes[proceduresName].GetFields();
                        for(int i = 0; i < procedureFields.Length; ++i)
                        {
                            if(procedureFields[i].Name.StartsWith("XGRSInfo_") && procedureFields[i].FieldType == typeof(EmbeddedSequenceInfo))
                            {
                                EmbeddedSequenceInfo xgrsInfo = (EmbeddedSequenceInfo)procedureFields[i].GetValue(null);
                                if(!seqGen.GenerateXGRSCode(procedureFields[i].Name.Substring("XGRSInfo_".Length), xgrsInfo.Package,
                                    xgrsInfo.XGRS, xgrsInfo.Parameters, xgrsInfo.ParameterTypes,
                                    xgrsInfo.OutParameters, xgrsInfo.OutParameterTypes, source, xgrsInfo.LineNr))
                                {
                                    return ErrorType.GrGenNetError;
                                }
                            }
                        }
                        while((line = sr.ReadLine()) != null)
                        {
                            if(line.StartsWith("#"))
                                break;
                        }
                    }
                    else if(line.Length > 0 && line[0] == '/' && line.StartsWith("// GrGen insert Actions here"))
                    {
                        actionPointFound = true;
                        break;
                    }
                    else
                    {
                        source.Append(line);
                        source.Append("\n");
                    }
                }
            }

            return ErrorType.NoError;
        }

        private void EmitExternalActionsFileHeader(CompileConfiguration cc, IGraphModel model, bool implementationNeeded,
            ref SourceBuilder externalSource)
        {
            cc.externalActionsExtensionOutputFilename = cc.destDir + cc.actionsName + "ExternalFunctions.cs";
            externalSource = new SourceBuilder((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0);

            // generate external action extension file header
            externalSource.AppendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n");
            externalSource.AppendFront("// Do not modify this file! Any changes will be lost!\n");
            externalSource.AppendFrontFormat("// Generated from \"{0}.grg\" on {1} {2}\n", cc.baseName, DateTime.Now.ToString(), System.TimeZone.CurrentTimeZone.StandardName);

            externalSource.AppendFront("using System;\n");
            externalSource.AppendFront("using System.Collections.Generic;\n");
            externalSource.AppendFront("using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n");
            externalSource.AppendFront("using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n");
            externalSource.AppendFrontFormat("using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.ModelName + ";\n");
            externalSource.AppendFrontFormat("using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_" + cc.baseName + ";\n");

            externalSource.AppendFront("\nnamespace de.unika.ipd.grGen.Action_" + cc.baseName + "\n");
            externalSource.AppendFront("{");
            externalSource.Indent();

            if(implementationNeeded) // not needed if only generated filters exist, then the generated file is sufficient
                cc.externalActionsExtensionFilename = cc.destDir + cc.actionsName + "ExternalFunctionsImpl.cs";
        }

        private static void DetermineWhetherExternalActionsFileIsNeeded(LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns,
            out bool isAutoGeneratedFilterExisting, out bool isExternalFilterFunctionExisting, out bool isExternalSequenceExisting)
        {
            isAutoGeneratedFilterExisting = false;
            isExternalFilterFunctionExisting = false;
            foreach(IRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
            {
                foreach(IFilter filter in rulePattern.Filters)
                {
                    if(filter is IFilterAutoGenerated)
                        isAutoGeneratedFilterExisting = true;
                    else if(((IFilterFunction)filter).IsExternal)
                        isExternalFilterFunctionExisting = true;
                }
            }

            isExternalSequenceExisting = false;
            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                if(sequence is ExternalDefinedSequenceInfo)
                {
                    isExternalSequenceExisting = true;
                    break;
                }
            }
        }

        public static bool IsGeneratedFilter(string filter, IRulePattern rulePattern)
        {
            if(filter.IndexOf('_')!=-1)
            {
                string filterBase = filter.Substring(0, filter.IndexOf('_'));
                string filterVariable = filter.Substring(filter.IndexOf('_') + 1);
                if(filterBase=="orderAscendingBy" || filterBase=="orderDescendingBy"
                    || filterBase=="groupBy" || filterBase=="keepSameAsFirst"
                    || filterBase=="keepSameAsLast" || filterBase=="keepOneForEach")
                {
                    if(IsFilterVariable(filterVariable, rulePattern))
                        return true;
                }
            }
            if(filter == "auto")
                return true;
            return false;
        }

        private static bool IsFilterVariable(string variable, IRulePattern rulePattern)
        {
            foreach(IPatternVariable patternVariable in rulePattern.PatternGraph.Variables)
            {
                if(patternVariable.UnprefixedName == variable)
                    return true;
            }
            return false;
        }

        private static void CollectActionParameterTypes(LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, IGraphModel model,
            out Dictionary<String, List<IFilter>> rulesToFilters, out Dictionary<String, List<String>> filterFunctionsToInputTypes,
            out Dictionary<String, List<String>> rulesToInputTypes, out Dictionary<String, List<String>> rulesToOutputTypes,
            out Dictionary<String, List<String>> rulesToTopLevelEntities, out Dictionary<String, List<String>> rulesToTopLevelEntityTypes,
            out Dictionary<String, List<String>> sequencesToInputTypes, out Dictionary<String, List<String>> sequencesToOutputTypes,
            out Dictionary<String, List<String>> proceduresToInputTypes, out Dictionary<String, List<String>> proceduresToOutputTypes,
            out Dictionary<String, List<String>> functionsToInputTypes, out Dictionary<String, String> functionsToOutputType)
        {
            rulesToFilters = new Dictionary<String, List<IFilter>>();
            filterFunctionsToInputTypes = new Dictionary<String, List<String>>();
            
            rulesToInputTypes = new Dictionary<String, List<String>>();
            rulesToOutputTypes = new Dictionary<String, List<String>>();
                        
            rulesToTopLevelEntities = new Dictionary<String, List<String>>();
            rulesToTopLevelEntityTypes = new Dictionary<String, List<String>>();
            
            sequencesToInputTypes = new Dictionary<String, List<String>>();
            sequencesToOutputTypes = new Dictionary<String, List<String>>();

            proceduresToInputTypes = new Dictionary<String, List<String>>();
            proceduresToOutputTypes = new Dictionary<String, List<String>>();

            functionsToInputTypes = new Dictionary<String, List<String>>();
            functionsToOutputType = new Dictionary<String, String>();

            foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
            {
                List<IFilter> filters = new List<IFilter>();
                rulesToFilters.Add(rulePattern.PatternGraph.PackagePrefixedName, filters);
                foreach(IFilter filter in rulePattern.Filters)
                {
                    filters.Add(filter);

                    if(filter is IFilterFunction)
                    {
                        IFilterFunction filterFunction = (IFilterFunction)filter;
                        List<String> filterFunctionInputTypes = new List<String>();
                        filterFunctionsToInputTypes.Add(filterFunction.PackagePrefixedName, filterFunctionInputTypes);
                        foreach(GrGenType inputType in filterFunction.Inputs)
                        {
                            filterFunctionInputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                        }
                    }
                }

                List<String> inputTypes = new List<String>();
                rulesToInputTypes.Add(rulePattern.PatternGraph.PackagePrefixedName, inputTypes);
                foreach(GrGenType inputType in rulePattern.Inputs)
                {
                    inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                }
                
                List<String> outputTypes = new List<String>();
                rulesToOutputTypes.Add(rulePattern.PatternGraph.PackagePrefixedName, outputTypes);
                foreach(GrGenType outputType in rulePattern.Outputs)
                {
                    outputTypes.Add(TypesHelper.DotNetTypeToXgrsType(outputType));
                }
                
                List<String> topLevelEntities = new List<String>();
                rulesToTopLevelEntities.Add(rulePattern.PatternGraph.PackagePrefixedName, topLevelEntities);
                foreach(IPatternNode node in rulePattern.PatternGraph.Nodes)
                {
                    topLevelEntities.Add(node.UnprefixedName);
                }
                foreach(IPatternEdge edge in rulePattern.PatternGraph.Edges)
                {
                    topLevelEntities.Add(edge.UnprefixedName);
                }
                foreach(IPatternVariable var in rulePattern.PatternGraph.Variables)
                {
                    topLevelEntities.Add(var.UnprefixedName);
                }
                
                List<String> topLevelEntityTypes = new List<String>();
                rulesToTopLevelEntityTypes.Add(rulePattern.PatternGraph.PackagePrefixedName, topLevelEntityTypes);
                foreach(IPatternNode node in rulePattern.PatternGraph.Nodes)
                {
                    topLevelEntityTypes.Add(TypesHelper.DotNetTypeToXgrsType(node.Type));
                }
                foreach(IPatternEdge edge in rulePattern.PatternGraph.Edges)
                {
                    topLevelEntityTypes.Add(TypesHelper.DotNetTypeToXgrsType(edge.Type));
                }
                foreach(IPatternVariable var in rulePattern.PatternGraph.Variables)
                {
                    topLevelEntityTypes.Add(TypesHelper.DotNetTypeToXgrsType(var.Type));
                }
            }
            
            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                List<String> inputTypes = new List<String>();
                sequencesToInputTypes.Add(sequence.PackagePrefixedName, inputTypes);
                foreach(GrGenType inputType in sequence.ParameterTypes)
                {
                    inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                }

                List<String> outputTypes = new List<String>();
                sequencesToOutputTypes.Add(sequence.PackagePrefixedName, outputTypes);
                foreach(GrGenType outputType in sequence.OutParameterTypes)
                {
                    outputTypes.Add(TypesHelper.DotNetTypeToXgrsType(outputType));
                }
            }

            foreach(ProcedureInfo procedure in ruleAndMatchingPatterns.Procedures)
            {
                List<String> inputTypes = new List<String>();
                proceduresToInputTypes.Add(procedure.packagePrefixedName, inputTypes);
                foreach(GrGenType inputType in procedure.inputs)
                {
                    inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                }

                List<String> outputTypes = new List<String>();
                proceduresToOutputTypes.Add(procedure.packagePrefixedName, outputTypes);
                foreach(GrGenType outputType in procedure.outputs)
                {
                    outputTypes.Add(TypesHelper.DotNetTypeToXgrsType(outputType));
                }
            }

            foreach(FunctionInfo function in ruleAndMatchingPatterns.Functions)
            {
                List<String> inputTypes = new List<String>();
                functionsToInputTypes.Add(function.packagePrefixedName, inputTypes);
                foreach(GrGenType inputType in function.inputs)
                {
                    inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                }

                functionsToOutputType.Add(function.packagePrefixedName, TypesHelper.DotNetTypeToXgrsType(function.output));
            }
        }

        private static void CollectActionTypes(Assembly initialAssembly, out Dictionary<String, Type> actionTypes,
            out Dictionary<String, Type> proceduresTypes, out LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns)
        {
            actionTypes = new Dictionary<string, Type>();
            proceduresTypes = new Dictionary<String, Type>();

            foreach(Type type in initialAssembly.GetTypes())
            {
                if(!type.IsClass || type.IsNotPublic) continue;
                if(type.BaseType == typeof(LGSPMatchingPattern) || type.BaseType == typeof(LGSPRulePattern))
                    actionTypes.Add(TypesHelper.GetPackagePrefixedNameFromFullTypeName(type.FullName), type);
                if(type.Name == "Procedures")
                    proceduresTypes.Add(TypesHelper.GetPackagePrefixedNameFromFullTypeName(type.FullName), type);
            }

            ruleAndMatchingPatterns = null;
            foreach(Type type in initialAssembly.GetTypes())
            {
                if(!type.IsClass || type.IsNotPublic) continue;
                if(type.BaseType == typeof(LGSPRuleAndMatchingPatterns))
                {
                    ruleAndMatchingPatterns = (LGSPRuleAndMatchingPatterns)Activator.CreateInstance(type);
                    break;
                }
            }
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="destDir">The directory, where the generated libraries are to be placed.</param>
        /// <param name="intermediateDir">A directory, where intermediate files can be placed.</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <param name="externalAssemblies">External assemblies to reference</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public static void ProcessSpecification(String specPath, String destDir, String intermediateDir, String statisticsPath, ProcessSpecFlags flags, params String[] externalAssemblies)
        {
            ErrorType ret;
            try
            {
                ret = new LGSPGrGen(flags).ProcessSpecificationImpl(specPath, destDir, intermediateDir, statisticsPath, externalAssemblies);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            if(ret != ErrorType.NoError)
            {
                if(ret == ErrorType.GrGenJavaError && File.Exists(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                {
                    using(StreamReader sr = new StreamReader(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                    {
                        String output = sr.ReadToEnd();
                        if(output.Length != 0)
                            throw new Exception("\nError while processing specification:\n" + output);
                    }
                }
                throw new Exception("Error while processing specification!");
            }
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <param name="externalAssemblies">External assemblies to reference</param>
        public static void ProcessSpecification(String specPath, String statisticsPath, 
            ProcessSpecFlags flags, params String[] externalAssemblies)
        {
            specPath = FixDirectorySeparators(specPath);

            String specDir;
            int index = specPath.LastIndexOf(Path.DirectorySeparatorChar);
            if(index == -1)
                specDir = "";
            else
            {
                specDir = specPath.Substring(0, index + 1);
                if(!Directory.Exists(specDir))
                    throw new Exception("Something is wrong with the directory of the specification file:\n\"" + specDir + "\" does not exist!");
            }

            String dirname;
            int id = 0;
            do
            {
                dirname = specDir + "tmpgrgen" + id + "";
                id++;
            }
            while(Directory.Exists(dirname));
            try
            {
                Directory.CreateDirectory(dirname);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to create temporary directory \"" + dirname + "\"!", ex);
            }

            try
            {
                ProcessSpecification(specPath, specDir, dirname, statisticsPath, flags, externalAssemblies);
            }
            finally
            {
                Directory.Delete(dirname, true);
            }
        }
    }
}
