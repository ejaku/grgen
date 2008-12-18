/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace benchmarker
{
    enum BenchmarkMode { Normal, GrShell, GrShellMutex, NoSize };

    class Benchmarker
    {
        static void PrintUsage()
        {
            Console.WriteLine("Usage: Benchmarker [OPTIONS] <test application> [test app params]");
            Console.WriteLine("Default mode: The test app will get a size parameter as the last parameter");
            Console.WriteLine("Options:");
            Console.WriteLine(" -grshell       GrShell mode");
            Console.WriteLine(" -grshellmutex  GrShell Mutex mode");
            Console.WriteLine(" -nosize        No size mode");
            Console.WriteLine(" -start <size>  Size the benchmark begins with");
            Console.WriteLine(" -end <size>    Size the benchmark ends with");
            Console.WriteLine(" -step <size>   The step size of the benchmark");
            Console.WriteLine(" -firstverbose  Displays the output of the first test app invocation");
        }

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                PrintUsage();
                return;
            }

            BenchmarkMode mode = BenchmarkMode.Normal;
            String basearguments = null, arguments = null;
            String filename = null;
            bool firstVerbose = false;
            int startSize = 100000;
            int endSize = 1000000;
            int stepSize = 100000;

            for(int i = 0; i < args.Length; i++)
            {
                if(args[i][0] == '-')
                {
                    if(args[i] == "-grshellmutex")
                    {
                        if(mode != BenchmarkMode.Normal)
                        {
                            Console.Error.WriteLine("Another mode has already been specified!");
                            PrintUsage();
                            return;
                        }
                        if(i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Test application not specified!");
                            PrintUsage();
                            return;
                        }

                        mode = BenchmarkMode.GrShellMutex;
                    }
                    else if(args[i] == "-grshell")
                    {
                        if(mode != BenchmarkMode.Normal)
                        {
                            Console.Error.WriteLine("Another mode has already been specified!");
                            PrintUsage();
                            return;
                        }
                        if(i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Test application not specified!");
                            PrintUsage();
                            return;
                        }

                        mode = BenchmarkMode.GrShell;
                    }
                    else if(args[i] == "-nosize")
                    {
                        if(mode != BenchmarkMode.Normal)
                        {
                            Console.Error.WriteLine("Another mode has already been specified!");
                            PrintUsage();
                            return;
                        }
                        mode = BenchmarkMode.NoSize;
                        startSize = 0;
                        endSize = 0;
                        stepSize = 1;
                    }
                    else if(args[i] == "-start")
                    {
                        if(i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Start size not specified!");
                            PrintUsage();
                            return;
                        }
                        startSize = Int32.Parse(args[++i]);
                    }
                    else if(args[i] == "-end")
                    {
                        if(i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("End size not specified!");
                            PrintUsage();
                            return;
                        }
                        endSize = Int32.Parse(args[++i]);
                    }
                    else if(args[i] == "-step")
                    {
                        if(i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Step size not specified!");
                            PrintUsage();
                            return;
                        }
                        stepSize = Int32.Parse(args[++i]);
                    }
                    else if(args[i] == "-firstverbose")
                    {
                        firstVerbose = true;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal option: \"" + args[i] + "\"");
                        PrintUsage();
                        return;
                    }
                }
                else
                {
                    filename = args[i];
                    basearguments = String.Join(" ", args, i + 1, args.Length - i - 1) + " ";
                    break;
                }
            }

            Console.Error.WriteLine("\"" + filename + "\": " + mode + " (" + startSize
                + " - " + endSize + ") += " + stepSize);

            int BIGN = 4;
            int N = 10;

