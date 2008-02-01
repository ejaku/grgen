using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.testBench
{
	class TestBench
	{
		static void ShowDiff()
		{
			Dictionary<String, object> oldResults = new Dictionary<string, object>();
			using(StreamReader reader = new StreamReader("summary_gold.log"))
			{
				String line;
				while((line = reader.ReadLine()) != null)
					oldResults[line] = null;
			}
			using(StreamReader reader = new StreamReader("summary.log"))
			{
				String line;
				while((line = reader.ReadLine()) != null)
					if(!oldResults.ContainsKey(line))
						Console.WriteLine("+" + line);
			}
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

							default:
								Console.WriteLine("Unknown option: " + args[i]);
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

					if(Directory.Exists(outDir))
					{
						if(onlyNew && Directory.GetLastWriteTime(outDir) > Directory.GetLastWriteTime(file)) continue;
						Directory.Delete(outDir, true);
					}
					Directory.CreateDirectory(outDir);

					TextWriter oldOut = Console.Out;

					Console.Write("===> TEST " + file);
					StringWriter log = new StringWriter();
					Console.SetError(log);
					Console.SetOut(log);
					bool failed = false;
					try
					{
						backend.ProcessSpecification(file, outDir + Path.DirectorySeparatorChar, outDir, de.unika.ipd.grGen.libGr.UseExistingKind.None, true, false);
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
						logFile.WriteLine("ABEND  " + file);
					}
					else if(logStr.Contains("ERROR"))
					{	
						Console.WriteLine(" ... ERROR");
						logFile.WriteLine("ERROR  " + file);
					}
					else if(logStr.Contains("Illegal model") || logStr.Contains("Illegal actions"))
					{
						Console.WriteLine(" ... FAILED");
						logFile.WriteLine("FAILED " + file);
					}
					else if(logStr.Contains("WARNING"))
					{
						Console.WriteLine(" ... WARNED");
						logFile.WriteLine("WARNED " + file);
					}
					else if(!failed)
					{
						if(File.Exists(outDir + Path.DirectorySeparatorChar + "printOutput.txt"))
						{
							using(StreamReader sr = new StreamReader(outDir + Path.DirectorySeparatorChar + "printOutput.txt"))
								javaOutput = sr.ReadToEnd();
							if(javaOutput.Contains("WARNING"))
							{
								Console.WriteLine(" ... WARNED");
								logFile.WriteLine("WARNED " + file);
							}
							else
							{
								javaOutput = null;
								Console.WriteLine(" ... OK");
								logFile.WriteLine("OK     " + file);
							}
						}
						else
						{
							Console.WriteLine(" ... OK");
							logFile.WriteLine("OK     " + file);
						}
					}
					else
					{
						Console.WriteLine(" ... ABEND");
						logFile.WriteLine("ABEND  " + file);
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
