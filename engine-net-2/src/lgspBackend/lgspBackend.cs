/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

using de.unika.ipd.grGen.libGr;
using System.IO;
using de.unika.ipd.grGen.libGr.sequenceParser;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A helper class for backend independent graph and rule handling.
    /// </summary>
    public class LGSPBackend : IBackend
    {
        private static int modelID = 0;
        private static int graphID = 0;
        private Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        private bool assemblyHandlerInstalled = false;
        private static char[] dirSepChars = new char[] { '/', '\\' };

        public String Name { get { return "lgspBackend"; } }

        public IEnumerable<String> ArgumentNames { get { yield break; } }

        private String GetNextGraphName()
        {
            return "graph_" + graphID++;
        }

        public IGraph CreateGraph(IGraphModel graphModel, string graphName, params String[] parameters)
        {
            String assemblyName = Assembly.GetAssembly(graphModel.GetType()).Location;
            return new LGSPGraph(this, graphModel, graphName, assemblyName);
        }

        public INamedGraph CreateNamedGraph(IGraphModel graphModel, string graphName, params String[] parameters)
        {
            String assemblyName = Assembly.GetAssembly(graphModel.GetType()).Location;
            return new LGSPNamedGraph(this, graphModel, graphName, assemblyName);
        }

        public IGraph CreateGraph(String modelFilename, String graphName, params String[] parameters)
        {
            return CreateGraph(modelFilename, graphName, false, parameters);
        }

        public INamedGraph CreateNamedGraph(String modelFilename, String graphName, params String[] parameters)
        {
            return (INamedGraph)CreateGraph(modelFilename, graphName, true, parameters);
        }

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph backend instance with the graph model provided by the graph model file and a name.
        /// </summary>
        /// <param name="modelFilename">Filename of a graph model file.</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <param name="parameters">Backend specific parameters.</param>
        /// <returns>The new IGraph backend instance.</returns>
        public LGSPGraph CreateGraph(String modelFilename, String graphName, bool named, params String[] parameters)
        {
            Assembly assembly;
            String assemblyName;

            String extension = Path.GetExtension(modelFilename);
            if(extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
            {
                CSharpCodeProvider compiler = new CSharpCodeProvider();
                CompilerParameters compParams = new CompilerParameters();
                compParams.ReferencedAssemblies.Add("System.dll");
                compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);

                //                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize";
                //                compParams.IncludeDebugInformation = true;
                compParams.OutputAssembly = String.Format("lgspmodelassembly-{0:00000}.dll", modelID++);

                CompilerResults compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename);
                if(compResults.Errors.HasErrors)
                {
                    String errorMsg = compResults.Errors.Count + " Errors:";
                    foreach(CompilerError error in compResults.Errors)
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    throw new ArgumentException("Illegal model C# source code: " + errorMsg);
                }

                assembly = compResults.CompiledAssembly;
                assemblyName = compParams.OutputAssembly;
            }
            else if(extension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
            {
                assembly = Assembly.LoadFrom(modelFilename);
                assemblyName = modelFilename;
                AddAssembly(assembly);
            }
            else if(extension.Equals(".gm", StringComparison.OrdinalIgnoreCase))
            {
                String modelDllFilename;
                if(MustGenerate(modelFilename, out modelDllFilename))
                    LGSPGrGen.ProcessSpecification(modelFilename, ProcessSpecFlags.UseNoExistingFiles, new String[0]);
                return named ? (LGSPNamedGraph)CreateNamedGraph(modelDllFilename, graphName, parameters) : (LGSPGraph)CreateGraph(modelDllFilename, graphName, parameters);
            }
            else
            {
                throw new ArgumentException("The model filename must be either a .cs or a .dll filename!");
            }

            Type modelType = null;
            try
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.GetInterface("IGraphModel") != null && type.GetInterface("IGraph") == null)
                    {
                        if(modelType != null)
                        {
                            throw new ArgumentException(
                                "The given model contains more than one IGraphModel implementation!");
                        }
                        modelType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                String errorMsg = "";
                foreach(Exception ex in e.LoaderExceptions)
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                if(errorMsg.Length == 0) errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(modelType == null)
            {
                throw new ArgumentException("The given model does not contain an IGraphModel implementation!");
            }

            IGraphModel graphModel = (IGraphModel)Activator.CreateInstance(modelType);
            return named ? new LGSPNamedGraph(this, graphModel, graphName, assemblyName) : new LGSPGraph(this, graphModel, graphName, assemblyName);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            loadedAssemblies.TryGetValue(args.Name, out assembly);
            return assembly;
        }

        internal void AddAssembly(Assembly assembly)
        {
            loadedAssemblies[assembly.FullName] = assembly;
            if(!assemblyHandlerInstalled)
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                assemblyHandlerInstalled = true;
            }
        }

        #region CreateFromSpec helper functions

        private void IgnoreComment(SimpleCharStream charStream)
        {
            char curChar = charStream.ReadChar();
            if(curChar == '/')
            {
                do
                {
                    curChar = charStream.ReadChar();
                }
                while(curChar != '\n' && curChar != '\r');
            }
            else if(curChar == '*')
            {
                bool foundStar = false;
                while(true)
                {
                    curChar = charStream.ReadChar();
                    if(foundStar && curChar == '/') break;
                    foundStar = curChar == '*';
                }
            }
            // otherwise its just not a comment
        }

        private void IgnoreSpace(SimpleCharStream charStream)
        {
            char curChar;
            do
            {
                curChar = charStream.ReadChar();
                if(curChar == '/')
                {
                    IgnoreComment(charStream);
                    curChar = ' ';
                    continue;
                }
            }
            while(curChar == ' ' || curChar == '\t' || curChar == '\n' || curChar == '\r');
            charStream.Backup(1);
        }

        private void IgnoreOther(SimpleCharStream charStream)
        {
            char curChar;
            do
            {
                curChar = charStream.ReadChar();
                if(curChar == '/')
                {
                    IgnoreComment(charStream);
                    curChar = ' ';
                    continue;
                }
            }
            while(!(curChar >= 'A' && curChar <= 'Z' || curChar >= 'a' && curChar <= 'z'
					|| curChar >= '0' && curChar <= '9' || curChar == '_' || curChar == '#'));
            charStream.Backup(1);
        }

        private void IgnoreString(SimpleCharStream charStream)
        {
            IgnoreSpace(charStream);

            char curChar;
            do
            {
                curChar = charStream.ReadChar();
            }
            while(curChar >= 'A' && curChar <= 'Z' || curChar >= 'a' && curChar <= 'z'
					|| curChar >= '0' && curChar <= '9' || curChar == '_' || curChar == '#');
            charStream.Backup(1);
        }

        /// <summary>
        /// Ignores the rest of a string.
        /// </summary>
        /// <param name="charStream">The SimpleCharStream object.</param>
        /// <param name="curChar">The last character read. Set to '\0' to ignore.</param>
        private void IgnoreRest(SimpleCharStream charStream, char curChar)
        {
            while(curChar >= 'A' && curChar <= 'Z' || curChar >= 'a' && curChar <= 'z'
					|| curChar >= '0' && curChar <= '9' || curChar == '_' || curChar == '#')
            {
                curChar = charStream.ReadChar();
            }
            charStream.Backup(1);
        }

        private String ReadString(SimpleCharStream charStream)
        {
            IgnoreSpace(charStream);

            char curChar;
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                curChar = charStream.ReadChar();
                if(!(curChar >= 'A' && curChar <= 'Z' || curChar >= 'a' && curChar <= 'z'
						|| curChar >= '0' && curChar <= '9' || curChar == '_' || curChar == '#'))
                    break;
                sb.Append(curChar);
            }
            charStream.Backup(1);
            return sb.ToString();
        }

        private String ReadQuotedString(SimpleCharStream charStream)
        {
            IgnoreSpace(charStream);

            MatchCharOrThrow(charStream, '\"');

            char curChar;
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                curChar = charStream.ReadChar();
                if(curChar == '\"')
                    break;
                sb.Append(curChar);
            }
            return sb.ToString();
        }

        private void MatchCharOrThrow(SimpleCharStream charStream, char ch)
        {
            char curChar = charStream.ReadChar();
            if(curChar != ch)
                throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar) + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
        }

        private bool MatchString(SimpleCharStream charStream, String str)
        {
            IgnoreSpace(charStream);

            char curChar;
            for(int i = 0; i < str.Length; i++)
            {
                curChar = charStream.ReadChar();
                if(curChar != str[i])
                {
                    charStream.Backup(i + 1);       // unread chars
                    return false;
                }
            }
            curChar = charStream.ReadChar();
            if(curChar != ' ' && curChar != '\t' && curChar != '\n' && curChar != '\r')
            {
                charStream.Backup(str.Length + 1);  // unread chars
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tries to match a string at the current position of a SimpleCharStream.
        /// If the string at the current position does not match, it is skipped.
        /// Here all characters other than A-Z, a-z, 0-9, _, and # are skipped.
        /// </summary>
        /// <param name="charStream">The char stream.</param>
        /// <param name="str">The string to be matched.</param>
        /// <returns>True, iff the string was found.</returns>
        private bool MatchStringOrIgnoreOther(SimpleCharStream charStream, String str)
        {
            IgnoreOther(charStream);

            char curChar;
            for(int i = 0; i < str.Length; i++)
            {
                curChar = charStream.ReadChar();
                if(curChar != str[i])
                {
                    IgnoreRest(charStream, curChar);
                    return false;
                }
            }

            // Does the string really end here?
            curChar = charStream.ReadChar();
            if(curChar != ' ' && curChar != '\t' && curChar != '\n' && curChar != '\r')
            {
                IgnoreRest(charStream, curChar);
                return false;
            }
            return true;
        }

        private String GetPrintable(char ch)
        {
            if(ch >= (char) 32) return ch.ToString();
            else return "\\" + Convert.ToString(ch, 8);
        }

        private void GetNeededFiles(String basePath, String mainActionsName, ref String mainModelName, String grgFilename,
            List<String> neededFiles, Dictionary<String, object> processedFiles)
        {
            if(processedFiles.ContainsKey(grgFilename))
                throw new Exception("Circular include detected with file \"" + grgFilename + "\"!");
            processedFiles[grgFilename] = null;

            if(!File.Exists(grgFilename))
				throw new FileNotFoundException("Included file \"" + grgFilename + "\" does not exist!");

            using(StreamReader reader = new StreamReader(grgFilename))
            {
                SimpleCharStream charStream = new SimpleCharStream(reader);
                char curChar;
                try
                {
                    // check optional header

                    bool needSemicolon = false;

                    // read optional "actions <name>"
                    if(MatchString(charStream, "actions"))
                    {
                        needSemicolon = true;
                        IgnoreString(charStream);                                       // ignore actions name
                    }

					if(MatchString(charStream, "#include"))
					{
						String includedGRG = ReadQuotedString(charStream);
						includedGRG = basePath + FixDirectorySeparators(includedGRG);
						neededFiles.Add(includedGRG);
						GetNeededFiles(basePath, mainActionsName, ref mainModelName, includedGRG, neededFiles, processedFiles);
					}

                    if(MatchString(charStream, "using"))
                    {
                        if(mainModelName != null)
                            throw new Exception("More than one using statement found in '" + grgFilename
                                + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                        bool moreThanOne = false;
                        while(true)
                        {
                            mainModelName = ReadString(charStream);
                            neededFiles.Add(basePath + mainModelName + ".gm");
                            IgnoreSpace(charStream);
                            curChar = charStream.ReadChar();
                            if(curChar == ';') break;
                            else if(curChar == ',')
                                moreThanOne = true;
                            else
                                throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar)
                                    + "' in '" + grgFilename + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                        }
                        if(moreThanOne)
                            mainModelName = mainActionsName;
                    }
                    else if(needSemicolon)
                    {
                        IgnoreSpace(charStream);
                        MatchCharOrThrow(charStream, ';');
                    }

                    // search the rest of the file for include tokens
                    while(true)
                    {
						curChar = charStream.ReadChar();
						if(curChar == '#' && MatchString(charStream, "include"))
						{
							String includedGRG = ReadQuotedString(charStream);
							includedGRG = basePath + FixDirectorySeparators(includedGRG);
							neededFiles.Add(includedGRG);
                            GetNeededFiles(basePath, mainActionsName, ref mainModelName, includedGRG, neededFiles, processedFiles);
						}
						else if(curChar == '\\') charStream.ReadChar();			// skip escape sequences
						else if(curChar == '/') IgnoreComment(charStream);
						else if(curChar == '"')
						{
							while(true)
							{
								curChar = charStream.ReadChar();
								if(curChar == '"') break;
								else if(curChar == '\\') charStream.ReadChar();		// skip escape sequence
							}
						}
                    }
                }
                catch(IOException)
                {
                    // end of file reached
                }
            }
        }

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

		/// <summary>
		/// Retrieves the directory path from a given file path.
		/// Any slashes or backslashes are converted to the correct directory
		/// separator chars for the current platform.
		/// </summary>
		/// <param name="path">A path to a file.</param>
		/// <returns>A path to the directory containing the file.</returns>
		private String GetDir(String path)
        {
            path = FixDirectorySeparators(path);

            int index = path.LastIndexOf(Path.DirectorySeparatorChar);
            if(index == -1) return "";
            else return path.Substring(0, index + 1);
        }

        /// <summary>
        /// Returns the base name of a path name (i.e. no path and no extension).
        /// </summary>
        private String GetPathBaseName(String filename)
        {
            String basename = filename;
            int index = basename.LastIndexOfAny(dirSepChars);
            if(index != -1) basename = basename.Substring(index + 1);
            index = basename.IndexOf('.');
            if(index != -1) basename = basename.Substring(0, index);
            return basename;
        }

        private bool DepsOlder(DateTime oldestOutputTime, List<String> neededFiles)
        {
            // LibGr, LGSPBackend, or GrGen newer than generated files?
            DateTime libGrTime = File.GetLastWriteTime(typeof(IGraph).Assembly.Location);
            DateTime lgspTime = File.GetLastWriteTime(typeof(LGSPBackend).Assembly.Location);
            DateTime grGenTime = File.GetLastWriteTime(
                Path.GetDirectoryName(typeof(LGSPBackend).Assembly.Location) + Path.DirectorySeparatorChar + "grgen.jar");
            if(libGrTime > oldestOutputTime || lgspTime > oldestOutputTime || grGenTime > oldestOutputTime)
                return true;

            // Check used file dates
            foreach(String neededFilename in neededFiles)
            {
                if(!File.Exists(neededFilename))
                {
                    Console.Error.WriteLine("Cannot find used file: \"" + neededFilename + "\"");
                    return true;
                }
                // Needed file newer than libraries?
                DateTime fileTime = File.GetLastWriteTime(neededFilename);
                if(fileTime > oldestOutputTime)
                    return true;
            }

            // Libraries are up to date!
            return false;
        }

        private bool MustGenerate(String grgFilename, out String actionsFilename, out String modelFilename)
        {
            String actionsName = GetPathBaseName(grgFilename);
            String actionsDir = GetDir(grgFilename);

            // Parse grg file and collect information about the used files and the main graph model
            List<String> neededFilenames = new List<String>();
            Dictionary<String, object> processedFiles = new Dictionary<String, object>();
            String mainModelName = null;

            neededFilenames.Add(grgFilename);
            GetNeededFiles(actionsDir, actionsName, ref mainModelName, grgFilename, neededFilenames, processedFiles);

            if(mainModelName == null) mainModelName = "Std";

            actionsFilename = actionsDir + "lgsp-" + actionsName + "Actions.dll";
            modelFilename = actionsDir + "lgsp-" + mainModelName + "Model.dll";

            // Do the libraries exist at all?
            if(!File.Exists(actionsFilename) || !File.Exists(modelFilename))
                return true;

            DateTime actionsTime = File.GetLastWriteTime(actionsFilename);
            DateTime modelTime = File.GetLastWriteTime(modelFilename);
            DateTime oldestOutputTime = actionsTime < modelTime ? actionsTime : modelTime;

            return DepsOlder(oldestOutputTime, neededFilenames);
        }

        private bool MustGenerate(String gmFilename, out String modelFilename)
        {
            String modelName = GetPathBaseName(gmFilename);
            String modelDir = GetDir(gmFilename);

            // Parse grg file and collect information about the used files and the main graph model
            List<String> neededFilenames = new List<String>();
            neededFilenames.Add(gmFilename);

            modelFilename = modelDir + "lgsp-" + modelName + "Model.dll";

            // Does the model library exist at all?
            if(!File.Exists(modelFilename))
                return true;

            DateTime modelTime = File.GetLastWriteTime(modelFilename);
            return DepsOlder(modelTime, neededFilenames);
        }

        #endregion CreateFromSpec helper functions

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph and LGSPActions instance from the specified specification file.
        /// If the according dlls do not exist or are out of date, the needed processing steps are performed automatically.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <param name="flags">Specifies how the specification is to be processed; only KeepGeneratedFiles and CompileWithDebug are taken care of!</param>
        /// <param name="externalAssemblies">List of external assemblies to reference.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateFromSpec(String grgFilename, String graphName, ProcessSpecFlags flags, List<String> externalAssemblies, bool named,
            out LGSPGraph newGraph, out LGSPActions newActions)
        {
            if(!File.Exists(grgFilename))
                throw new FileNotFoundException("The rule specification file \"" + grgFilename + "\" does not exist!", grgFilename);

            String actionsFilename;
            String modelFilename;

            if(MustGenerate(grgFilename, out actionsFilename, out modelFilename))
                LGSPGrGen.ProcessSpecification(grgFilename, flags, externalAssemblies.ToArray());

            newGraph = named ? (LGSPNamedGraph)CreateNamedGraph(modelFilename, graphName) : (LGSPGraph)CreateGraph(modelFilename, graphName);
            newActions = LGSPActions.LoadActions(actionsFilename, newGraph);
        }

        public void CreateFromSpec(string grgFilename, string graphName, ProcessSpecFlags flags, List<String> externalAssemblies, 
            out IGraph newGraph, out BaseActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, graphName, flags, externalAssemblies, false, out graph, out actions);
            newGraph = graph;
            newActions = actions;
        }

        public void CreateNamedFromSpec(string grgFilename, string graphName, ProcessSpecFlags flags, List<String> externalAssemblies,
            out INamedGraph newGraph, out BaseActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, graphName, flags, externalAssemblies, true, out graph, out actions);
            newGraph = (LGSPNamedGraph)graph;
            newActions = actions;
        }

        /// <summary>
        /// Creates a new LGSPGraph and LGSPActions instance from the specified specification file.
        /// If the according dlls do not exist or are out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateFromSpec(String grgFilename,
            out LGSPGraph newGraph, out LGSPActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, GetNextGraphName(), ProcessSpecFlags.UseNoExistingFiles, new List<String>(), false,
                out graph, out actions);
            newGraph = graph;
            newActions = actions;
        }

        /// <summary>
        /// Creates a new LGSPNamedGraph and LGSPActions instance from the specified specification file.
        /// If the according dlls do not exist or are out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="newGraph">Returns the new named graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateNamedFromSpec(String grgFilename,
            out LGSPNamedGraph newGraph, out LGSPActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, GetNextGraphName(), ProcessSpecFlags.UseNoExistingFiles, new List<String>(), true,
                out graph, out actions);
            newGraph = (LGSPNamedGraph)graph;
            newActions = actions;
        }

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <param name="flags">Specifies how the specification is to be processed; only KeepGeneratedFiles and CompileWithDebug are taken care of!</param>
        /// <param name="externalAssemblies">List of external assemblies to reference.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPGraph or LGSPNamedGraph instance.</returns>
        public LGSPGraph CreateFromSpec(String gmFilename, String graphName, ProcessSpecFlags flags, List<String> externalAssemblies, bool named)
        {
            if(!File.Exists(gmFilename))
                throw new FileNotFoundException("The model specification file \"" + gmFilename + "\" does not exist!", gmFilename);

            String modelFilename;
            if(MustGenerate(gmFilename, out modelFilename))
                LGSPGrGen.ProcessSpecification(gmFilename, flags, externalAssemblies.ToArray());

            return CreateGraph(modelFilename, graphName, named);
        }

        public IGraph CreateFromSpec(String gmFilename, String graphName, ProcessSpecFlags flags, List<String> externalAssemblies)
        {
            return CreateFromSpec(gmFilename, graphName, flags, externalAssemblies, false);
        }

        public INamedGraph CreateNamedFromSpec(String gmFilename, String graphName, ProcessSpecFlags flags, List<String> externalAssemblies)
        {
            return (LGSPNamedGraph)CreateFromSpec(gmFilename, graphName, flags, externalAssemblies, true);
        }

        /// <summary>
        /// Creates a new LGSPGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPGraph instance.</returns>
        public LGSPGraph CreateFromSpec(String gmFilename)
        {
            return CreateFromSpec(gmFilename, GetNextGraphName(), ProcessSpecFlags.UseNoExistingFiles, new List<String>(), false);
        }

        /// <summary>
        /// Creates a new LGSPNamedGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="newGraph">Returns the new named graph.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPNamedGraph instance.</returns>
        public LGSPNamedGraph CreateNamedFromSpec(String gmFilename)
        {
            return (LGSPNamedGraph)CreateFromSpec(gmFilename, GetNextGraphName(), ProcessSpecFlags.UseNoExistingFiles, new List<String>(), true);
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
        public void ProcessSpecification(string specPath, string destDir, string intermediateDir, ProcessSpecFlags flags, params String[] externalAssemblies)
        {
            LGSPGrGen.ProcessSpecification(specPath, destDir, intermediateDir, flags, externalAssemblies);
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public void ProcessSpecification(string specPath)
        {
            LGSPGrGen.ProcessSpecification(specPath, ProcessSpecFlags.UseNoExistingFiles, new String[0]);
        }
    }
}
