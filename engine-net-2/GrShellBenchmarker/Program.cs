/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GrShellBenchmarker
{
    class Program
    {
        static void Main(string[] args)
        {
            int totaltime = 0;
            int totalmatchtime = 0;
            int totalrewritetime = 0;
            int totalanalyzetime = 0;
            int totalgenmatchertime = 0;
            int totalbenchtime = 0;
            int size = 80000;
            String testName = "test2";
            int N = 10;

            if(args.Length > 0)
            {
                testName = args[0];

                if(args.Length > 1)
                {
                    if(!int.TryParse(args[1], out size))
                    {
                        Console.WriteLine("Usage: GrShellBenchmarker [<testname> <graph size> [<num iterations>]");
                        return;
                    }
                    if(args.Length > 2)
                    {
                        if(!int.TryParse(args[2], out N))
                        {
                            Console.WriteLine("Usage: GrShellBenchmarker [<testname> <graph size> [<num iterations>]");
                            return;
                        }
                    }
                }
            }

            int Q02 = (int) (N * 0.2);

            Console.WriteLine("Testname: \"{0}\"  Graph size: {1} nodes\n", testName, size);
            Console.WriteLine("{0,4}{1,8}{2,8}{3,8}{4,8}{5,8}{6,8}", "", "total", "match", "rewrite", "analyze", "gensp", "bench");

            int[] time = new int[N];
            int[] matchtime = new int[N];
            int[] rewritetime = new int[N];
            int[] analyzetime = new int[N];
            int[] genmatchertime = new int[N];
            int[] benchtime = new int[N];
            int mintime = int.MaxValue, minmatchtime = int.MaxValue;
            int minrewritetime = int.MaxValue, minbenchtime = int.MaxValue;
            int minanalyzetime = int.MaxValue, mingenmatchertime = int.MaxValue;

            try
            {
                for(int i = 0; i < N; i++)
                {
                    int startms = Environment.TickCount;

                    Process grshell = new Process();
                    grshell.StartInfo.UseShellExecute = false;
                    grshell.StartInfo.RedirectStandardOutput = true;
                    grshell.StartInfo.FileName = "grShell.exe";
                    grshell.StartInfo.Arguments = testName + " " + size;
//                    grshell.StartInfo.WorkingDirectory = "..\\..\\..\\release";
                    grshell.Start();

                    StreamReader reader = grshell.StandardOutput;

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

                            if((str = reader.ReadLine()) == null) break;

                            strs = str.Split(' ');
                            int curmatchtime = int.Parse(strs[6]);
                            matchtime[i] += curmatchtime;
                            totalmatchtime += curmatchtime;

                            if((str = reader.ReadLine()) == null) break;
                            
                            strs = str.Split(' ');
                            int currewritetime = int.Parse(strs[6]);
                            rewritetime[i] += currewritetime;
                            totalrewritetime += currewritetime;
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
                    Console.WriteLine("{0,2}. {1,8}{2,8}{3,8}{4,8}{5,8}{6,8}", i + 1, time[i], matchtime[i], rewritetime[i], analyzetime[i],
                        genmatchertime[i], benchtime[i]);

                    if(time[i] < mintime) mintime = time[i];
                    if(matchtime[i] < minmatchtime) minmatchtime = matchtime[i];
                    if(rewritetime[i] < minrewritetime) minrewritetime = rewritetime[i];
                    if(analyzetime[i] < minanalyzetime) minanalyzetime = analyzetime[i];
                    if(genmatchertime[i] < mingenmatchertime) mingenmatchertime = genmatchertime[i];
                    if(benchtime[i] < minbenchtime) minbenchtime = benchtime[i];
                }
                float avgtime = (float) totaltime / N;
                float avgmatchtime = (float) totalmatchtime / N;
                float avgrewritetime = (float) totalrewritetime / N;
                float avganalyzetime = (float) totalanalyzetime / N;
                float avggenmatchertime = (float) totalgenmatchertime / N;
                float avgbenchtime = (float) totalbenchtime / N;

                Array.Sort(time);
                Array.Sort(matchtime);
                Array.Sort(rewritetime);
                Array.Sort(analyzetime);
                Array.Sort(genmatchertime);
                Array.Sort(benchtime);

                int q02timesum = 0, q02matchtimesum = 0, q02rewritetimesum = 0, q02analyzetimesum = 0, q02genmatchertimesum = 0, q02benchtimesum = 0;
                for(int i = Q02; i < N - Q02; i++)
                {
                    q02timesum += time[i];
                    q02matchtimesum += matchtime[i];
                    q02rewritetimesum += rewritetime[i];
                    q02analyzetimesum += analyzetime[i];
                    q02genmatchertimesum += genmatchertime[i];
                    q02benchtimesum += benchtime[i];
                }

                float q02time = (float) q02timesum / (N - 2* Q02);
                float q02matchtime = (float) q02matchtimesum / (N - 2 * Q02);
                float q02rewritetime = (float) q02rewritetimesum / (N - 2 * Q02);
                float q02analyzetime = (float) q02analyzetimesum / (N - 2 * Q02);
                float q02genmatchertime = (float) q02genmatchertimesum / (N - 2 * Q02);
                float q02benchtime = (float) q02benchtimesum / (N - 2 * Q02);

                Console.WriteLine();
                Console.WriteLine("{0,12}{1,10:#.00}{2,10:#.00}{3,10:#.00}{4,10:#.00}{5,10:#.00}{6,10:#.00}",
                    "", "total", "match", "rewrite", "analyze", "gensp", "bench");
                Console.WriteLine("{0,12}{1,10:#.00}{2,10:#.00}{3,10:#.00}{4,10:#.00}{5,10:#.00}{6,10:#.00}",
                    "Average", avgtime, avgmatchtime, avgrewritetime, avganalyzetime, avggenmatchertime, avgbenchtime);
                Console.WriteLine("{0,12}{1,10:#.00}{2,10:#.00}{3,10:#.00}{4,10:#.00}{5,10:#.00}{6,10:#.00}",
                    "0.2 quantil", q02time, q02matchtime, q02rewritetime, q02analyzetime, q02genmatchertime, q02benchtime);
                Console.WriteLine("{0,12}{1,10:#.00}{2,10:#.00}{3,10:#.00}{4,10:#.00}{5,10:#.00}{6,10:#.00}",
                    "Minimum", mintime, minmatchtime, minrewritetime, minanalyzetime, mingenmatchertime, minbenchtime);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to benchmark GrShell:\nException occurred: {0}\nStack trace:\n{1}", e.Message, e.StackTrace);
            }
        }
    }
}
