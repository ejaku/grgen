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
    public class LGSPStaticScheduleInfo
    {
        public String ActionName;
        public LGSPRulePattern RulePattern;

        // Indices according to the pattern element enums
        public float[] NodeCost;
        public float[] EdgeCost;

        public float[][] NegNodeCost;
        public float[][] NegEdgeCost;
    }
    
    public class LGSPGrGen
    {
        class SourceBuilder
        {
            StringBuilder builder;
            String indentation = "";

            public SourceBuilder(String str)
            {
                builder = new StringBuilder(str);
            }

            public SourceBuilder Append(String str)
            {
                builder.Append(str);
                return this;
            }

            public SourceBuilder AppendFormat(String str, params object[] args)
            {
                builder.AppendFormat(str, args);
                return this;
            }

            public SourceBuilder AppendFront(String str)
            {
                builder.Append(indentation);
                builder.Append(str);
                return this;
            }

            public SourceBuilder AppendFrontFormat(String str, params object[] args)
            {
                builder.Append(indentation);
                builder.AppendFormat(str, args);
                return this;
            }

            public void Indent()
            {
                indentation += "    ";
            }

            public void Unindent()
            {
                indentation = indentation.Substring(4);
            }

            public override String ToString()
            {
                return builder.ToString();
            }
        }

        private Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        private bool assemblyHandlerInstalled = false;

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

        bool ProcessModel(String modelFilename, String destDir, bool compileWithDebug, out Assembly modelAssembly, out String modelAssemblyName)
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

            compParams.CompilerOptions = compileWithDebug ? "/debug" : "/optimize";
            compParams.OutputAssembly = destDir  + "lgsp-" + modelName + ".dll";

            CompilerResults compResults;
            try
            {
                compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename);
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
                    if(type.GetInterface("IGraphModel") != null)
                    {
                        if(modelType != null)
                        {
                            Console.Error.WriteLine("The given model contains more than one IModelDescription implementation!");
                            return null;
                        }
                        modelType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                Console.WriteLine(e.Message);
            }
            if(modelType == null)
            {
                Console.Error.WriteLine("The given model doesn't contain an IModelDescription implementation!");
                return null;
            }

            return (IGraphModel) modelAssembly.CreateInstance(modelType.FullName);
        }

        PlanGraph GenerateStaticPlanGraph(PatternGraph patternGraph, float[] nodeCost, float[] edgeCost, bool negPatternGraph)
        {
            PlanNode[] nodes = new PlanNode[patternGraph.nodes.Length + patternGraph.edges.Length];
            List<PlanEdge> edges = new List<PlanEdge>(patternGraph.nodes.Length + 5 * patternGraph.edges.Length);   // upper bound for num of edges

            int nodesIndex = 0;

            PlanNode root = new PlanNode("root");
            for(int i = 0; i < patternGraph.nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.PatternElementType == PatternElementType.Preset)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negPatternGraph && node.PatternElementType == PatternElementType.Normal)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
                    cost = 2 *nodeCost[i] + 10;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                node.TempPlanMapping = nodes[nodesIndex];
                nodesIndex++;
            }

            for(int i = 0; i < patternGraph.edges.Length; i++)
            {
                PatternEdge edge = patternGraph.edges[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negPatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
                    cost = 2 * edgeCost[i] + 10;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }

                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
#if NO_EDGE_LOOKUP
                if(isPreset)
                {
#endif
                    PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                    edges.Add(rootToNodeEdge);
                    nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
#if NO_EDGE_LOOKUP
                }
#endif


                if(edge.source != null && !edge.source.TempPlanMapping.IsPreset)
                {
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 0);
                    edges.Add(implSrcEdge);
                    edge.source.TempPlanMapping.IncomingEdges.Add(implSrcEdge);
                }
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 0);
                    edges.Add(implTgtEdge);
                    edge.target.TempPlanMapping.IncomingEdges.Add(implTgtEdge);
                }

                if(!isPreset)
                {
                    if(edge.source != null)
                    {
                        PlanEdge outEdge = new PlanEdge(SearchOperationType.Outgoing, edge.source.TempPlanMapping, nodes[nodesIndex], edgeCost[i]);
                        edges.Add(outEdge);
                        nodes[nodesIndex].IncomingEdges.Add(outEdge);
                    }
                    if(edge.target != null)
                    {
                        PlanEdge inEdge = new PlanEdge(SearchOperationType.Incoming, edge.target.TempPlanMapping, nodes[nodesIndex], edgeCost[i]);
                        edges.Add(inEdge);
                        nodes[nodesIndex].IncomingEdges.Add(inEdge);
                    }
                }

                nodesIndex++;
            }

            return new PlanGraph(root, nodes, edges.ToArray(), patternGraph);
        }

        void GenerateAction(LGSPStaticScheduleInfo schedule, IGraphModel model, SourceBuilder source, LGSPMatcherGenerator matcherGen)
        {
            source.Append("    public class Action_" + schedule.ActionName + " : LGSPAction\n    {\n"
                + "        private static Action_" + schedule.ActionName + " instance = new Action_" + schedule.ActionName + "();\n\n"
                + "        public Action_" + schedule.ActionName + "() { rulePattern = " + schedule.RulePattern.GetType().Name
                + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, " + schedule.RulePattern.PatternGraph.Nodes.Length
                + ", " + schedule.RulePattern.PatternGraph.Edges.Length + "); matchesList = matches.matches;}\n\n"

                + "        public override string Name { get { return \"" + schedule.ActionName + "\"; } }\n"
                + "        public static LGSPAction Instance { get { return instance; } }\n"
                + "        private LGSPMatches matches;\n"
                + "        private LGSPMatchesList matchesList;\n");

            PlanGraph planGraph = GenerateStaticPlanGraph((PatternGraph) schedule.RulePattern.PatternGraph,
                schedule.NodeCost, schedule.EdgeCost, false);
            matcherGen.MarkMinimumSpanningArborescence(planGraph, schedule.ActionName);
            SearchPlanGraph searchPlanGraph = matcherGen.GenerateSearchPlanGraph(planGraph);

            SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[schedule.RulePattern.NegativePatternGraphs.Length];
            for(int i = 0; i < schedule.RulePattern.NegativePatternGraphs.Length; i++)
            {
                PlanGraph negPlanGraph = GenerateStaticPlanGraph((PatternGraph) schedule.RulePattern.NegativePatternGraphs[i],
                    schedule.NegNodeCost[i], schedule.NegEdgeCost[i], true);
                matcherGen.MarkMinimumSpanningArborescence(negPlanGraph, schedule.ActionName + "_neg_" + (i + 1));
                negSearchPlanGraphs[i] = matcherGen.GenerateSearchPlanGraph(negPlanGraph);
            }

            ScheduledSearchPlan scheduledSearchPlan = matcherGen.ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);

            matcherGen.CalculateNeededMaps(scheduledSearchPlan);
            source.Append(matcherGen.GenerateMatcherSourceCode(scheduledSearchPlan, schedule.ActionName, schedule.RulePattern));
        }

        enum ErrorType { NoError, GrGenJavaError, GrGenNetError };

        ErrorType ProcessSpecificationImpl(String specFile, String destDir, String tmpDir, UseExistingKind useExisting,
                bool keepGeneratedFiles, bool compileWithDebug)
        {
            Console.WriteLine("Building libraries...");

            String binPath = FixDirectorySeparators(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + Path.DirectorySeparatorChar;

            if(useExisting == UseExistingKind.None)
            {
                try
                {
                    String javaString;
                    if(Environment.OSVersion.Platform == PlatformID.Unix) javaString = "java";
                    else javaString = "javaw";

                    ProcessStartInfo startInfo = new ProcessStartInfo(javaString, "-Xmx1024M -classpath \"" + binPath + "grgen.jar\""
                        + Path.PathSeparator + "\"" + binPath + "antlr.jar\"" + Path.PathSeparator + "\"" + binPath + "jargs.jar\" "
                        + "de.unika.ipd.grgen.Main -n -b de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2 "
                        + "-c " + tmpDir + Path.DirectorySeparatorChar + "printOutput.txt -o " + tmpDir + " \"" + specFile + "\"");
                    startInfo.CreateNoWindow = true;
                    Process grGenJava = Process.Start(startInfo);
                    grGenJava.WaitForExit();

/*                    using(StreamReader sr = new StreamReader(tmpDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                        Console.WriteLine(sr.ReadToEnd());*/
                }
                catch(Exception e)
                {
                    Console.Error.WriteLine("Unable to process specification: " + e.Message);
                    return ErrorType.GrGenJavaError;
                }
            }

            String modelFilename = null;
            String actionsFilename = null;

            String[] producedFiles = Directory.GetFiles(tmpDir);
            foreach(String file in producedFiles)
            {
                if(file.EndsWith("Model.cs"))
                    modelFilename = file;
                else if(file.EndsWith("Actions_intermediate.cs"))
                    actionsFilename = file;
            }

            if(modelFilename == null || actionsFilename == null)
            {
                Console.Error.WriteLine("Not all required files have been generated!");
                return ErrorType.GrGenJavaError;
            }

            if(File.Exists(tmpDir + Path.DirectorySeparatorChar + "printOutput.txt"))
            {
                String output;
                using(StreamReader sr = new StreamReader(tmpDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                {
                    output = sr.ReadToEnd();
                }
                if(output.Contains("ERROR"))
                    return ErrorType.GrGenJavaError;
                if(output.Contains(" warning(s)"))
                {
                    Console.WriteLine(output);
                }
            }

            Assembly modelAssembly;
            String modelAssemblyName;
            if(!ProcessModel(modelFilename, destDir, compileWithDebug, out modelAssembly, out modelAssemblyName)) return ErrorType.GrGenNetError;

            IGraphModel model = GetGraphModel(modelAssembly);
            if(model == null) return ErrorType.GrGenNetError;

            String actionsName = Path.GetFileNameWithoutExtension(actionsFilename);
            actionsName = actionsName.Substring(0, actionsName.Length - 13);    // remove "_intermediate" suffix
            String actionsOutputFilename = tmpDir + Path.DirectorySeparatorChar + actionsName + ".cs";

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPStaticScheduleInfo)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);

            String actionsOutputSource;
            if(useExisting != UseExistingKind.Full)
            {
                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize /d:INITIAL_WARMUP";

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

                String actionsSource;
                using(StreamReader sr = new StreamReader(actionsFilename))
                    actionsSource = sr.ReadToEnd();
                actionsSource = actionsSource.Substring(0, actionsSource.LastIndexOf('}'));     // remove namespace closing bracket
                SourceBuilder source = new SourceBuilder(actionsSource);
                source.Append("\n");
                source.Indent();

                LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(model);
                if(keepGeneratedFiles) matcherGen.CommentSourceCode = true;

                String unitName;
                String modelNamespace = model.GetType().Namespace;
                int lastDot = modelNamespace.LastIndexOf(".");
                if(lastDot == -1) unitName = "";
                else unitName = modelNamespace.Substring(lastDot + 1);

                SourceBuilder endSource = new SourceBuilder("\n");
                endSource.Indent();
                endSource.AppendFront("public class " + unitName + "Actions : LGSPActions\n");
                endSource.AppendFront("{\n");
                endSource.Indent();
                endSource.AppendFront("public " + unitName + "Actions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)\n");
                endSource.AppendFront("    : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)\n");
                endSource.AppendFront("{\n");
                endSource.AppendFront("    InitActions();\n");
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("public " + unitName + "Actions(LGSPGraph lgspgraph)\n");
                endSource.AppendFront("    : base(lgspgraph)\n");
                endSource.AppendFront("{\n");
                endSource.AppendFront("    InitActions();\n");
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("private void InitActions()\n");
                endSource.AppendFront("{\n");
                endSource.Indent();

                foreach(Type type in initialAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.BaseType == typeof(LGSPStaticScheduleInfo))
                    {
                        LGSPStaticScheduleInfo schedule = (LGSPStaticScheduleInfo) initialAssembly.CreateInstance(type.FullName);
                        GenerateAction(schedule, model, source, matcherGen);
                        endSource.AppendFrontFormat("actions.Add(\"{0}\", (LGSPAction) Action_{0}.Instance);\n", schedule.ActionName);
                    }
                }
                endSource.Unindent();
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("public override String Name { get { return \"" + actionsName + "\"; } }\n");
                endSource.AppendFront("public override String ModelMD5Hash { get { return \"" + model.MD5Hash + "\"; } }\n");
                endSource.Unindent();
                endSource.AppendFront("}\n");
                source.Append(endSource.ToString());
                source.Append("}");

                actionsOutputSource = source.ToString();

                if(keepGeneratedFiles)
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

            compParams.GenerateInMemory = false;
            compParams.CompilerOptions = compileWithDebug ? "/debug" : "/optimize";
            compParams.OutputAssembly = destDir + "lgsp-" + actionsName + ".dll";

            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource);
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
        /// <param name="useExisting">Specifies whether and how existing files in the intermediate directory will be used.</param>
        /// <param name="keepIntermediateDir">If true, ..._output.cs files will be generated.</param>
        /// <param name="compileWithDebug">If true, debug information will be generated for the generated assemblies.</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public static void ProcessSpecification(String specPath, String destDir, String intermediateDir, UseExistingKind useExisting,
            bool keepIntermediateDir, bool compileWithDebug)
        {
            ErrorType ret = new LGSPGrGen().ProcessSpecificationImpl(specPath, destDir, intermediateDir, useExisting,
                keepIntermediateDir, compileWithDebug);
            if(ret != ErrorType.NoError)
            {
                if(ret == ErrorType.GrGenJavaError && File.Exists(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                {
                    using(StreamReader sr = new StreamReader(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                        throw new Exception("Error while processing specification:\n" + sr.ReadToEnd());
                }
                else throw new Exception("Error while processing specification!");
            }
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        public static void ProcessSpecification(String specPath)
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
                ProcessSpecification(specPath, specDir, dirname, UseExistingKind.None, false, false);
            }
            finally
            {
                Directory.Delete(dirname, true);
            }
        }
    }
}