//            int BIGN = 2;
//            int N = 2;

            Console.WriteLine("{0,10};{1,10};{2,10};{3,10}", "size", "time", "benchtime", "memory");

            for(int bigi = 0; bigi < BIGN; bigi++)
            {
                Console.Error.WriteLine(bigi + "");
                
                for(int SIZE = startSize; SIZE <= endSize; SIZE += stepSize)
                {
                    if(mode == BenchmarkMode.GrShellMutex)
                    {
                        arguments = basearguments + "-C \"select backend '../bin/lgspBackend.dll';;"
//                            + "new graph '..\\lib\\lgsp-MutexModel.dll' 'mutex graph';;"
//                            + "select actions '..\\lib\\lgsp-MutexActions.dll';;"
                            + "new graph '../lib/lgsp-MutexPimpedModel.dll' 'mutex graph';;"
                            + "select actions '../lib/lgsp-MutexPimpedActions.dll';;"
                            + "new p1:Process;;"
                            + "new p2:Process;;"
                            + "new p1-:next->p2;;"
                            + "new p2-:next->p1;;"
//                            + "grs newRule{" + (SIZE - 2) + "} ; mountRule ; requestRule{" + SIZE + "};;"
//                            + "custom graph analyze;;"
//                            + "custom actions gen_searchplan takeRule releaseRule giveRule;;"
//                            + "grs (takeRule ; releaseRule ; giveRule){" + SIZE + "}\";;"
                            + "grs newRule{" + (SIZE - 2) + "} ; mountRule ; requestRule{" + SIZE + "} ;"
                            + "(takeRule ; releaseRule ; giveRule){" + SIZE + "};;"
                            + "quit\"";
                    }
                    else if(mode == BenchmarkMode.GrShell) arguments = basearguments;
                    else arguments = basearguments + SIZE;

                    if(firstVerbose)
                    {
                        Console.WriteLine("basearguments = >>>" + basearguments + "<<<");
                        Console.WriteLine("arguments = >>>" + arguments + "<<<");
                    }

                    Process benchTarget = new Process();
                    benchTarget.StartInfo.UseShellExecute = false;
                    benchTarget.StartInfo.RedirectStandardOutput = !firstVerbose;
                    benchTarget.StartInfo.FileName = filename;
                    benchTarget.StartInfo.Arguments = arguments;
                    benchTarget.Start();

                    long peakPagedMem = 0;
                    do
                    {
                        try
                        {
                            benchTarget.Refresh();
                            peakPagedMem = benchTarget.PeakPagedMemorySize64;       // TODO: Not supported by Mono yet...
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Exception occurred while accessing PeakVirtualMemorySize64: " + ex.Message);
                        }
                    }
                    while(!benchTarget.WaitForExit(50));

                    try
                    {
                        for(int i = 0; i < N; i++)
                        {
                            int startms = Environment.TickCount;

                            benchTarget = new Process();
                            benchTarget.StartInfo.UseShellExecute = false;
                            benchTarget.StartInfo.RedirectStandardOutput = true;
                            benchTarget.StartInfo.FileName = filename;
                            benchTarget.StartInfo.Arguments = arguments;
                            benchTarget.Start();

                            StreamReader reader = benchTarget.StandardOutput;

                            int time = 0;
                            String str;
                            while((str = reader.ReadLine()) != null)
                            {
                                if(mode == BenchmarkMode.GrShell || mode == BenchmarkMode.GrShellMutex)
                                {
                                    if(str.Length > 0 && (str[str.Length - 1] == ':' || str[str.Length - 1] == '.') && str[str.Length - 2] == 's'
                                        && str[str.Length - 3] == 'm' && str[str.Length - 4] == ' ')
                                    {
                                        int timeStartIndex = str.LastIndexOf(' ', str.Length - 5);
                                        String timeStr = str.Substring(timeStartIndex + 1, str.Length - 5 - timeStartIndex);
                                        int curTime = Int32.Parse(timeStr);
                                        time += curTime;
                                    }
                                }
                                else if(str.EndsWith(" ms"))
                                {
                                    int timeStartIndex = str.LastIndexOf(' ', str.Length - 4);
                                    String timeStr = str.Substring(timeStartIndex + 1, str.Length - 4 - timeStartIndex);
                                    int curTime = Int32.Parse(timeStr);
                                    time += curTime;
                                }
                            }
                            benchTarget.Close();

                            int benchtime = Environment.TickCount - startms;

                            Console.WriteLine("{0,10};{1,10};{2,10};{3,10}", SIZE, time, benchtime, peakPagedMem / 1024);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Unable to benchmark \"" + filename + "\":\nException occurred: " + e.Message
                            + "\nStack trace:\n" + e.StackTrace);
                    }
                }
            }
        }
    }
}
