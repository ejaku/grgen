/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GrShellBenchmarker
{
    class Program
    {
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

        static void Main(string[] args)
        {
            int totaltime = 0;
            int totalanalyzetime = 0;
            int totalgenmatchertime = 0;
            int totalbenchtime = 0;
            int N = 10;
            bool error = false;
            Dictionary<char, String> scriptVars = new Dictionary<char, String>();
            String grsFile = null;

            for(int i = 0; i < args.Length && !error; i++)
            {
                if(args[i][0] == '-')
                {
                    switch(args[i])
                    {
                        case "--help":
                            error = true;
                            break;

                        case "--times":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.Error.WriteLine("No <n> specified for --times option!");
                                break;
                            }
                            if(!int.TryParse(args[++i], out N))
                            {
                                error = true;
                                Console.Error.WriteLine("<n> must be an integer for --times option!");
                                break;
                            }
                            break;

                        default:
                            if(args[i].Length == 2 && char.IsUpper(args[i][1]))
                            {
                                char varName = args[i][1];
                                if(scriptVars.ContainsKey(varName))
                                {
                                    error = true;
                                    Console.Error.WriteLine("The script variable \"" + varName + "\" has already been defined!");
                                    break;
                                }
                                if(i + 1 >= args.Length)
                                {
                                    error = true;
                                    Console.Error.WriteLine("No <str> specified for script variable \"" + varName + "\"!");
                                    break;
                                }
                                scriptVars[varName] = args[++i];
                            }
                            else
                            {
                                error = true;
                                Console.Error.WriteLine("Illegal option: " + args[i]);
                            }
                            break;
                    }
                }
                else if(grsFile != null)
                {
                    error = true;
                    Console.Error.WriteLine("GRS-file already provided!");
                }
                else grsFile = args[i];
            }

            if(error || grsFile == null)
            {
                Console.Error.WriteLine("Usage: GrShellBenchmarker [OPTIONS] <GRS-file>\n"
                    + "Options:\n"
                    + "  --help                     Displays this usage info\n"
                    + "  --times <n>                Sets the number of times to execute the benchmark\n"
                    + "                             (default: " + N + ")\n"
                    + "  -<uppercase letter> <str>  Replaces e.g. \"$A$\" in the script by <str>");
                return;
            }

            String grsScript;
            try
            {
                using(StreamReader reader = new StreamReader(grsFile))
                {
                    grsScript = reader.ReadToEnd();
                }
            }
            catch(Exception)
            {
                Console.Error.WriteLine("Unable to read script file \"" + grsFile + "\"!");
                return;
            }

            Console.Write("Script file: \"" + grsFile + "\" Vars: ");

            foreach(KeyValuePair<char, String> var in scriptVars)
            {
                grsScript = grsScript.Replace(var.Key.ToString(), var.Value);
                Console.Write(var.Key + "=" + var.Value + " ");
            }
            grsScript += "\nquit\n";

            int Q02 = (int) (N * 0.2);

            Console.WriteLine();

            int[] time = new int[N];
            int[] matchtime = new int[N];
            int[] rewritetime = new int[N];
            int[] analyzetime = new int[N];
            int[] genmatchertime = new int[N];
            int[] benchtime = new int[N];
            int mintime = int.MaxValue, minbenchtime = int.MaxValue;
            int minanalyzetime = int.MaxValue, mingenmatchertime = int.MaxValue;

            String filename, arguments;
            if(Type.GetType("System.Int32").GetType().ToString() == "System.MonoType")
            {
                filename = "mono";
                arguments = '"' + FixDirectorySeparators(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    + Path.DirectorySeparatorChar + "GrShell.exe" + '"';
            }
            else
            {
                filename = "GrShell.exe";
                arguments = "";
            }

            try
            {
                bool first = true;
                for(int i = 0; i < N; i++)
                {
                    int startms = Environment.TickCount;

                    Process grshell = new Process();
                    grshell.StartInfo.UseShellExecute = false;
                    grshell.StartInfo.RedirectStandardOutput = true;
                    grshell.StartInfo.FileName = filename;
                    grshell.StartInfo.RedirectStandardInput = true;
                    grshell.StartInfo.Arguments = arguments;
                    grshell.Start();

                    grshell.StandardInput.Write(grsScript);
                    StreamReader reader = grshell.StandardOutput;

                    if(first)
                    {
                        Console.WriteLine("Executing warmup run...");
                        reader.ReadToEnd();
                        grshell.Close();
                        first = false;
                        i--;
                        Console.WriteLine("Starting benchmark:\n{0,4}{1,8}{2,8}{3,8}{4,8}", "", "total", "analyze", "gensp", "bench");
                        continue;
                    }

                    time[i] = 0;
                    matchtime[i] = 0;
                    rewritetime[i] = 0;
                    analyzetime[i] = 0;
                    genmatchertime[i] = 0;
                    String str;
                    while((str = reader.ReadLine()) != null)
                    {
                        if(str.Length == 0) continue;
                        if(str.StartsWith("Executing Graph Rewrite Sequence done"))
                        {
                            String[] strs = str.Split(' ');
                            int curtime = int.Parse(strs[6]);
                            time[i] += curtime;
                            totaltime += curtime;

                            if(reader.ReadLine() == null) break;        // matches found
                            if(reader.ReadLine() == null) break;        // rewrites performed
                        }
                        else if(str.StartsWith("Graph "))
                        {
                            String[] strs = str.Split(' ');
                            int curanalyzetime = int.Parse(strs[strs.Length - 2]);
                            analyzetime[i] += curanalyzetime;
                            totalanalyzetime += curanalyzetime;
                        }
                        else if(str.StartsWith("Searchplan "))
                        {
                            String[] strs = str.Split(' ');
                            int curgenmatchertime = int.Parse(strs[strs.Length - 2]);
                            genmatchertime[i] += curgenmatchertime;
                            totalgenmatchertime += curgenmatchertime;
                        }
                    }
                    grshell.Close();

                    benchtime[i] = Environment.TickCount - startms;
                    totalbenchtime += benchtime[i];

//                    Console.WriteLine("{0,2}. time: {1,6}  match: {2,6}  rewritetime = {3,6}  benchtime = {4,6}",
//                        i + 1, time[i], matchtime[i], rewritetime[i], benchtime[i]);
                    Console.WriteLine("{0,2}. {1,8}{2,8}{3,8}{4,8}", i + 1, time[i], analyzetime[i],
                        genmatchertime[i], benchtime[i]);

                    if(time[i] < mintime) mintime = time[i];
                    if(analyzetime[i] < minanalyzetime) minanalyzetime = analyzetime[i];
                    if(genmatchertime[i] < mingenmatchertime) mingenmatchertime = genmatchertime[i];
                    if(benchtime[i] < minbenchtime) minbenchtime = benchtime[i];
                }
                float avgtime = (float) totaltime / N;
                float avganalyzetime = (float) totalanalyzetime / N;
                float avggenmatchertime = (float) totalgenmatchertime / N;
                float avgbenchtime = (float) totalbenchtime / N;

                Array.Sort(time);
                Array.Sort(analyzetime);
                Array.Sort(genmatchertime);
                Array.Sort(benchtime);

                int q02timesum = 0, q02analyzetimesum = 0, q02genmatchertimesum = 0, q02benchtimesum = 0;
                for(int i = Q02; i < N - Q02; i++)
                {
                    q02timesum += time[i];
                    q02analyzetimesum += analyzetime[i];
                    q02genmatchertimesum += genmatchertime[i];
                    q02benchtimesum += benchtime[i];
                }

                float q02time = (float) q02timesum / (N - 2* Q02);
                float q02analyzetime = (float) q02analyzetimesum / (N - 2 * Q02);
                float q02genmatchertime = (float) q02genmatchertimesum / (N - 2 * Q02);
                float q02benchtime = (float) q02benchtimesum / (N - 2 * Q02);

                Console.WriteLine();
                String format = "{0,12}{1,10:0.00}{2,10:0.00}{3,10:0.00}{4,10:0.00}";
                Console.WriteLine(format, "",            "xgrs",  "analyze",      "gensp",          "bench");
                Console.WriteLine(format, "Average",     avgtime, avganalyzetime, avggenmatchertime, avgbenchtime);
                Console.WriteLine(format, "0.2 quantil", q02time, q02analyzetime, q02genmatchertime, q02benchtime);
                Console.WriteLine(format, "Minimum",     mintime, minanalyzetime, mingenmatchertime, minbenchtime);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to benchmark GrShell:\nException occurred: {0}\nStack trace:\n{1}", e.Message, e.StackTrace);
            }
        }
    }
}
