/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define NO_EDGE_LOOKUP

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
        internal bool UsePerfInfo { get { return (flags & ProcessSpecFlags.NoPerformanceInfoUpdates) == 0; } }

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

        bool ProcessModel(String modelFilename, String modelStubFilename, String destDir, String[] externalAssemblies, 
                out Assembly modelAssembly, out String modelAssemblyName)
        {
            String modelName = Path.GetFileNameWithoutExtension(modelFilename);
            String modelExtension = Path.GetExtension(modelFilename);

            modelAssembly = null;
            modelAssemblyName = null;

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();

            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
            compParams.ReferencedAssemblies.AddRange(externalAssemblies);

            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.OutputAssembly = destDir  + "lgsp-" + modelName + ".dll";

            CompilerResults compResults;
            try
            {
                if(File.Exists(modelName + "ExternalFunctions.cs"))
                {
                    String externalFunctionsFile = modelName + "ExternalFunctions.cs";
                    String externalFunctionsImplFile = modelName + "ExternalFunctionsImpl.cs";
                    if(modelStubFilename != null)
                        compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename, modelStubFilename, externalFunctionsFile, externalFunctionsImplFile);
                    else
                        compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename, externalFunctionsFile, externalFunctionsImplFile);
                }
                else
                {
                    if(modelStubFilename != null)
                        compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename, modelStubFilename);
                    else
                        compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename);
                }
                if(compResults.Errors.HasErrors)
                {
                    Console.Error.WriteLine("Illegal model C# source code: " + compResults.Errors.Count + " Errors:");
                    foreach(CompilerError error in compResults.Errors)
                        Console.Error.WriteLine("Line: " + error.Line + " - " + error.ErrorText);
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Unable to compile model: {0}", ex.Message);
                return false;
            }

            modelAssembly = compResults.CompiledAssembly;
            modelAssemblyName = compParams.OutputAssembly;
            AddAssembly(modelAssembly);

            Console.WriteLine(" - Model assembly \"{0}\" generated.", modelAssemblyName);
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
        PlanGraph GenerateStaticPlanGraph(PatternGraph patternGraph, int index,
            bool isNegativeOrIndependent, bool isSubpattern)
        {
            //
            // If you change this method, chances are high you also want to change GeneratePlanGraph in LGSPMatcherGenerator
            // todo: unify it with GeneratePlanGraph in LGSPMatcherGenerator
            //

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
            // Create "pick from storage" plan graph edge for plan graph nodes which are to picked from a storage,
            //     from root node on, no lookup here, no other plan graph edge having this node as target
            // Create "pick from storage attribute" plan graph edge from storage attribute owner to storage picking result,
            //     no lookup, no other plan graph edge having this node as target
            // Create "map" by storage plan graph edge from accessor to storage mapping result
            //     no lookup, no other plan graph edge having this node as target
            // Create "cast" plan graph edge from element before casting to cast result,
            //     no lookup, no other plan graph edge having this node as target

            PlanNode[] planNodes = new PlanNode[patternGraph.nodes.Length + patternGraph.edges.Length];
            List<PlanEdge> planEdges = new List<PlanEdge>(patternGraph.nodes.Length + 5 * patternGraph.edges.Length);   // upper bound for num of edges

            int nodesIndex = 0;

            // create plan nodes and lookup plan edges for all pattern graph nodes
            PlanNode planRoot = new PlanNode("root");
            for(int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];

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
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if (node.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(node.Storage != null)
                {
                    if(node.Accessor != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the accessor is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(node.StorageAttributeOwner != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the attribute owner is needed, so there is no lookup like operation
                }
                else if(node.ElementBeforeCasting != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
                }
                else
                {
                    cost = 2 * node.Cost + 10;
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
            for(int i = 0; i < patternGraph.edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];

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
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if (edge.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(edge.Storage != null)
                {
                    if(edge.Accessor != null)
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the accessor is needed, so there is no lookup like operation
                    }
                    else
                    {
                        cost = 0;
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(edge.StorageAttributeOwner != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the attribute owner is needed, so there is no lookup like operation
                }
                else if(edge.ElementBeforeCasting != null)
                {
                    cost = 0;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
                }
                else
                {
                    cost = 2 * edge.Cost + 10;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    patternGraph.GetSource(edge)!=null ? patternGraph.GetSource(edge).TempPlanMapping : null,
                    patternGraph.GetTarget(edge)!=null ? patternGraph.GetTarget(edge).TempPlanMapping : null);
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

                // only add implicit source operation if edge source is needed and the edge source is not a preset node and not a storage node and not a cast node
                if(patternGraph.GetSource(edge) != null 
                    && !patternGraph.GetSource(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetSource(edge).Storage == null
                    && patternGraph.GetSource(edge).StorageAttributeOwner == null
                    && patternGraph.GetSource(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ? 
                        SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                    PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetSource(edge).TempPlanMapping, 0);
                    planEdges.Add(implSrcPlanEdge);
                    patternGraph.GetSource(edge).TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node and not a storage node and not a cast node
                if(patternGraph.GetTarget(edge) != null
                    && !patternGraph.GetTarget(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetTarget(edge).Storage == null
                    && patternGraph.GetTarget(edge).StorageAttributeOwner == null
                    && patternGraph.GetTarget(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                    PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetTarget(edge).TempPlanMapping, 0);
                    planEdges.Add(implTgtPlanEdge);
                    patternGraph.GetTarget(edge).TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset and not storage determined and not a cast
                if(!isPreset && edge.Storage == null && edge.StorageAttributeOwner == null && edge.ElementBeforeCasting == null)
                {
                    // no outgoing on source node if no source
                    if(patternGraph.GetSource(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Outgoing : SearchOperationType.Incident;
                        PlanEdge outPlanEdge = new PlanEdge(operation, patternGraph.GetSource(edge).TempPlanMapping,
                            planNodes[nodesIndex], edge.Cost);
                        planEdges.Add(outPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(outPlanEdge);
                    }
                    // no incoming on target node if no target
                    if(patternGraph.GetTarget(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Incoming: SearchOperationType.Incident;
                        PlanEdge inPlanEdge = new PlanEdge(operation, patternGraph.GetTarget(edge).TempPlanMapping,
                            planNodes[nodesIndex], edge.Cost);
                        planEdges.Add(inPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(inPlanEdge);
                    }
                }

                edge.TempPlanMapping = planNodes[nodesIndex];
                ++nodesIndex;
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling storage mapping (can't be done in first run due to dependencies between elements)

            // create map with storage plan edges for all pattern graph nodes 
            // which are the result of a mapping/picking from attribute operation or element type casting
            for(int i = 0; i < patternGraph.Nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];
                if(node.PointOfDefinition == patternGraph)
                {
                    if(node.Accessor != null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            node.Accessor.TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(node.StorageAttributeOwner != null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            node.StorageAttributeOwner.TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(node.ElementBeforeCasting != null)
                    {
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            node.ElementBeforeCasting.TempPlanMapping, node.TempPlanMapping, 0);
                        planEdges.Add(castPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                }
            }

            // create map with storage plan edges for all pattern graph edges which are the result of a mapping/picking from attribute operation
            for(int i = 0; i < patternGraph.Edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];
                if(edge.PointOfDefinition == patternGraph)
                {
                    if(edge.Accessor != null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            edge.Accessor.TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(edge.StorageAttribute != null)
                    {
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            edge.StorageAttributeOwner.TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(storAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(edge.ElementBeforeCasting != null)
                    {
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            edge.ElementBeforeCasting.TempPlanMapping, edge.TempPlanMapping, 0);
                        planEdges.Add(castPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                }
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }
       
        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of static schedule information given by rulePattern elements, 
        /// utilizing code of the lgsp matcher generator.
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        protected void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPMatcherGenerator matcherGen,
            bool isSubpattern, bool isNegativeOrIndependent)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                PlanGraph planGraph = GenerateStaticPlanGraph(patternGraph, i,
                    isNegativeOrIndependent, isSubpattern);
                matcherGen.MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
                SearchPlanGraph searchPlanGraph = matcherGen.GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = matcherGen.ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent);
                matcherGen.AppendHomomorphyInformation(scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
                {
                    GenerateScheduledSearchPlans(neg, matcherGen, isSubpattern, true);
                }

                foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
                {
                    GenerateScheduledSearchPlans(idpt, matcherGen, isSubpattern, true);
                }

                foreach(Alternative alt in patternGraph.alternatives)
                {
                    foreach(PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, matcherGen, true, false);
                    }
                }

                foreach(Iterated iter in patternGraph.iterateds)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, matcherGen, true, false);
                }
            }
        }

        public static bool ExecuteGrGenJava(String tmpDir, ProcessSpecFlags flags, out List<String> genModelFiles,
                out List<String> genModelStubFiles, out List<String> genActionsFiles, params String[] sourceFiles)
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

        ErrorType ProcessSpecificationImpl(String specFile, String destDir, String tmpDir, String[] externalAssemblies)
        {
            Console.WriteLine("Building libraries...");

            ///////////////////////////////////////////////
            // use java frontend to build the model and intermediate action source files
            ///////////////////////////////////////////////

            String modelFilename = null;
            String modelStubFilename = null;
            String actionsFilename = null;

            if((flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
            {
                List<String> genModelFiles, genModelStubFiles, genActionsFiles;

                if(!ExecuteGrGenJava(tmpDir, flags, out genModelFiles, out genModelStubFiles,
                        out genActionsFiles, specFile))
                    return ErrorType.GrGenJavaError;

                if(genModelFiles.Count == 1) modelFilename = genModelFiles[0];
                else if(genModelFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple models are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genModelStubFiles.Count == 1) modelStubFilename = genModelStubFiles[0];
                else if(genModelStubFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple model stubs are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genActionsFiles.Count == 1) actionsFilename = genActionsFiles[0];
                else if(genActionsFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple action sets are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }
            }
            else
            {
                String[] producedFiles = Directory.GetFiles(tmpDir);
                foreach(String file in producedFiles)
                {
                    if(file.EndsWith("Model.cs"))
                    {
                        if(modelFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(modelFilename))
                            modelFilename = file;
                    }
                    else if(file.EndsWith("Actions_intermediate.cs"))
                    {
                        if(actionsFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(actionsFilename))
                            actionsFilename = file;
                    }
                    else if(file.EndsWith("ModelStub.cs"))
                    {
                        if(modelStubFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(modelStubFilename))
                            modelStubFilename = file;
                    }
                }
            }

            if(modelFilename == null || actionsFilename == null)
            {
                Console.Error.WriteLine("Not all required files have been generated!");
                return ErrorType.GrGenJavaError;
            }

            ///////////////////////////////////////////////
            // compile the model and intermediate action files generated by the java frontend
            // to gain access via reflection to their content needed for matcher code generation
            ///////////////////////////////////////////////

            Assembly modelAssembly;
            String modelAssemblyName;
            if(!ProcessModel(modelFilename, modelStubFilename, destDir, externalAssemblies, out modelAssembly, out modelAssemblyName))
                return ErrorType.GrGenNetError;

            IGraphModel model = GetGraphModel(modelAssembly);
            if(model == null) return ErrorType.GrGenNetError;

            if((flags & ProcessSpecFlags.NoProcessActions) != 0) return ErrorType.NoError;

            String actionsName = Path.GetFileNameWithoutExtension(actionsFilename);
            actionsName = actionsName.Substring(0, actionsName.Length - 13);    // remove "_intermediate" suffix
            String actionsOutputFilename = tmpDir + Path.DirectorySeparatorChar + actionsName + ".cs";

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);

            String actionsOutputSource;
            if((flags & ProcessSpecFlags.UseExistingMask) != ProcessSpecFlags.UseAllGeneratedFiles)
            {
                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize /d:INITIAL_WARMUP";
                compParams.TreatWarningsAsErrors = false;

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

                Assembly initialAssembly = compResultsWarmup.CompiledAssembly;

                Dictionary<String, Type> actionTypes = new Dictionary<string, Type>();

                foreach(Type type in initialAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.BaseType == typeof(LGSPMatchingPattern) || type.BaseType == typeof(LGSPRulePattern))
                        actionTypes.Add(type.Name, type);
                }

                LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns = null;
                foreach (Type type in initialAssembly.GetTypes())
                {
                    if (!type.IsClass || type.IsNotPublic) continue;
                    if (type.BaseType == typeof(LGSPRuleAndMatchingPatterns))
                    {
                        ruleAndMatchingPatterns = (LGSPRuleAndMatchingPatterns)Activator.CreateInstance(type);
                        break;
                    }
                }

                Dictionary<String, List<String>> rulesToInputTypes = new Dictionary<String, List<String>>();
                Dictionary<String, List<String>> rulesToOutputTypes = new Dictionary<String, List<String>>();
                Dictionary<String, List<String>> sequencesToInputTypes = new Dictionary<String, List<String>>();
                Dictionary<String, List<String>> sequencesToOutputTypes = new Dictionary<String, List<String>>();
                foreach (IRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                {
                    List<String> inputTypes = new List<String>();
                    rulesToInputTypes.Add(rulePattern.PatternGraph.Name, inputTypes);
                    foreach (GrGenType inputType in rulePattern.Inputs)
                    {
                        inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                    }
                    List<String> outputTypes = new List<String>();
                    rulesToOutputTypes.Add(rulePattern.PatternGraph.Name, outputTypes);
                    foreach (GrGenType outputType in rulePattern.Outputs)
                    {
                        outputTypes.Add(TypesHelper.DotNetTypeToXgrsType(outputType));
                    }
                }
                foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
                {
                    List<String> inputTypes = new List<String>();
                    sequencesToInputTypes.Add(sequence.Name, inputTypes);
                    foreach(GrGenType inputType in sequence.ParameterTypes)
                    {
                        inputTypes.Add(TypesHelper.DotNetTypeToXgrsType(inputType));
                    }
                    List<String> outputTypes = new List<String>();
                    sequencesToOutputTypes.Add(sequence.Name, outputTypes);
                    foreach(GrGenType outputType in sequence.OutParameterTypes)
                    {
                        outputTypes.Add(TypesHelper.DotNetTypeToXgrsType(outputType));
                    }
                }
                LGSPSequenceGenerator seqGen = new LGSPSequenceGenerator(this, model,
                    rulesToInputTypes, rulesToOutputTypes,
                    sequencesToInputTypes, sequencesToOutputTypes);

                ///////////////////////////////////////////////
                // take action intermediate file until action insertion point as base for action file 
                ///////////////////////////////////////////////

                SourceBuilder source = new SourceBuilder((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0);
                source.Indent();
                source.Indent();
                bool actionPointFound = false;
                String actionsNamespace = null;
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
                        else if(line.Length > 0 && line[0] == '#' && line.Contains("// GrGen imperative statement section"))
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
                                    if(!seqGen.GenerateXGRSCode(ruleFields[i].Name.Substring("XGRSInfo_".Length),
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
                ///////////////////////////////////////////////

                String unitName;
                int lastDot = actionsNamespace.LastIndexOf(".");
                if(lastDot == -1) unitName = "";
                else unitName = actionsNamespace.Substring(lastDot + 8);  // skip ".Action_"

                // the actions class referencing the generated stuff is generated now into 
                // a different source builder which is appended at the end of the other generated stuff
                SourceBuilder endSource = new SourceBuilder("\n");
                endSource.Indent();
                if ((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
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

                PatternGraphAnalyzer analyzer = new PatternGraphAnalyzer();
                LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(model);
                if ((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0) matcherGen.CommentSourceCode = true;
                if ((flags & ProcessSpecFlags.LazyNIC) != 0) matcherGen.LazyNegativeIndependentConditionEvaluation = true;

                foreach (LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
                    analyzer.AnalyzeNestingOfAndRemember(matchingPattern);
                }
                analyzer.ComputeInterPatternRelations();
                foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
                    analyzer.AnalyzeWithInterPatternRelationsKnown(matchingPattern);
                }

                endSource.AppendFront("GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();\n");
                foreach (LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
                    GenerateScheduledSearchPlans(matchingPattern.patternGraph, matcherGen, !(matchingPattern is LGSPRulePattern), false);

                    matcherGen.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(matchingPattern.patternGraph);

                    matcherGen.GenerateActionAndMatcher(source, matchingPattern, true);

                    if (matchingPattern is LGSPRulePattern) // normal rule
                    {
                        endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfAndRemember(Rule_{0}.Instance);\n",
                                matchingPattern.name);
                        endSource.AppendFrontFormat("actions.Add(\"{0}\", (GRGEN_LGSP.LGSPAction) "
                                + "Action_{0}.Instance);\n", matchingPattern.name);

                        endSource.AppendFrontFormat("@{0} = Action_{0}.Instance;\n", matchingPattern.name);
                    }
                    else
                    {
                        endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfAndRemember(Pattern_{0}.Instance);\n",
                                matchingPattern.name);
                    }
                }
                endSource.AppendFront("analyzer.ComputeInterPatternRelations();\n");
                foreach (LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
                    if (matchingPattern is LGSPRulePattern) // normal rule
                    {
                        endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_{0}.Instance);\n",
                                matchingPattern.name);
                    }
                    else
                    {
                        endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_{0}.Instance);\n",
                                matchingPattern.name);
                    }
                }

                foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
                {
                    seqGen.GenerateDefinedSequences(source, sequence);

                    endSource.AppendFrontFormat("RegisterGraphRewriteSequenceDefinition("
                            + "Sequence_{0}.Instance);\n", sequence.Name);

                    endSource.AppendFrontFormat("@{0} = Sequence_{0}.Instance;\n", sequence.Name);
                }

                endSource.Unindent();
                endSource.AppendFront("}\n");
                endSource.AppendFront("\n");

                foreach (LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
                {
                    if (matchingPattern is LGSPRulePattern) // normal rule
                    {
                        endSource.AppendFrontFormat("public IAction_{0} @{0};\n", matchingPattern.name);
                    }
                }
                endSource.AppendFront("\n");
                
                foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
                {
                    endSource.AppendFrontFormat("public Sequence_{0} @{0};\n", sequence.Name);
                }
                endSource.AppendFront("\n");

                endSource.AppendFront("public override string Name { get { return \"" + actionsName + "\"; } }\n");
                endSource.AppendFront("public override string ModelMD5Hash { get { return \"" + model.MD5Hash + "\"; } }\n");
                endSource.Unindent();
                endSource.AppendFront("}\n");
                source.Append(endSource.ToString());
                source.Append("}");

                actionsOutputSource = source.ToString();

                if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
                {
                    StreamWriter writer = new StreamWriter(actionsOutputFilename);
                    writer.Write(actionsOutputSource);
                    writer.Close();
                }
            }
            else
            {
                try
                {
                    using(StreamReader reader = new StreamReader(actionsOutputFilename))
                        actionsOutputSource = reader.ReadToEnd();
                }
                catch(Exception)
                {
                    Console.Error.WriteLine("Unable to read from file \"" + actionsOutputFilename + "\"!");
                    return ErrorType.GrGenNetError;
                }
            }

            if((flags & ProcessSpecFlags.NoCreateActionsAssembly) != 0) return ErrorType.NoError;

            ///////////////////////////////////////////////
            // finally compile the action source file into action assembly
            ///////////////////////////////////////////////
            // action source file was built this way:
            // the rule pattern code was copied from the action intermediate file, 
            // action code was appended by matcher generation,
            // which needed access to the rule pattern objects, 
            // given via reflection of the compiled action intermediate file)
            ///////////////////////////////////////////////

            compParams.GenerateInMemory = false;
            compParams.IncludeDebugInformation = (flags & ProcessSpecFlags.CompileWithDebug) != 0;
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = destDir + "lgsp-" + actionsName + ".dll";

            CompilerResults compResults;
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
            {
                compResults = compiler.CompileAssemblyFromFile(compParams, actionsOutputFilename);
            }
            else
            {
                compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource);
            }
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                Console.Error.WriteLine("Illegal generated actions C# source code: " + errorMsg);
                return ErrorType.GrGenNetError;
            }

            Console.WriteLine(" - Actions assembly \"{0}\" generated.", compParams.OutputAssembly);
            return ErrorType.NoError;
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="destDir">The directory, where the generated libraries are to be placed.</param>
        /// <param name="intermediateDir">A directory, where intermediate files can be placed.</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <param name="externalAssemblies">External assemblies to reference</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public static void ProcessSpecification(String specPath, String destDir, String intermediateDir, ProcessSpecFlags flags, params String[] externalAssemblies)
        {
            ErrorType ret;
            try
            {
                ret = new LGSPGrGen(flags).ProcessSpecificationImpl(specPath, destDir, intermediateDir, externalAssemblies);
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
                            throw new Exception("Error while processing specification:\n" + output);
                    }
                }
                throw new Exception("Error while processing specification!");
            }
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <param name="externalAssemblies">External assemblies to reference</param>
        public static void ProcessSpecification(String specPath, ProcessSpecFlags flags, params String[] externalAssemblies)
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
                ProcessSpecification(specPath, specDir, dirname, flags, externalAssemblies);
            }
            finally
            {
                Directory.Delete(dirname, true);
            }
        }
    }
}
