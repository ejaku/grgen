/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.grGen
{
    class GrGen
    {
        /// <summary>
        /// Returns a string where all "wrong" directory separator chars are replaced by the ones used by the system.
        /// </summary>
        /// <param name="path">The original path string potentially with wrong chars.</param>
        /// <returns>The corrected path string.</returns>
        static String FixDirectorySeparators(String path)
        {
            if(Path.DirectorySeparatorChar != '\\')
                path = path.Replace('\\', Path.DirectorySeparatorChar);
            if(Path.DirectorySeparatorChar != '/')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }

        /// <summary>
        /// Creates an IBackend instance from the given backend.
        /// </summary>
        /// <param name="backendPath">The path to the backend library (.dll.)</param>
        /// <returns>The created IBackend instance.</returns>
        static IBackend LoadBackend(String backendPath)
        {
            backendPath = FixDirectorySeparators(backendPath);

            try
            {
                Assembly assembly = Assembly.LoadFrom(backendPath);
                Type backendType = null;
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.GetInterface("IBackend") != null)
                    {
                        if(backendType != null)
                        {
                            Console.Error.WriteLine("The given backend contains more than one IBackend implementation!");
                            return null;
                        }
                        backendType = type;
                    }
                }
                if(backendType == null)
                {
                    Console.Error.WriteLine("The given backend doesn't contain an IBackend implementation!");
                    return null;
                }
                return (IBackend) Activator.CreateInstance(backendType);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Unable to load backend: {0}", ex.Message);
                return null;
            }
        }

        static int Main(string[] args)
        {
            String specFile = null;
            String dirname = null;
            String destDir = null;
            ProcessSpecFlags flags = ProcessSpecFlags.UseNoExistingFiles;
            IBackend backend = null;
            List<String> externalAssemblies = new List<String>();
            String statisticsPath = null;

            for(int i = 0; i < args.Length; i++)
            {
                if(args[i][0] == '-')
                {
                    switch(args[i])
                    {
                        case "-o":
                            if(i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -o option!");
                                specFile = null;         // show usage
                                break;
                            }
                            destDir = args[++i];
                            if(!Directory.Exists(destDir))
                            {
                                Console.Error.WriteLine("Specified output directory does not exist!");
                                return 1;
                            }
                            if(destDir[destDir.Length - 1] != Path.DirectorySeparatorChar)
                                destDir += Path.DirectorySeparatorChar;
                            break;

                        case "-b":
                            if(i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -b option!");
                                specFile = null;         // show usage
                                break;
                            }
                            backend = LoadBackend(args[++i]);
                            if(backend == null)
                                return 1;
                            Console.WriteLine("Using backend \"" + backend.Name + "\".");
                            break;

                        case "-r":
                            if (i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -r option!");
                                specFile = null;         // show usage
                                break;
                            }
                            externalAssemblies.Add(args[++i]);
                            break;

                        case "-keep":
                            flags |= ProcessSpecFlags.KeepGeneratedFiles;
                            if(specFile != null)                // specFile already specified?
                            {
                                if(i + 1 >= args.Length)        // yes. is there another parameter?
                                    break;
                            }
                            else if(i + 2 >= args.Length)       // no. are there two more parameters?
                                break;
                            if(args[i + 1][0] == '-')           // is the next parameter an option?
                                break;
                            dirname = args[++i];                // no, use it as a gen-dir
                            break;

                        case "-use":
                            if(i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -use option!");
                                specFile = null;         // show usage
                                break;
                            }
                            if(dirname != null)
                            {
                                Console.Error.WriteLine("The -d option may not specify a directory if -use is used!");
                                specFile = null;
                                break;
                            }
                            dirname = args[++i];
                            flags |= ProcessSpecFlags.UseJavaGeneratedFiles;
                            if(!Directory.Exists(dirname))
                            {
                                Console.Error.WriteLine("Illegal directory specified! It does not exist!");
                                return 1;
                            }
                            break;

                        case "-usefull":
                            if(i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -usefull option!");
                                specFile = null;         // show usage
                                break;
                            }
                            if(dirname != null)
                            {
                                Console.Error.WriteLine("The -d option may not specify a directory if -usefull is used!");
                                specFile = null;
                                break;
                            }
                            dirname = args[++i];
                            flags |= ProcessSpecFlags.UseAllGeneratedFiles;
                            if(!Directory.Exists(dirname))
                            {
                                Console.Error.WriteLine("Illegal directory specified! It does not exist!");
                                return 1;
                            }
                            break;

                        case "-debug":
                            flags |= ProcessSpecFlags.CompileWithDebug;
                            break;

                        case "-noattributeevents":
                            flags |= ProcessSpecFlags.NoAttributeEvents;
                            break;

                        case "-noactionevents":
                            flags |= ProcessSpecFlags.NoActionEvents;
                            break;

                        case "-lazynic":
                            flags |= ProcessSpecFlags.LazyNIC;
                            break;

                        case "-noinline":
                            flags |= ProcessSpecFlags.Noinline;
                            break;

                        case "-statistics":
                            if(i + 1 >= args.Length)
                            {
                                Console.Error.WriteLine("Missing parameter for -statistics option!");
                                specFile = null;         // show usage
                                break;
                            }
                            statisticsPath = args[++i];
                            if(!File.Exists(statisticsPath))
                            {
                                Console.Error.WriteLine("Specified statistics file \"" + statisticsPath + "\" does not exist!");
                                return 1;
                            }
                            break;

                        case "-profile":
                            flags |= ProcessSpecFlags.Profile;
                            break;

                        default:
                            Console.Error.WriteLine("Illegal option: " + args[i]);
                            specFile = null;
                            i = args.Length;        // leave for loop
                            break;
                    }
                }
                else if(specFile != null)
                {
                    Console.Error.WriteLine("Two rule specification files specified: \"" + specFile + "\" and \"" + args[i] + "\"");
                    specFile = null;
                    break;
                }
                else specFile = args[i];
            }

            // flags |= ProcessSpecFlags.Profile; // uncomment to test profiling

            if(specFile == null)
            {
                Console.WriteLine(
                      "Usage: GrGen [OPTIONS] <grg-file>[.grg]\n"
                    + "Options:\n"
                    + "  -o <output-dir>       Output directory for the generated assemblies\n"
                    + "  -keep [<gen-dir>]     Don't delete generated files making it possible\n"
                    + "                        to use the files in a C# project directly.\n"
                    + "                        This way you can also debug non-matching rules.\n"
                    + "                        Optionally you can specify a destination directory.\n"
                    + "  -debug                Compiles the assemblies with debug information\n"
                    + "  -r <assembly-path>    Assembly path to reference, i.e. link into\n"
                    + "                        the generated assembly\n"
                    + "  -statistics <path>    Build matchers based on the graph statistics\n"
                    + "                        contained in the specified file\n"
                    + "  -use <existing-dir>   Use old C# files generated by the Java frontend\n"
                    + "                        using the -keep option in the specified directory\n"
                    + "  -usefull <exist-dir>  Use all old C# files generated using the -keep option\n"
                    + "                        in the specified directory\n"
                    + "  -b <backend-dll>      Use the specified backend library\n"
                    + "                        (default: LGSPBackend)\n"
                    + "  -lazynic              Negatives, Independents, and Conditions are only\n"
                    + "                        executed at the end of matching (normally asap)\n"
                    + "  -noinline             disables subpattern inlining\n"
                    + "Optimizing options:\n"
                    + "  -noactionevents       Do not fire action events in the generated code.\n"
                    + "                        Impacts debugging.\n"
                    + "  -noattributeevents    Do not fire attrib. change events in the generated code.\n"
                    + "                        Impacts transactions, recording, debugging.\n"
                    + "  -noperfinfo           Do not try to update the performance info object\n"
                    + "                        counting number of matches and rewrites.");
                return 1;
            }
            if(!File.Exists(specFile))
            {
                if(File.Exists(specFile + ".grg"))
                    specFile += ".grg";
                else
                {
                    Console.Error.WriteLine("The GRG-file \"" + specFile + "\" does not exist!");
                    return 1;
                }
            }

            specFile = FixDirectorySeparators(specFile);

            String specDir;
            int index = specFile.LastIndexOf(Path.DirectorySeparatorChar);
            if(index == -1)
                specDir = "";
            else
            {
                specDir = specFile.Substring(0, index + 1);
                if(!Directory.Exists(specDir))
                {
                    Console.Error.WriteLine("Something is wrong with the directory of the specification file:\n\"" + specDir + "\" does not exist!");
                    return 1;
                }
            }

            if(destDir == null) destDir = specDir;

            if(dirname == null)
            {
                int id = 0;
                do
                {
                    dirname = specDir + "tmpgrgen" + id + "";
                    id++;
                }
                while(Directory.Exists(dirname));
            }
            if(!Directory.Exists(dirname))
            {
                try
                {
                    Directory.CreateDirectory(dirname);
                }
                catch(Exception)
                {
                    Console.Error.WriteLine("Unable to create temporary directory \"" + dirname + "\"!");
                    return 1;
                }
            }

            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
                Console.WriteLine("The generated files will be kept in: " + dirname);

            if(backend == null)
                backend = new LGSPBackend();

            int ret = 0;
            try
            {
                backend.ProcessSpecification(specFile, destDir, dirname, statisticsPath, flags, externalAssemblies.ToArray());
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine((flags & ProcessSpecFlags.CompileWithDebug) != 0 ? ex.ToString() : ex.Message);
                ret = 1;
            }

            if((flags & ProcessSpecFlags.KeepGeneratedFiles) == 0 && (flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
                Directory.Delete(dirname, true);
            return ret;
        }
    }
}
