/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

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
        private readonly Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>(); // TODO: usage? besides help during debugging
        private bool assemblyHandlerInstalled = false;
        private static readonly char[] dirSepChars = new char[] { '/', '\\' };

        private static LGSPBackend instance;
        public static LGSPBackend Instance { get { if(instance == null) instance = new LGSPBackend(); return instance; } }

        public String Name { get { return "lgspBackend"; } }

        public IEnumerable<String> ArgumentNames { get { yield break; } }

        public IGlobalVariables CreateGlobalVariables()
        {
            return new LGSPGlobalVariables();
        }

        private String GetNextGraphName()
        {
            return "graph_" + graphID++;
        }

        public IGraph CreateGraph(IGraphModel graphModel, IGlobalVariables globalVariables, string graphName, params String[] parameters)
        {
            return new LGSPGraph(graphModel, globalVariables, graphName);
        }

        public INamedGraph CreateNamedGraph(IGraphModel graphModel, IGlobalVariables globalVariables, string graphName, params String[] parameters)
        {
            int capacity = 0;
            if(parameters != null)
            {
                foreach(String parameter in parameters)
                {
                    if(parameter.StartsWith("capacity="))
                        capacity = int.Parse(parameter.Substring("capacity=".Length));
                }
            }
            return new LGSPNamedGraph(graphModel, globalVariables, graphName, capacity);
        }

        public IGraph CreateGraph(String modelFilename, IGlobalVariables globalVariables, String graphName, params String[] parameters)
        {
            return CreateGraph(modelFilename, globalVariables, graphName, false, parameters);
        }

        public INamedGraph CreateNamedGraph(String modelFilename, IGlobalVariables globalVariables, String graphName, params String[] parameters)
        {
            return (INamedGraph)CreateGraph(modelFilename, globalVariables, graphName, true, parameters);
        }

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph backend instance with the graph model provided by the graph model file and a name.
        /// </summary>
        /// <param name="modelFilename">Filename of a graph model file.</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <param name="parameters">Backend specific parameters.</param>
        /// <returns>The new IGraph backend instance.</returns>
        public LGSPGraph CreateGraph(String modelFilename, IGlobalVariables globalVariables, String graphName, bool named, params String[] parameters)
        {
            Assembly assembly;

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
                    {
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    }
                    throw new ArgumentException("Illegal model C# source code: " + errorMsg);
                }

                assembly = compResults.CompiledAssembly;
            }
            else if(extension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
            {
                assembly = Assembly.LoadFrom(modelFilename);
                AddAssembly(assembly);
            }
            else if(extension.Equals(".gm", StringComparison.OrdinalIgnoreCase))
            {
                String modelDllFilename;
                if(MustGenerate(modelFilename, null, ProcessSpecFlags.UseNoExistingFiles, out modelDllFilename))
                    LGSPGrGen.ProcessSpecification(modelFilename, null, ProcessSpecFlags.UseNoExistingFiles, new String[0]);
                return named ? (LGSPNamedGraph)CreateNamedGraph(modelDllFilename, globalVariables, graphName, parameters) : (LGSPGraph)CreateGraph(modelDllFilename, globalVariables, graphName, parameters);
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
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
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
                {
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                }
                if(errorMsg.Length == 0)
                    errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(modelType == null)
                throw new ArgumentException("The given model does not contain an IGraphModel implementation!");

            IGraphModel graphModel = (IGraphModel)Activator.CreateInstance(modelType);
            int capacity = 0;
            if(parameters != null)
            {
                foreach(String parameter in parameters)
                {
                    if(parameter.StartsWith("capacity="))
                        capacity = int.Parse(parameter.Substring("capacity=".Length));
                }
            }
            return named ? new LGSPNamedGraph(graphModel, globalVariables, graphName, capacity) : new LGSPGraph(graphModel, globalVariables, graphName);
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
                    if(foundStar && curChar == '/')
                        break;
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
                {
                    break;
                }
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
            if(ch >= (char) 32)
                return ch.ToString();
            else
                return "\\" + Convert.ToString(ch, 8);
        }

        // scans through a grg on the search for model usages and rule file inclusions
        // processedActionFiles are for preventing circular includes, full path
        // processedModelFiles are for determining the amount of models, getting the name of the one model in case it's only one
        private void GetNeededFiles(String basePath, String grgFilename, List<String> neededFiles,
            Dictionary<String, object> processedActionFiles, Dictionary<String, object> processedModelFiles)
        {
            if(processedActionFiles.ContainsKey(grgFilename))
                throw new Exception("Circular include detected with file \"" + grgFilename + "\"!");
            processedActionFiles[grgFilename] = null;

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
						GetNeededFiles(basePath, includedGRG, neededFiles, processedActionFiles, processedModelFiles);
					}

                    if(MatchString(charStream, "#using"))
                    {
                        String modelName = ReadQuotedString(charStream);
                        modelName = basePath + FixDirectorySeparators(modelName);
                        neededFiles.Add(modelName);
                        GetNeededFiles(basePath, modelName, neededFiles, processedModelFiles);
                    }

                    if(MatchString(charStream, "using"))
                    {
                        while(true)
                        {
                            String modelName = ReadString(charStream);
                            neededFiles.Add(basePath + modelName + ".gm");
                            GetNeededFiles(basePath, basePath + modelName + ".gm", neededFiles, processedModelFiles);
                            IgnoreSpace(charStream);
                            curChar = charStream.ReadChar();
                            if(curChar == ';')
                                break;
                            if(curChar != ',')
                            {
                                throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar)
                                    + "' in '" + grgFilename + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                            }
                        }
                    }
                    else if(needSemicolon)
                    {
                        IgnoreSpace(charStream);
                        MatchCharOrThrow(charStream, ';');
                    }

                    // search the rest of the file for includes and usings
                    while(true)
                    {
                        curChar = charStream.ReadChar();
						
                        if(curChar == '#' && MatchString(charStream, "include"))
						{
							String includedGRG = ReadQuotedString(charStream);
							includedGRG = basePath + FixDirectorySeparators(includedGRG);
							neededFiles.Add(includedGRG);
                            GetNeededFiles(basePath, includedGRG, neededFiles, processedActionFiles, processedModelFiles);
						}
                        else if(curChar == '\\')
                            charStream.ReadChar();			// skip escape sequences
						else if(curChar == '/')
                            IgnoreComment(charStream);
						else if(curChar == '"')
						{
							while(true)
							{
								curChar = charStream.ReadChar();
								if(curChar == '"')
                                    break;
								else if(curChar == '\\')
                                    charStream.ReadChar();		// skip escape sequence
							}
						}
                        else if(curChar == '#' && MatchString(charStream, "using"))
                        {
                            String modelName = ReadQuotedString(charStream);
                            modelName = basePath + FixDirectorySeparators(modelName);
                            neededFiles.Add(modelName);
                            GetNeededFiles(basePath, modelName, neededFiles, processedModelFiles);
                        }
                        else if(curChar == 'u' && MatchString(charStream, "sing"))
                        {
                            while(true)
                            {
                                String modelName = ReadString(charStream);
                                neededFiles.Add(basePath + modelName + ".gm");
                                GetNeededFiles(basePath, basePath + modelName + ".gm", neededFiles, processedModelFiles);
                                IgnoreSpace(charStream);
                                curChar = charStream.ReadChar();
                                if(curChar == ';')
                                    break;
                                if(curChar != ',')
                                {
                                    throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar)
                                        + "' in '" + grgFilename + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                                }
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

        // scans through a gm on the search for model usages
        private void GetNeededFiles(String basePath, String modelName, List<String> neededFiles,
            Dictionary<String, object> processedModelFiles)
        {
            processedModelFiles[modelName] = null;

            if(!File.Exists(modelName))
                throw new FileNotFoundException("Used model file \"" + modelName + "\" does not exist!");

            using(StreamReader reader = new StreamReader(modelName))
            {
                SimpleCharStream charStream = new SimpleCharStream(reader);
                char curChar;
                try
                {
                    if(MatchString(charStream, "#using"))
                    {
                        String usedModelName = ReadQuotedString(charStream);
                        usedModelName = basePath + FixDirectorySeparators(usedModelName);
                        neededFiles.Add(usedModelName);
                        GetNeededFiles(basePath, usedModelName, neededFiles, processedModelFiles);
                    }

                    if(MatchString(charStream, "using"))
                    {
                        while(true)
                        {
                            String usedModelName = ReadString(charStream);
                            neededFiles.Add(basePath + usedModelName + ".gm");
                            GetNeededFiles(basePath, basePath + usedModelName + ".gm", neededFiles, processedModelFiles);
                            IgnoreSpace(charStream);
                            curChar = charStream.ReadChar();
                            if(curChar == ';')
                                break;
                            if(curChar != ',')
                            {
                                throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar)
                                    + "' in '" + basePath + modelName + ".gm" + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                            }
                        }
                    }

                    // search the rest of the file for include tokens
                    while(true)
                    {
                        curChar = charStream.ReadChar();
 
                        if(curChar == '\\')
                            charStream.ReadChar();			// skip escape sequences
                        else if(curChar == '/')
                            IgnoreComment(charStream);
                        else if(curChar == '"')
                        {
                            while(true)
                            {
                                curChar = charStream.ReadChar();
                                if(curChar == '"')
                                    break;
                                else if(curChar == '\\')
                                    charStream.ReadChar();		// skip escape sequence
                            }
                        }
                        else if(MatchString(charStream, "#using"))
                        {
                            String usedModelName = ReadQuotedString(charStream);
                            usedModelName = basePath + FixDirectorySeparators(usedModelName);
                            neededFiles.Add(usedModelName);
                            GetNeededFiles(basePath, usedModelName, neededFiles, processedModelFiles);
                        }
                        else if(curChar == 'u' && MatchString(charStream, "sing"))
                        {
                            while(true)
                            {
                                String usedModelName = ReadString(charStream);
                                neededFiles.Add(basePath + usedModelName + ".gm");
                                GetNeededFiles(basePath, basePath + usedModelName + ".gm", neededFiles, processedModelFiles);
                                IgnoreSpace(charStream);
                                curChar = charStream.ReadChar();
                                if(curChar == ';')
                                    break;
                                if(curChar != ',')
                                {
                                    throw new Exception("Parse error: Unexpected token '" + GetPrintable(curChar)
                                        + "' in '" + basePath + modelName + ".gm" + "' at line " + charStream.EndLine + ":" + charStream.EndColumn + "!");
                                }
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
            if(index == -1)
                return "";
            else
                return path.Substring(0, index + 1);
        }

        /// <summary>
        /// Returns the base name of a path name (i.e. no path and no extension).
        /// </summary>
        private String GetPathBaseName(String filename)
        {
            String basename = filename;
            int index = basename.LastIndexOfAny(dirSepChars);
            if(index != -1)
                basename = basename.Substring(index + 1);
            index = basename.IndexOf('.');
            if(index != -1)
                basename = basename.Substring(0, index);
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
                    ConsoleUI.errorOutWriter.WriteLine("Cannot find used file: \"" + neededFilename + "\"");
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

        private bool MustGenerate(String grgFilename, String statisticsPath, ProcessSpecFlags flags, out String actionsFilename, out String modelFilename)
        {
            String actionsName = GetPathBaseName(grgFilename);
            String actionsDir = GetDir(grgFilename);

            // Parse grg file and collect information about the used files and the main graph model
            List<String> neededFilenames = new List<String>();
            neededFilenames.Add(grgFilename);
            if(statisticsPath != null)
                neededFilenames.Add(statisticsPath);

            Dictionary<String, object> processedActionFiles = new Dictionary<String, object>();
            Dictionary<String, object> processedModelFiles = new Dictionary<String, object>();
            GetNeededFiles(actionsDir, grgFilename, neededFilenames, processedActionFiles, processedModelFiles);

            String mainModelName;
            if(processedModelFiles.Count == 0)
                mainModelName = "Std";
            else if(processedModelFiles.Count == 1)
            {
                Dictionary<string, object>.Enumerator enu = processedModelFiles.GetEnumerator();
                enu.MoveNext();
                mainModelName = enu.Current.Key;
            }
            else
                mainModelName = actionsName;

            actionsFilename = actionsDir + "lgsp-" + actionsName + "Actions.dll";
            modelFilename = actionsDir + "lgsp-" + GetPathBaseName(mainModelName) + "Model.dll";

            // Do the libraries exist at all?
            if(!File.Exists(actionsFilename) || !File.Exists(modelFilename))
                return true;

            if(File.Exists(actionsDir + actionsName + "ActionsExternalFunctionsImpl.cs"))
                neededFilenames.Add(actionsDir + actionsName + "ActionsExternalFunctionsImpl.cs");
            if(File.Exists(actionsDir + mainModelName + "ModelExternalFunctionsImpl.cs"))
                neededFilenames.Add(actionsDir + mainModelName + "ModelExternalFunctionsImpl.cs");

            DateTime actionsTime = File.GetLastWriteTime(actionsFilename);
            DateTime modelTime = File.GetLastWriteTime(modelFilename);
            DateTime oldestOutputTime = actionsTime < modelTime ? actionsTime : modelTime;

            return DepsOlder(oldestOutputTime, neededFilenames) || (flags & ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange) == ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange;
        }

        private bool MustGenerate(String gmFilename, String statisticsPath, ProcessSpecFlags flags, out String modelFilename)
        {
            String modelName = GetPathBaseName(gmFilename);
            String modelDir = GetDir(gmFilename);

            // Parse grg file and collect information about the used files and the main graph model
            List<String> neededFilenames = new List<String>();
            neededFilenames.Add(gmFilename);
            if(statisticsPath != null)
                neededFilenames.Add(statisticsPath);

            Dictionary<String, object> processedModelFiles = new Dictionary<String, object>();
            GetNeededFiles(modelDir, gmFilename, neededFilenames, processedModelFiles);

            modelFilename = modelDir + "lgsp-" + modelName + "Model.dll";

            // Does the model library exist at all?
            if(!File.Exists(modelFilename))
                return true;

            if(File.Exists(modelDir + modelName + "ModelExternalFunctionsImpl.cs"))
                neededFilenames.Add(modelDir + modelName + "ModelExternalFunctionsImpl.cs");

            DateTime modelTime = File.GetLastWriteTime(modelFilename);

            return DepsOlder(modelTime, neededFilenames) || (flags & ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange) == ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange;
        }

        #endregion CreateFromSpec helper functions

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph and LGSPActions instance from the specified specification file.
        /// If the according dlls do not exist or are out of date, the needed processing steps are performed automatically.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="flags">Specifies how the specification is to be processed; only KeepGeneratedFiles and CompileWithDebug are taken care of!</param>
        /// <param name="externalAssemblies">List of external assemblies to reference.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <param name="capacity">The initial capacity for the name maps, only used if named (performance optimization, use 0 if unsure).</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateFromSpec(String grgFilename, IGlobalVariables globalVariables, String graphName, String statisticsPath,
            ProcessSpecFlags flags, List<String> externalAssemblies, bool named, int capacity,
            out LGSPGraph newGraph, out LGSPActions newActions)
        {
            if(!File.Exists(grgFilename))
                throw new FileNotFoundException("The rule specification file \"" + grgFilename + "\" does not exist!", grgFilename);

            String actionsFilename;
            String modelFilename;
            if(MustGenerate(grgFilename, statisticsPath, flags, out actionsFilename, out modelFilename))
                LGSPGrGen.ProcessSpecification(grgFilename, statisticsPath, flags, externalAssemblies.ToArray());

            newGraph = named ? (LGSPNamedGraph)CreateNamedGraph(modelFilename, globalVariables, graphName, "capacity=" + capacity.ToString()) : (LGSPGraph)CreateGraph(modelFilename, globalVariables, graphName);
            newActions = LGSPActions.LoadActions(actionsFilename, newGraph);
        }

        public void CreateFromSpec(string grgFilename, IGlobalVariables globalVariables, string graphName, String statisticsPath, ProcessSpecFlags flags, List<String> externalAssemblies, 
            out IGraph newGraph, out IActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, globalVariables, graphName, statisticsPath, flags, externalAssemblies, false, 0, out graph, out actions);
            newGraph = graph;
            newActions = actions;
        }

        public void CreateNamedFromSpec(string grgFilename, IGlobalVariables globalVariables, string graphName, String statisticsPath, 
            ProcessSpecFlags flags, List<String> externalAssemblies, int capacity,
            out INamedGraph newGraph, out IActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, globalVariables, graphName, statisticsPath, flags, externalAssemblies, true, capacity, out graph, out actions);
            newGraph = (LGSPNamedGraph)graph;
            newActions = actions;
        }

        /// <summary>
        /// Creates a new LGSPGraph and LGSPActions instance from the specified specification file.
        /// If the according dlls do not exist or are out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateFromSpec(String grgFilename, IGlobalVariables globalVariables, String statisticsPath,
            out LGSPGraph newGraph, out LGSPActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, globalVariables, GetNextGraphName(), statisticsPath, ProcessSpecFlags.UseNoExistingFiles, new List<String>(), false, 0,
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
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="capacity">The initial capacity for the name maps (performance optimization, use 0 if unsure).</param>
        /// <param name="newGraph">Returns the new named graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        public void CreateNamedFromSpec(String grgFilename, IGlobalVariables globalVariables, String statisticsPath, int capacity,
            out LGSPNamedGraph newGraph, out LGSPActions newActions)
        {
            LGSPGraph graph;
            LGSPActions actions;
            CreateFromSpec(grgFilename, globalVariables, GetNextGraphName(), statisticsPath, ProcessSpecFlags.UseNoExistingFiles, new List<String>(), true, capacity,
                out graph, out actions);
            newGraph = (LGSPNamedGraph)graph;
            newActions = actions;
        }

        /// <summary>
        /// Creates a new LGSPGraph or LGSPNamedGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <param name="statisticsPath">Optional path to a file containing the graph statistics to be used for building the matchers.</param>
        /// <param name="flags">Specifies how the specification is to be processed; only KeepGeneratedFiles and CompileWithDebug are taken care of!</param>
        /// <param name="externalAssemblies">List of external assemblies to reference.</param>
        /// <param name="named">Returns a named graph if true otherwise a non-named graph. You must cast the LGSPGraph returned to the inherited LGSPNamedGraph if named=true.</param>
        /// <param name="capacity">The initial capacity for the name maps (performance optimization, use 0 if unsure).</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPGraph or LGSPNamedGraph instance.</returns>
        public LGSPGraph CreateFromSpec(String gmFilename, IGlobalVariables globalVariables, String graphName, String statisticsPath,
            ProcessSpecFlags flags, List<String> externalAssemblies, bool named, int capacity)
        {
            if(!File.Exists(gmFilename))
                throw new FileNotFoundException("The model specification file \"" + gmFilename + "\" does not exist!", gmFilename);

            String modelFilename;
            if(MustGenerate(gmFilename, statisticsPath, flags, out modelFilename))
                LGSPGrGen.ProcessSpecification(gmFilename, statisticsPath, flags, externalAssemblies.ToArray());

            return CreateGraph(modelFilename, globalVariables, graphName, named, "capacity=" + capacity.ToString());
        }

        public IGraph CreateFromSpec(String gmFilename, IGlobalVariables globalVariables, String graphName, String statisticsPath, 
            ProcessSpecFlags flags, List<String> externalAssemblies)
        {
            return CreateFromSpec(gmFilename, globalVariables, graphName, statisticsPath, flags, externalAssemblies, false, 0);
        }

        public INamedGraph CreateNamedFromSpec(String gmFilename, IGlobalVariables globalVariables, String graphName, String statisticsPath, 
            ProcessSpecFlags flags, List<String> externalAssemblies, int capacity)
        {
            return (LGSPNamedGraph)CreateFromSpec(gmFilename, globalVariables, graphName, statisticsPath, flags, externalAssemblies, true, capacity);
        }

        /// <summary>
        /// Creates a new LGSPGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPGraph instance.</returns>
        public LGSPGraph CreateFromSpec(String gmFilename, IGlobalVariables globalVariables)
        {
            return CreateFromSpec(gmFilename, globalVariables, GetNextGraphName(), null,
                ProcessSpecFlags.UseNoExistingFiles, new List<String>(), false, 0);
        }

        /// <summary>
        /// Creates a new LGSPNamedGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// A name for the graph is automatically generated.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="globalVariables">An object implementing the IGlobalVariables interface, serving as global variables.</param>
        /// <param name="capacity">The initial capacity for the name maps (performance optimization, use 0 if unsure).</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new LGSPNamedGraph instance.</returns>
        public LGSPNamedGraph CreateNamedFromSpec(String gmFilename, IGlobalVariables globalVariables, int capacity)
        {
            return (LGSPNamedGraph)CreateFromSpec(gmFilename, globalVariables, GetNextGraphName(), null,
                ProcessSpecFlags.UseNoExistingFiles, new List<String>(), true, capacity);
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
        public void ProcessSpecification(string specPath, string destDir, string intermediateDir, String statisticsPath, ProcessSpecFlags flags, params String[] externalAssemblies)
        {
            LGSPGrGen.ProcessSpecification(specPath, destDir, intermediateDir, statisticsPath, flags, externalAssemblies);
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public void ProcessSpecification(string specPath)
        {
            LGSPGrGen.ProcessSpecification(specPath, null, ProcessSpecFlags.UseNoExistingFiles, new String[0]);
        }
    }
}
