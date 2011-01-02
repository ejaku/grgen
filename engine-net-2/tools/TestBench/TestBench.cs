/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.testBench
{
	class TestBench
	{
		static void ShowDiff()
		{
            Dictionary<String, String> fileToStatus = new Dictionary<String, String>();
			using(StreamReader reader = new StreamReader("summary_gold.log"))
			{
				String line;
                while((line = reader.ReadLine()) != null)
                {
                    String status = line.Substring(0, 6);
                    String file = line.Substring(7);
                    fileToStatus[file] = status;
                }
			}
			using(StreamReader reader = new StreamReader("summary.log"))
			{
				String line;
				while((line = reader.ReadLine()) != null)
                {
                    String status = line.Substring(0, 6);
                    String file = line.Substring(7);
                    String oldStatus;
                    if(!fileToStatus.TryGetValue(file, out oldStatus))
                        oldStatus = "      ";

                    if(status != oldStatus)
                        Console.WriteLine(oldStatus + " -> " + status + ": " + file);
                }
            }
		}

        static void Usage()
        {
            Console.WriteLine(
                  "Usage: TestBench [OPTIONS] [<grg-files>]\n\n"
                + "Default for <grg-files> is:\n"
                + "  should_pass" + Path.DirectorySeparatorChar + "*.grg should_warn"
                + Path.DirectorySeparatorChar + "*.grg should_fail" + Path.DirectorySeparatorChar + "*.grg\n\n"
                + "Options:\n"
                + "  -a     Append the new results to summary.log\n"
                + "  -c     Clean, by removing all subsubdirectories ending on \"_out\"\n"
                + "  -d     Shows the current differences between summary_gold.log and summary.log\n"
                + "  -n     Only test, when .grg file is newer than according output folder\n"
                + "         or the output folder does not exist yet\n"
                + "  -v     Write the output of GrGen to the console\n"
                + "  --help Output this usage");
        }

		static void Main(string[] args)
		{
			bool verbose = false, onlyNew = false, append = false;
			int filesStart = -1;
			if(args.Length > 0)
			{
				for(int i = 0; i < args.Length; i++)
				{
					if(args[i][0] == '-')
					{
						switch(args[i])
						{
							case "-a": append = true; break;

							case "-c":
								Console.Write("Cleaning ...");
								foreach(String dir in Directory.GetDirectories("."))
								{
									foreach(String subdir in Directory.GetDirectories(dir, "*_out"))
										Directory.Delete(subdir, true);
								}
								Console.WriteLine(" OK");
								return;

							case "-d":
								ShowDiff();
								return;

							case "-n": onlyNew = true; break;
							case "-v": verbose = true; break;

                            case "--help":
                            case "-?":
                            case "/?":
                                Usage();
                                return;

							default:
								Console.WriteLine("Unknown option: " + args[i] + "\n");
                                Usage();
								return;
						}
					}
					else
					{
						filesStart = i;
						break;
					}
				}
			}
			List<String> files = new List<string>();
			if(filesStart == -1)
			{
				files.AddRange(Directory.GetFiles("should_pass", "*.grg"));
				files.AddRange(Directory.GetFiles("should_warn", "*.grg"));
				files.AddRange(Directory.GetFiles("should_fail", "*.grg"));
			}
			else
			{
				for(int i = filesStart; i < args.Length; i++)
					files.Add(args[i]);
			}

			LGSPBackend backend = new LGSPBackend();

			using(StreamWriter logFile = new StreamWriter("summary.log", append))
			{
				foreach(String file in files)
				{
					if(!file.EndsWith(".grg"))
					{
						Console.WriteLine("Error: Filename does not end with \".grg\": " + file);
						continue;
					}

					if(!File.Exists(file))
					{
						Console.WriteLine("Error: File does not exist: " + file);
						continue;
					}

					String outDir = file.Replace(".grg", "_out");
                    String fileForLog = file.Replace('\\', '/');

					if(Directory.Exists(outDir))
					{
						if(onlyNew && Directory.GetLastWriteTime(outDir) > Directory.GetLastWriteTime(file)) continue;
						Directory.Delete(outDir, true);
					}
					Directory.CreateDirectory(outDir);

					TextWriter oldOut = Console.Out;

                    Console.Write("===> TEST " + fileForLog);
					StringWriter log = new StringWriter();
					Console.SetError(log);
					Console.SetOut(log);
					bool failed = false;
					try
					{
                        backend.ProcessSpecification(file, outDir + Path.DirectorySeparatorChar, outDir, ProcessSpecFlags.KeepGeneratedFiles);
					}
					catch(Exception ex)
					{
						failed = true;
						log.Write(ex.Message);
					}

					Console.SetOut(oldOut);

					String javaOutput = null;
					String logStr = log.ToString();
					if(logStr.Contains("Exception in thread"))
					{
						Console.WriteLine(" ... ABEND");
                        logFile.WriteLine("ABEND  " + fileForLog);
					}
					else if(logStr.Contains("ERROR"))
					{	
						Console.WriteLine(" ... ERROR");
                        logFile.WriteLine("ERROR  " + fileForLog);
					}
					else if(logStr.Contains("Illegal model") || logStr.Contains("Illegal actions"))
					{
						Console.WriteLine(" ... FAILED");
                        logFile.WriteLine("FAILED " + fileForLog);
					}
					else if(!failed)
					{
					    if(logStr.Contains("WARNING"))
					    {
						    Console.WriteLine(" ... WARNED");
                            logFile.WriteLine("WARNED " + fileForLog);
					    }
						else if(File.Exists(outDir + Path.DirectorySeparatorChar + "printOutput.txt"))
						{
							using(StreamReader sr = new StreamReader(outDir + Path.DirectorySeparatorChar + "printOutput.txt"))
								javaOutput = sr.ReadToEnd();
							if(javaOutput.Contains("WARNING"))
							{
								Console.WriteLine(" ... WARNED");
                                logFile.WriteLine("WARNED " + fileForLog);
							}
							else
							{
								javaOutput = null;
								Console.WriteLine(" ... OK");
                                logFile.WriteLine("OK     " + fileForLog);
							}
						}
						else
						{
							Console.WriteLine(" ... OK");
                            logFile.WriteLine("OK     " + fileForLog);
						}
					}
					else
					{
						Console.WriteLine(" ... ABEND");
                        logFile.WriteLine("ABEND  " + fileForLog);
					}

					if(verbose)
					{
						if(javaOutput != null)
							Console.WriteLine(javaOutput);
						Console.WriteLine(log.ToString());
					}
				}
			}
			ShowDiff();
		}
	}
}
