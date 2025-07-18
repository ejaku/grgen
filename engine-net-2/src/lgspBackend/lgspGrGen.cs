/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define DUMP_PATTERNS
//#define USE_NET_3_5 // use .NET 3.5 for compiling the generated code (not needed) and the user extensions (maybe needed there)

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Diagnostics;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The C#-part of the GrGen.NET frontend.
    /// It is responsible for generating initial actions with static search plans.
    /// (and compiling the XGRSs of the exec statements vie LGSPSequenceGenerator)
    /// </summary>
    public class LGSPGrGen
    {
        private readonly Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        private bool assemblyHandlerInstalled = false;

        private readonly ProcessSpecFlags flags;

        /// <summary>
        /// Constructs an LGSPGrGen object.
        /// </summary>
        /// <param name="flags">Flags specifying how the specification should be processed.</param>
        private LGSPGrGen(ProcessSpecFlags flags)
        {
            this.flags = flags;
        }

        /// <summary>
        /// Returns a string where all "wrong" directory separator chars are replaced by the ones used by the system 
        /// </summary>
        /// <param name="path">The original path string potentially with wrong chars</param>
        /// <returns>The corrected path string</returns>
        private static String FixDirectorySeparators(String path)
        {
            if(Path.DirectorySeparatorChar != '\\')
                path = path.Replace('\\', Path.DirectorySeparatorChar);
            if(Path.DirectorySeparatorChar != '/')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            loadedAssemblies.TryGetValue(args.Name, out assembly);
            return assembly;
        }

        private void AddAssembly(Assembly assembly)
        {
            loadedAssemblies.Add(assembly.FullName, assembly);
            if(!assemblyHandlerInstalled)
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                assemblyHandlerInstalled = true;
            }
        }

        private bool ProcessModel(CompileConfiguration cc)
        {
            String modelName = Path.GetFileNameWithoutExtension(cc.modelFilename);
            String modelExtension = Path.GetExtension(cc.modelFilename);

            CSharpCodeProvider compiler;
            CompilerParameters compParams;
            SetupCompiler(null, out compiler, out compParams);
            compParams.ReferencedAssemblies.AddRange(cc.externalAssemblies);
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug /define:DEBUG" : "/optimize";
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
                    ConsoleUI.errorOutWriter.WriteLine("Illegal model C# source code: " + compResults.Errors.Count + " Errors:");
                    foreach(CompilerError error in compResults.Errors)
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Line: " + error.Line + " - " + error.ErrorText + " @ " + error.FileName);
                    }
                    return false;
                }
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to compile model: {0}", ex.Message);
                return false;
            }

            cc.modelAssembly = compResults.CompiledAssembly;
            cc.modelAssemblyName = compParams.OutputAssembly;
            AddAssembly(cc.modelAssembly);

            ConsoleUI.outWriter.WriteLine(" - Model assembly \"{0}\" generated.", cc.modelAssemblyName);
            return true;
        }

        private IGraphModel GetGraphModel(Assembly modelAssembly)
        {
            Type modelType = null;
            try
            {
                foreach(Type type in modelAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
                    if(type.GetInterface("IGraphModel") != null && type.GetInterface("IGraph") == null)
                    {
                        if(modelType != null)
                        {
                            ConsoleUI.errorOutWriter.WriteLine("The given model contains more than one IGraphModel implementation: '"
                                + modelType + "' and '" + type + "'");
                            return null;
                        }
                        modelType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                ConsoleUI.outWriter.WriteLine(e);
            }
            if(modelType == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("The given model does not contain an IGraphModel implementation!");
                return null;
            }

            return (IGraphModel) Activator.CreateInstance(modelType);
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
            bool isSubpatternLike, bool isNegativeOrIndependent,
            ScheduledSearchPlan nestingScheduledSearchPlan)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                if(matcherGen.Profile)
                    LGSPMatcherGenerator.SetNeedForProfiling(patternGraph);
                PlanGraph planGraph;
                if(graphStatistics != null)
                {
                    planGraph = PlanGraphGenerator.GeneratePlanGraph(matcherGen.GetModel(), graphStatistics, patternGraph,
                        isNegativeOrIndependent, isSubpatternLike, matcherGen.InlineIndependents,
                        ScheduleEnricher.ExtractOwnElements(nestingScheduledSearchPlan, patternGraph));
                }
                else
                {
                    planGraph = PlanGraphGenerator.GenerateStaticPlanGraph(patternGraph,
                       isNegativeOrIndependent, isSubpatternLike, matcherGen.InlineIndependents,
                       ScheduleEnricher.ExtractOwnElements(nestingScheduledSearchPlan, patternGraph));
                }
                PlanGraphGenerator.MarkMinimumSpanningArborescence(planGraph, patternGraph.name, matcherGen.DumpSearchPlan);
                SearchPlanGraph searchPlanGraph = SearchPlanGraphGeneratorAndScheduler.GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = SearchPlanGraphGeneratorAndScheduler.ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent, matcherGen.LazyNegativeIndependentConditionEvaluation);
                ScheduleEnricher.AppendHomomorphyInformation(matcherGen.GetModel(), scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(neg, graphStatistics, matcherGen, 
                        isSubpatternLike, true, null);
                }

                foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(idpt, graphStatistics, matcherGen,
                        isSubpatternLike, true, patternGraph.schedules[i]);
                }

                foreach(Alternative alt in patternGraph.alternativesPlusInlined)
                {
                    foreach(PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, graphStatistics, matcherGen, 
                            true, false, null);
                    }
                }

                foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, graphStatistics, matcherGen, 
                        true, false, null);
                }
            }
        }

        private static bool ExecuteGrGenJava(String tmpDir, ProcessSpecFlags flags,
            out List<String> genModelFiles, out List<String> genModelStubFiles,
            out List<String> genActionsFiles, params String[] sourceFiles)
        {
            genModelFiles = new List<string>();
            genModelStubFiles = new List<string>();
            genActionsFiles = new List<string>();

            if(sourceFiles.Length == 0)
            {
                ConsoleUI.errorOutWriter.WriteLine("No GrGen.NET source files specified!");
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
                    if(grGenJava == null || grGenJava.HasExited)
                        return;

                    ConsoleUI.errorOutWriter.WriteLine("Aborting...");
                    System.Threading.Thread.Sleep(100);     // a short delay to make sure the process is correctly started
                    if(!grGenJava.HasExited)
                        grGenJava.Kill();
                }
                ctrlCPressed = true;
                if(e != null)               // compare to null, as we also call this by ourself when handling has been delayed
                    e.Cancel = true;        // we handled the cancel event
            };

            ProcessStartInfo startInfo = null;
            String javaString = null;
            try
            {
                if(Environment.OSVersion.Platform == PlatformID.Unix)
                    javaString = "java";
                else
                    javaString = "javaw";

                String execStr = "-Xss1M -Xmx1024M -jar \"" + binPath + "grgen.jar\" "
                    + "-b de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2 "
                    + "-c \"" + tmpDir + Path.DirectorySeparatorChar + "printOutput.txt\" "
                    + "-o \"" + tmpDir + "\""
                    + ((flags & ProcessSpecFlags.NoEvents) != 0 ? " --noevents" : "")
                    + ((flags & ProcessSpecFlags.NoDebugEvents) != 0 ? " --nodebugevents" : "")
                    + ((flags & ProcessSpecFlags.Profile) != 0 ? " --profile" : "")
                    + " \"" + String.Join("\" \"", sourceFiles) + "\"";
                startInfo = new ProcessStartInfo(javaString, execStr);
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                try
                {
                    ConsoleUI.consoleIn.CancelKeyPress += ctrlCHandler;

                    grGenJava = Process.Start(startInfo);

                    delayCtrlC = false;
                    if(ctrlCPressed)
                        ctrlCHandler(null, null);

                    grGenJava.WaitForExit();
                }
                finally
                {
                    ConsoleUI.consoleIn.CancelKeyPress -= ctrlCHandler;
                }
            }
            catch(System.ComponentModel.Win32Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to process specification: " + e.Message);
                ConsoleUI.errorOutWriter.WriteLine("Is Java installed and the executable " + javaString + " available in one of the folders of the search path? Search path: " + startInfo.EnvironmentVariables["path"]);
                return false;
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to process specification: " + e.Message);
                return false;
            }

            if(ctrlCPressed)
                return false;

            bool noError = true;
            bool doneFound = false;
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
                        ConsoleUI.errorOutWriter.WriteLine(line);
                        noError = false;
                        continue;
                    }
                    if(line.Contains("WARNING"))
                    {
                        ConsoleUI.errorOutWriter.WriteLine(line);
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

            public String specFile
            {
                get { return _specFile; }
            }
            public String destDir
            {
                get { return _destDir; }
            }
            public String tmpDir
            {
                get { return _tmpDir; }
            }
            public String[] externalAssemblies
            {
                get { return _externalAssemblies; }
            }

            public String modelFilename
            {
                get { return _modelFilename; }
                set { _modelFilename = value; }
            }
            public String modelStubFilename
            {
                get { return _modelStubFilename; }
                set { _modelStubFilename = value; }
            }
            public String modelAssemblyName
            {
                get { return _modelAssemblyName; }
                set { _modelAssemblyName = value; }
            }
            public Assembly modelAssembly
            {
                get { return _modelAssembly; }
                set { _modelAssembly = value; }
            }

            public String actionsName
            {
                get { return _actionsName; }
            }
            public String actionsFilename
            {
                get { return _actionsFilename; } 
                set
                { 
                    _actionsFilename = value;
                    _actionsName = Path.GetFileNameWithoutExtension(_actionsFilename); 
                    _actionsName = _actionsName.Substring(0, _actionsName.Length - 13); // remove "_intermediate" suffix
                    _baseName = _actionsName.Substring(0, _actionsName.Length - 7); // remove "Actions" suffix
                    _actionsOutputFilename = _tmpDir + Path.DirectorySeparatorChar + _actionsName + ".cs";
                }
            }
            public String actionsOutputFilename
            {
                get { return _actionsOutputFilename; }
            }
            public String baseName
            {
                get { return _baseName; }
            }

            public String externalActionsExtensionFilename
            {
                get { return _externalActionsExtensionFilename; }
                set { _externalActionsExtensionFilename = value; }
            }
            public String externalActionsExtensionOutputFilename
            {
                get { return _externalActionsExtensionOutputFilename; }
                set { _externalActionsExtensionOutputFilename = value; }
            }


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

        private ErrorType ProcessSpecificationImpl(String specFile, String destDir, String tmpDir, String statisticsPath, String[] externalAssemblies)
        {
            ConsoleUI.outWriter.WriteLine("Building libraries...");

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
                    {
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    }
                    ConsoleUI.errorOutWriter.WriteLine("Illegal actions C# input source code: " + errorMsg);
                    return ErrorType.GrGenNetError;
                }
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to compile initial actions: {0}", ex.Message);
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
                ConsoleUI.errorOutWriter.WriteLine("Unable to read from file \"" + actionsOutputFilename + "\"!");
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

            ActionsTypeInformation actionsTypeInformation = ruleAndMatchingPatterns.CollectActionParameterTypes();

            LGSPSequenceGenerator seqGen = new LGSPSequenceGenerator(model, actionsTypeInformation,
                (flags & ProcessSpecFlags.NoDebugEvents) == 0, (flags & ProcessSpecFlags.Profile) != 0);

            ///////////////////////////////////////////////
            // generate external extension source if needed (cause there are external action extension)

            bool isAutoGeneratedFilterExisting;
            bool isExternalFilterFunctionExisting;
            bool isMatchClassAutoGeneratedFilterExisting;
            bool isMatchClassExternalFilterFunctionExisting;
            bool isExternalSequenceExisting;
            DetermineWhetherExternalActionsFileIsNeeded(ruleAndMatchingPatterns,
                out isAutoGeneratedFilterExisting, out isExternalFilterFunctionExisting,
                out isMatchClassAutoGeneratedFilterExisting, out isMatchClassExternalFilterFunctionExisting,
                out isExternalSequenceExisting);

            SourceBuilder externalSource = null;
            if(isAutoGeneratedFilterExisting || isExternalFilterFunctionExisting 
                || isMatchClassAutoGeneratedFilterExisting || isMatchClassExternalFilterFunctionExisting
                || isExternalSequenceExisting)
            {
                EmitExternalActionsFileHeader(cc, model, isExternalFilterFunctionExisting || isMatchClassExternalFilterFunctionExisting || isExternalSequenceExisting,
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
                ConsoleUI.errorOutWriter.WriteLine("Illegal actions C# input source code: Actions insertion point not found!");
                return ErrorType.GrGenJavaError;
            }

            source.Unindent();
            source.Append("\n");

            ///////////////////////////////////////////////
            // generate and insert the matcher source code into the action file
            // already filled with the content of the action intermediate file until the action insertion point

            String unitName;
            int lastDot = actionsNamespace.LastIndexOf(".");
            if(lastDot == -1)
                unitName = "";
            else
                unitName = actionsNamespace.Substring(lastDot + 8);  // skip ".Action_"

            LGSPGraphStatistics graphStatistics = null;
            if(statisticsPath != null)
            {
                ConsoleUI.outWriter.WriteLine("Reading graph statistics from {0}", statisticsPath);
                graphStatistics = new LGSPGraphStatistics(model);
                GraphStatisticsParserSerializer parserSerializer = new GraphStatisticsParserSerializer(graphStatistics);
                parserSerializer.Parse(statisticsPath);
            }

            result = GenerateAndInsertMatcherSourceCode(model, cc.actionsName, unitName,
                cc.externalActionsExtensionFilename, ruleAndMatchingPatterns, seqGen,
                isAutoGeneratedFilterExisting, isExternalFilterFunctionExisting,
                isMatchClassAutoGeneratedFilterExisting, isMatchClassExternalFilterFunctionExisting,
                graphStatistics, statisticsPath,
                externalSource, source);
            if(result != ErrorType.NoError)
                return result;

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
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug /define:DEBUG" : "/optimize";
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
                {
                    errorMsg += String.Format("\r\n{0} at line {1} of {2}: {3}", error.IsWarning ? "Warning" : "ERROR", error.Line, error.FileName, error.ErrorText);
                }
                ConsoleUI.errorOutWriter.WriteLine("Illegal generated actions C# source code (or erroneous programmed extension), " + errorMsg);
                return ErrorType.GrGenNetError;
            }

            ConsoleUI.outWriter.WriteLine(" - Actions assembly \"{0}\" generated.", compParams.OutputAssembly);
            return ErrorType.NoError;
        }

        private ErrorType GenerateAndInsertMatcherSourceCode(IGraphModel model, String actionsName, String unitName,
            string externalActionsExtensionFilename, LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, 
            LGSPSequenceGenerator seqGen, bool isAutoGeneratedFilterExisting, bool isExternalFilterFunctionExisting,
            bool isMatchClassAutoGeneratedFilterExisting, bool isMatchClassExternalFilterFunctionExisting,
            LGSPGraphStatistics graphStatistics, string statisticsPath,
            SourceBuilder externalSource, SourceBuilder source)
        {
            // analyze the matching patterns, inline the subpatterns when expected to be benefitial
            // the analyzer must be run before the matcher generation
            AnalyzeAndInlineMatchingPatterns((flags & ProcessSpecFlags.Noinline) == 0, ruleAndMatchingPatterns);

            LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(model);
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
                matcherGen.CommentSourceCode = true;
            if((flags & ProcessSpecFlags.CompileWithDebug) != 0)
                matcherGen.EmitDebugValidityChecks = true;
            if((flags & ProcessSpecFlags.LazyNIC) != 0)
                matcherGen.LazyNegativeIndependentConditionEvaluation = true;
            if((flags & ProcessSpecFlags.Noinline) != 0)
                matcherGen.InlineIndependents = false;
            if((flags & ProcessSpecFlags.Profile) != 0)
                matcherGen.Profile = true;

            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                GenerateScheduledSearchPlans(matchingPattern.patternGraph, graphStatistics, 
                    matcherGen, !(matchingPattern is LGSPRulePattern), false, null);

                ScheduleEnricher.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(matchingPattern.patternGraph, matcherGen.LazyNegativeIndependentConditionEvaluation);

                ScheduleEnricher.ParallelizeAsNeeded(matchingPattern);

                matcherGen.GenerateActionAndMatcher(source, matchingPattern, true);
            }

            ErrorType result = GenerateDefinedSequencesAndFiltersAndFilterStubs(externalActionsExtensionFilename, 
                isAutoGeneratedFilterExisting, isExternalFilterFunctionExisting,
                isMatchClassAutoGeneratedFilterExisting, isMatchClassExternalFilterFunctionExisting,
                ruleAndMatchingPatterns, seqGen, externalSource, source);
            if(result != ErrorType.NoError)
                return result;

            foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
            {
                FilterGenerator.GenerateMatchClassFilterers(source, matchClass, model);
            }

            // the actions class referencing the generated stuff is generated now into 
            // a source builder which is appended at the end of the other generated stuff
            SourceBuilder endSource = GenerateActionsClass(model, actionsName, unitName,
                statisticsPath, ruleAndMatchingPatterns, 
                matcherGen.LazyNegativeIndependentConditionEvaluation,
                matcherGen.InlineIndependents,
                matcherGen.Profile);
            source.Append(endSource.ToString());
            source.Append("}");

            return ErrorType.NoError;
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
            {
                matchingPattern.patternGraph.maxIsoSpace = 0; // reset of max iso space for computation of max iso space of inlined patterns
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                analyzer.AnalyzeNestingOfPatternGraph(matchingPattern.patternGraph, true);
            }

            analyzer.ComputeInterPatternRelations(true);
        }

        private static ErrorType GenerateDefinedSequencesAndFiltersAndFilterStubs(string externalActionsExtensionFilename, 
            bool isAutoGeneratedFilterExisting, bool isExternalFilterFunctionExisting,
            bool isMatchClassAutoGeneratedFilterExisting, bool isMatchClassExternalFilterFunctionExisting,
            LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, LGSPSequenceGenerator seqGen,
            SourceBuilder externalSource, SourceBuilder source)
        {
            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                if(sequence is ExternalDefinedSequenceInfo)
                    seqGen.GenerateExternalDefinedSequencePlaceholder(externalSource, (ExternalDefinedSequenceInfo)sequence, externalActionsExtensionFilename);
            }

            if(isAutoGeneratedFilterExisting || isExternalFilterFunctionExisting
                || isMatchClassAutoGeneratedFilterExisting || isMatchClassExternalFilterFunctionExisting)
            {
                if(isExternalFilterFunctionExisting)
                {
                    externalSource.Append("\n").AppendFront("// ------------------------------------------------------\n\n");

                    externalSource.AppendFrontFormat("// You must implement the following filter functions in the same partial class in ./{0}\n", externalActionsExtensionFilename);
                    externalSource.Append("\n");
                    foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                    {
                        FilterGenerator.GenerateFilterStubs(externalSource, rulePattern);
                    }
                }

                if(isAutoGeneratedFilterExisting)
                {
                    externalSource.Append("\n").AppendFront("// ------------------------------------------------------\n\n");

                    externalSource.AppendFront("// The following filter functions are automatically generated, you don't need to supply any further implementation\n");
                    externalSource.Append("\n");
                    foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                    {
                        FilterGenerator.GenerateFilters(externalSource, rulePattern);
                    }

                    foreach(LGSPRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
                    {
                        foreach(IIterated iterated in rulePattern.PatternGraph.Iterateds)
                        {
                            FilterGenerator.GenerateFilters(externalSource, rulePattern, iterated);
                        }
                    }
                }

                if(isMatchClassExternalFilterFunctionExisting)
                {
                    externalSource.Append("\n").AppendFront("// ------------------------------------------------------\n\n");

                    externalSource.AppendFrontFormat("// You must implement the following match class filter functions in the same partial class in ./{0}\n", externalActionsExtensionFilename);
                    externalSource.Append("\n");
                    foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
                    {
                        FilterGenerator.GenerateMatchClassFilterStubs(externalSource, matchClass);
                    }
                }

                if(isMatchClassAutoGeneratedFilterExisting)
                {
                    externalSource.Append("\n").AppendFront("// ------------------------------------------------------\n\n");

                    externalSource.AppendFront("// The following match class filter functions are automatically generated, you don't need to supply any further implementation\n");
                    externalSource.Append("\n");
                    foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
                    {
                        FilterGenerator.GenerateMatchClassFilters(externalSource, matchClass);
                    }
                }
            }

            if(externalSource != null)
            {
                externalSource.Append("\n");
                externalSource.AppendFront("// ------------------------------------------------------\n");
            }

            bool success = true;
            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                if(sequence is ExternalDefinedSequenceInfo)
                    seqGen.GenerateExternalDefinedSequence(externalSource, (ExternalDefinedSequenceInfo)sequence);
                else
                    success &= seqGen.GenerateDefinedSequence(source, sequence);
            }

            if(success)
                return ErrorType.NoError;
            else
                return ErrorType.GrGenNetError;
        }

        private SourceBuilder GenerateActionsClass(IGraphModel model, String actionsName, String unitName,
            string statisticsPath, LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, 
            bool lazyNIC, bool inlineIndependents, bool profile)
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
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("analyzer.RememberMatchingPattern({1}Rule_{0}.Instance);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("actions.Add(\"{2}\", (GRGEN_LGSP.LGSPAction) "
                            + "{1}Action_{0}.Instance);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                            matchingPattern.PatternGraph.PackagePrefixedName);

                    endSource.AppendFrontFormat("@{2} = {1}Action_{0}.Instance;\n",
                        matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                        TypesHelper.PackagePrefixedNameUnderscore(matchingPattern.PatternGraph.Package, matchingPattern.PatternGraph.Name));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Pattern_{0}.Instance.patternGraph, false);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                    endSource.AppendFrontFormat("analyzer.RememberMatchingPattern({1}Pattern_{0}.Instance);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }

            endSource.AppendFront("analyzer.ComputeInterPatternRelations(false);\n");
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeWithInterPatternRelationsKnown({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.InlineSubpatternUsages({1}Rule_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.InlineSubpatternUsages({1}Pattern_{0}.Instance.patternGraph);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("{1}Rule_{0}.Instance.patternGraph.maxIsoSpace = 0;\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("{1}Pattern_{0}.Instance.patternGraph.maxIsoSpace = 0;\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Rule_{0}.Instance.patternGraph, true);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
                else
                {
                    endSource.AppendFrontFormat("analyzer.AnalyzeNestingOfPatternGraph({1}Pattern_{0}.Instance.patternGraph, true);\n",
                            matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package));
                }
            }
            endSource.AppendFront("analyzer.ComputeInterPatternRelations(true);\n");

            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                endSource.AppendFrontFormat("RegisterGraphRewriteSequenceDefinition("
                        + "{1}Sequence_{0}.Instance);\n", sequence.Name,
                        "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(sequence.Package));

                endSource.AppendFrontFormat("@{2} = {1}Sequence_{0}.Instance;\n", sequence.Name,
                    "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(sequence.Package),
                    TypesHelper.PackagePrefixedNameUnderscore(sequence.Package, sequence.Name));
            }

            foreach(FunctionInfo function in ruleAndMatchingPatterns.Functions)
            {
                endSource.AppendFrontFormat("namesToFunctionDefinitions.Add(\"{2}\", {1}FunctionInfo_{0}.Instance);\n",
                    function.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(function.package), function.packagePrefixedName);
            }

            foreach(ProcedureInfo procedure in ruleAndMatchingPatterns.Procedures)
            {
                endSource.AppendFrontFormat("namesToProcedureDefinitions.Add(\"{2}\", {1}ProcedureInfo_{0}.Instance);\n",
                    procedure.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(procedure.package), procedure.packagePrefixedName);
            }

            foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
            {
                endSource.AppendFrontFormat("namesToMatchClassFilterers.Add(\"{2}\", {1}MatchClassFilterer_{0}.Instance);\n",
                    matchClass.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchClass.package), matchClass.packagePrefixedName);
            }

            endSource.Unindent();
            endSource.AppendFront("}\n");
            endSource.AppendFront("\n");

            foreach(LGSPMatchingPattern matchingPattern in ruleAndMatchingPatterns.RulesAndSubpatterns)
            {
                if(matchingPattern is LGSPRulePattern) // normal rule
                {
                    endSource.AppendFrontFormat("public {1}IAction_{0} @{2};\n",
                        matchingPattern.name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(matchingPattern.PatternGraph.Package),
                        TypesHelper.PackagePrefixedNameUnderscore(matchingPattern.PatternGraph.Package, matchingPattern.PatternGraph.Name));
                }
            }
            endSource.AppendFront("\n");

            foreach(DefinedSequenceInfo sequence in ruleAndMatchingPatterns.DefinedSequences)
            {
                endSource.AppendFrontFormat("public {1}Sequence_{0} @{2};\n",
                    sequence.Name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(sequence.Package),
                    TypesHelper.PackagePrefixedNameUnderscore(sequence.Package, sequence.Name));
            }
            endSource.AppendFront("\n");

            endSource.AppendFront("public override string[] Packages { get { return packages; } }\n");
            endSource.AppendFront("private string[] packages;\n");
            endSource.AppendFront("\n");

            endSource.AppendFront("public override string Name { get { return \"" + actionsName + "\"; } }\n");
            endSource.AppendFront("public override string StatisticsPath { get { return " + (statisticsPath != null ? "@\"" + statisticsPath + "\"" : "null") + "; } }\n");
            endSource.AppendFront("public override bool LazyNIC { get { return " + (lazyNIC ? "true" : "false") + "; } }\n");
            endSource.AppendFront("public override bool InlineIndependents { get { return " + (inlineIndependents ? "true" : "false") + "; } }\n");
            endSource.AppendFront("public override bool Profile { get { return " + (profile ? "true" : "false") + "; } }\n");

            GenerateArrayHelperDispatchers(endSource, ruleAndMatchingPatterns);

            endSource.AppendFront("public override void FailAssertion() { Debug.Assert(false); }\n");
            endSource.AppendFront("public override string ModelMD5Hash { get { return \"" + model.MD5Hash + "\"; } }\n");
            endSource.Unindent();
            endSource.AppendFront("}\n");
            return endSource;
        }

        private void GenerateArrayHelperDispatchers(SourceBuilder sb, LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns)
        {
            sb.Append("\n");

            sb.AppendFront("public override IList ArrayOrderAscendingBy(IList array, string member)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "orderAscendingBy", ruleAndMatchingPatterns, true, false, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override IList ArrayOrderDescendingBy(IList array, string member)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "orderDescendingBy", ruleAndMatchingPatterns, true, false, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override IList ArrayGroupBy(IList array, string member)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "groupBy", ruleAndMatchingPatterns, false, false, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override IList ArrayKeepOneForEach(IList array, string member)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "keepOneForEachBy", ruleAndMatchingPatterns, false, false, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override int ArrayIndexOfBy(IList array, string member, object value)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "indexOfBy", ruleAndMatchingPatterns, false, true, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override int ArrayIndexOfBy(IList array, string member, object value, int startIndex)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "indexOfBy", ruleAndMatchingPatterns, false, true, true);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override int ArrayLastIndexOfBy(IList array, string member, object value)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "lastIndexOfBy", ruleAndMatchingPatterns, false, true, false);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override int ArrayLastIndexOfBy(IList array, string member, object value, int startIndex)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "lastIndexOfBy", ruleAndMatchingPatterns, false, true, true);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.Append("\n");

            sb.AppendFront("public override int ArrayIndexOfOrderedBy(IList array, string member, object value)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            GenerateArrayHelperDispatcher(sb, "indexOfOrderedBy", ruleAndMatchingPatterns, true, true, false);
            sb.Unindent();
            sb.AppendFront("}\n");
        }

        private void GenerateArrayHelperDispatcher(SourceBuilder sb, String function,
            LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns, bool requiresOrderable, bool isIndexOfByMethod, bool hasStartIndex)
        {
            sb.AppendFront("if(array.Count == 0)\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "array");
            sb.AppendFront("string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());\n");
            sb.AppendFront("string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);\n");
            sb.AppendFront("if(!arrayValueType.StartsWith(\"match<\"))\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("if(array[0] == null)\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("if(arrayValueType == \"match<>\")\n");
            sb.AppendFrontIndented("arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());\n");
            sb.AppendFront("if(arrayValueType.StartsWith(\"match<class \"))\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))\n");
            sb.AppendFront("{\n");
            foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
            {
                GenerateArrayHelperByTypeDispatcher(sb, function, matchClass, requiresOrderable, isIndexOfByMethod, hasStartIndex);
            }
            sb.AppendFront("default:\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("else\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))\n");
            sb.AppendFront("{\n");
            foreach(LGSPRulePattern rule in ruleAndMatchingPatterns.Rules)
            {
                GenerateArrayHelperByTypeDispatcher(sb, function, rule, null, requiresOrderable, isIndexOfByMethod, hasStartIndex);
                /*foreach(Iterated iterated in rule.patternGraph.iterateds)
                {
                    GenerateArrayHelperByTypeDispatcher(sb, function, rule, iterated, requiresOrderable);
                }*/
            }
            sb.AppendFront("default:\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");
        }

        private void GenerateArrayHelperByTypeDispatcher(SourceBuilder sb, String function,
            MatchClassInfo matchClass, bool requiresOrderable, bool isIndexOfByMethod, bool hasStartIndex)
        {
            sb.AppendFrontFormat("case \"{0}\":\n", matchClass.PackagePrefixedName);
            sb.Indent();
            sb.AppendFront("switch(member)\n");
            sb.AppendFront("{\n");
            foreach(IPatternElement member in matchClass.PatternElements)
            {
                if(requiresOrderable && !isOfOrderableType(member))
                    continue;
                if(!isOfFilterableType(member))
                    continue;
                GenerateArrayHelperByMemberDispatcher(sb, function, matchClass, member, isIndexOfByMethod, hasStartIndex);
            }
            sb.AppendFront("default:\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("}\n");
            sb.Unindent();
        }

        private void GenerateArrayHelperByMemberDispatcher(SourceBuilder sb, String function,
            MatchClassInfo matchClass, IPatternElement member, bool isIndexOfByMethod, bool hasStartIndex)
        {
            sb.AppendFrontFormat("case \"{0}\":\n", member.UnprefixedName);
            String packagePrefix = matchClass.Package != null ? matchClass.Package + "." : "";
            String arrayHelperFunctionName = "Array_" + matchClass.Name + "_" + function + "_" + member.UnprefixedName;
            sb.AppendFrontIndentedFormat("return {0}ArrayHelper.{1}({2}.ConvertAsNeeded(array)",
                packagePrefix, arrayHelperFunctionName,
                TypesHelper.MatchClassInfoForMatchClassType("match<class " + matchClass.PackagePrefixedName + ">"));
            if(isIndexOfByMethod)
                sb.AppendFormat(", ({0})value", TypesHelper.TypeName(member.Type));
            if(hasStartIndex)
                sb.Append(", startIndex");
            sb.Append(");\n");
        }

        private void GenerateArrayHelperByTypeDispatcher(SourceBuilder sb, String function,
            LGSPRulePattern rule, Iterated iterated, bool requiresOrderable, bool isIndexOfByMethod, bool hasStartIndex)
        {
            String iteratedPostfixDot = iterated != null ? "." + iterated.IteratedPattern.Name : "";
            sb.AppendFrontFormat("case \"{0}\":\n", rule.PatternGraph.PackagePrefixedName + iteratedPostfixDot);
            sb.Indent();
            sb.AppendFront("switch(member)\n");
            sb.AppendFront("{\n");
            if(iterated != null)
            {
                foreach(IPatternElement member in iterated.iteratedPattern.PatternElements)
                {
                    if(requiresOrderable && !isOfOrderableType(member))
                        continue;
                    if(!isOfFilterableType(member))
                        continue;
                    GenerateArrayHelperByMemberDispatcher(sb, function, rule, iterated, member, isIndexOfByMethod, hasStartIndex);
                }
            }
            else
            {
                foreach(IPatternElement member in rule.patternGraph.PatternElements)
                {
                    if(requiresOrderable && !isOfOrderableType(member))
                        continue;
                    if(!isOfFilterableType(member))
                        continue;
                    GenerateArrayHelperByMemberDispatcher(sb, function, rule, null, member, isIndexOfByMethod, hasStartIndex);
                }
            }
            sb.AppendFront("default:\n");
            sb.AppendFrontIndentedFormat("return {0};\n", isIndexOfByMethod ? "-1" : "null");
            sb.AppendFront("}\n");
            sb.Unindent();
        }

        private void GenerateArrayHelperByMemberDispatcher(SourceBuilder sb, String function,
            LGSPRulePattern rule, Iterated iterated, IPatternElement member, bool isIndexOfByMethod, bool hasStartIndex)
        {
            String iteratedPostfixDot = iterated != null ? "." + iterated.IteratedPattern.Name : "";
            sb.AppendFrontFormat("case \"{0}\":\n", member.UnprefixedName);
            String packagePrefix = rule.patternGraph.Package != null ? rule.patternGraph.Package + "." : "";
            String ruleClassName = "Rule_" + rule.PatternGraph.Name;
            String iteratedPostfix = iterated != null ? "_" + iterated.IteratedPattern.Name : "";
            String arrayHelperFunctionName = "Array_" + rule.PatternGraph.Name + iteratedPostfix + "_" + function + "_" + member.UnprefixedName;
            sb.AppendFrontIndentedFormat("return {0}ArrayHelper.{1}({2}.ConvertAsNeeded(array)",
                packagePrefix, arrayHelperFunctionName,
                TypesHelper.RuleClassForMatchType("match<" + rule.PatternGraph.PackagePrefixedName+ ">"));
            if(isIndexOfByMethod)
                sb.AppendFormat(", ({0})value", TypesHelper.TypeName(member.Type));
            if(hasStartIndex)
                sb.Append(", startIndex");
            sb.Append(");\n");
        }

        private bool isOfOrderableType(IPatternElement member)
        {
            if(member is IPatternNode)
                return false;
            else if(member is IPatternEdge)
                return false;
            else
                return isOrderableType(((IPatternVariable)member).Type);
        }

        private bool isOrderableType(VarType type)
        {
            if(type.Equals(VarType.GetVarType(typeof(sbyte))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(short))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(int))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(long))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(float))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(double))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(string))))
                return true;
            if(type.Equals(VarType.GetVarType(typeof(bool))))
                return true;
            if(type.Type.IsEnum)
                return true;
            return false;
        }

        private bool isOfFilterableType(IPatternElement member)
        {
            if(member is IPatternNode)
                return true;
            else if(member is IPatternEdge)
                return true;
            else
                return isOrderableType(((IPatternVariable)member).Type);
        }

        private ErrorType GenerateModelAndIntermediateActions(CompileConfiguration cc)
        {
            if((flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
            {
                List<String> genModelFiles;
                List<String> genModelStubFiles;
                List<String> genActionsFiles;
                if(!ExecuteGrGenJava(cc.tmpDir, flags,
                    out genModelFiles, out genModelStubFiles,
                    out genActionsFiles, cc.specFile))
                {
                    return ErrorType.GrGenJavaError;
                }

                if(genModelFiles.Count == 1)
                    cc.modelFilename = genModelFiles[0];
                else if(genModelFiles.Count > 1)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Multiple models are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genModelStubFiles.Count == 1)
                    cc.modelStubFilename = genModelStubFiles[0];
                else if(genModelStubFiles.Count > 1)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Multiple model stubs are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genActionsFiles.Count == 1)
                    cc.actionsFilename = genActionsFiles[0];
                else if(genActionsFiles.Count > 1)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Multiple action sets are not supported by ProcessSpecification, yet!");
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
                ConsoleUI.errorOutWriter.WriteLine("Not all required files have been generated!");
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
                ConsoleUI.errorOutWriter.WriteLine("Unable to read from file \"" + externalActionsExtensionOutputFilename + "\"!");
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
                    ConsoleUI.errorOutWriter.WriteLine("Unable to read from file \"" + externalActionsExtensionFilename + "\"!");
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
            out bool isAutoGeneratedFilterExisting, out bool isExternalFilterFunctionExisting,
            out bool isMatchClassAutoGeneratedFilterExisting, out bool isMatchClassExternalFilterFunctionExisting,
            out bool isExternalSequenceExisting)
        {
            isAutoGeneratedFilterExisting = false;
            isExternalFilterFunctionExisting = false;
            foreach(IRulePattern rulePattern in ruleAndMatchingPatterns.Rules)
            {
                foreach(IFilter filter in rulePattern.Filters)
                {
                    if(filter is IFilterAutoGenerated)
                        isAutoGeneratedFilterExisting = true;
                    else if(filter is IFilterFunction && ((IFilterFunction)filter).IsExternal)
                        isExternalFilterFunctionExisting = true;
                }

                foreach(IIterated iterated in rulePattern.PatternGraph.Iterateds)
                {
                    foreach(IFilter filter in iterated.Filters)
                    {
                        if(filter is IFilterAutoGenerated)
                            isAutoGeneratedFilterExisting = true;
                    }
                }
            }

            isMatchClassAutoGeneratedFilterExisting = false;
            isMatchClassExternalFilterFunctionExisting = false;
            foreach(MatchClassInfo matchClass in ruleAndMatchingPatterns.MatchClasses)
            {
                foreach(IFilter filter in matchClass.Filters)
                {
                    if(filter is IFilterAutoGenerated)
                        isMatchClassAutoGeneratedFilterExisting = true;
                    else if(filter is IFilterFunction && ((IFilterFunction)filter).IsExternal)
                        isMatchClassExternalFilterFunctionExisting = true;
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

        private static bool IsFilterVariable(string variable, IRulePattern rulePattern)
        {
            foreach(IPatternVariable patternVariable in rulePattern.PatternGraph.Variables)
            {
                if(patternVariable.UnprefixedName == variable)
                    return true;
            }
            return false;
        }

        private static void CollectActionTypes(Assembly initialAssembly, out Dictionary<String, Type> actionTypes,
            out Dictionary<String, Type> proceduresTypes, out LGSPRuleAndMatchingPatterns ruleAndMatchingPatterns)
        {
            actionTypes = new Dictionary<string, Type>();
            proceduresTypes = new Dictionary<String, Type>();

            foreach(Type type in initialAssembly.GetTypes())
            {
                if(!type.IsClass || type.IsNotPublic)
                    continue;
                if(type.BaseType == typeof(LGSPMatchingPattern) || type.BaseType == typeof(LGSPRulePattern))
                    actionTypes.Add(TypesHelper.GetPackagePrefixedNameFromFullTypeName(type.FullName), type);
                if(type.Name == "Procedures")
                    proceduresTypes.Add(TypesHelper.GetPackagePrefixedNameFromFullTypeName(type.FullName), type);
            }

            ruleAndMatchingPatterns = null;
            foreach(Type type in initialAssembly.GetTypes())
            {
                if(!type.IsClass || type.IsNotPublic)
                    continue;
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
                ConsoleUI.outWriter.WriteLine(ex);
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
                throw new Exception("\nError while processing specification!");
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
                ++id;
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
                if((flags & ProcessSpecFlags.KeepGeneratedFiles) == 0 && (flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
                    Directory.Delete(dirname, true);
            }
        }
    }
}
