/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class GrShellDriver
    {
        public const String VersionString = "GrShell v5.0";

        // stack of token sources, for a new file included, a new token source is created, while the old ones are kept so we can restore its state
        private readonly Stack<GrShellTokenManager> tokenSources = new Stack<GrShellTokenManager>();

        // stack of results of evaluating "if expr commands endif" statements; entire file/session is enclosed in true (safing us from special case handling) 
        private readonly Stack<bool> conditionalEvaluationResults = new Stack<bool>();

        public bool Quitting = false;
        public bool Eof = false;

        private bool showIncludes = false;

        private readonly GrShell grShell;
        private readonly IGrShellImplForDriver impl;


        GrShellDriver(GrShell grShell, IGrShellImplForDriver impl)
        {
            this.grShell = grShell;
            this.impl = impl;
        }

        static int Main(string[] args)
        {
            String command = null;
            List<String> scriptFilename = new List<String>();
            bool showUsage = false;
            bool nonDebugNonGuiExitOnError = false;
            bool showIncludes = false;
            int errorCode = 0; // 0==success, the return value

            PrintVersion();

            ParseArguments(args,
                out command, out scriptFilename, out showUsage,
                out nonDebugNonGuiExitOnError, out showIncludes, out errorCode);

            if(showUsage)
            {
                PrintUsage();
                return errorCode;
            }

            TextReader reader;
            bool showPrompt;
            bool readFromConsole;

            errorCode = DetermineAndOpenInputSource(command, scriptFilename, 
                out reader, out showPrompt, out readFromConsole);
            if(errorCode != 0)
                return errorCode;

            GrShell shell = new GrShell(reader);
            GrShellImpl shellImpl = new GrShellImpl();
            IGrShellImplForDriver impl = shellImpl;
            GrShellDriver driver = new GrShellDriver(shell, impl);
            shell.SetImpl(shellImpl);
            shell.SetDriver(driver);
            driver.tokenSources.Push(shell.token_source);
            driver.showIncludes = showIncludes;
            impl.nonDebugNonGuiExitOnError = nonDebugNonGuiExitOnError;

            try
            {
                driver.conditionalEvaluationResults.Push(true);

                while(!driver.Quitting && !driver.Eof)
                {
                    driver.ShowPromptAsNeeded(showPrompt);

                    bool success = shell.ParseShellCommand();

                    errorCode = HandleEofOrErrorIfNonConsoleShell(scriptFilename, success, nonDebugNonGuiExitOnError,
                        shell, driver,
                        ref reader, ref showPrompt, ref readFromConsole);
                    if(errorCode != 0)
                        return errorCode;
                }

                driver.conditionalEvaluationResults.Pop();
            }
            catch(Exception e)
            {
                Console.WriteLine("exit due to " + e.Message);
                errorCode = -2;
            }
            finally
            {
                impl.Cleanup();
            }

            return errorCode;
        }

        public static void PrintVersion()
        {
            Console.WriteLine(VersionString + " (enter \"help\" for a list of commands)");
        }

        public void ShowPromptAsNeeded(bool ShowPrompt)
        {
            if(ShowPrompt)
                Console.Write("> ");
        }

        public bool Include(String filename, String from, String to)
        {
            try
            {
                if(showIncludes)
                    impl.debugOut.WriteLine("Including " + filename);

                TextReader reader = null;
                if(filename.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                    FileStream filereader = new FileStream(filename, FileMode.Open,  FileAccess.Read);
                    reader = new StreamReader(new GZipStream(filereader, CompressionMode.Decompress));
                }
                else
                    reader = new StreamReader(filename);
                if(from != null || to != null)
                    reader = new FromToReader(reader, from, to);

                using(reader)
                {
                    SimpleCharStream charStream = new SimpleCharStream(reader);
                    GrShellTokenManager tokenSource = new GrShellTokenManager(charStream);
                    tokenSources.Push(tokenSource);

                    try
                    {
                        grShell.ReInit(tokenSource);

                        while(!Quitting && !Eof)
                        {
                            if(!grShell.ParseShellCommand())
                            {
                                impl.errOut.WriteLine("Shell command parsing failed in include of \"" + filename + "\" (at nesting level " + tokenSources.Count + ")");
                                return false;
                            }
                        }
                        Eof = false;
                    }
                    finally
                    {
                        if(showIncludes)
                            impl.debugOut.WriteLine("Leaving " + filename);

                        tokenSources.Pop();
                        grShell.ReInit(tokenSources.Peek());
                    }
                }
            }
            catch(Exception e)
            {
                impl.errOut.WriteLine("Error during include of \"" + filename + "\": " + e.Message);
                return false;
            }

            return true;
        }

        public void Quit()
        {
            impl.QuitDebugMode();

            Quitting = true;

            impl.debugOut.WriteLine("Bye!\n");
        }

        public void ParsedIf(SequenceExpression seqExpr)
        {
            conditionalEvaluationResults.Push(conditionalEvaluationResults.Peek() && impl.Evaluate(seqExpr));
        }

        public void ParsedElse()
        {
            bool conditionalValue = conditionalEvaluationResults.Peek();
            conditionalEvaluationResults.Pop();
            conditionalEvaluationResults.Push(conditionalEvaluationResults.Peek() && !conditionalValue);
        }

        public void ParsedEndif()
        {
            conditionalEvaluationResults.Pop();
        }

        public bool ExecuteCommandLine()
        {
            return conditionalEvaluationResults.Peek();
        }

        static void ParseArguments(string[] args,
            out String command, out List<String> scriptFilename, out bool showUsage,
            out bool nonDebugNonGuiExitOnError, out bool showIncludes, out int errorCode)
        {
            command = null;
            scriptFilename = new List<String>();
            showUsage = false;
            nonDebugNonGuiExitOnError = false;
            showIncludes = false;
            errorCode = 0; // 0==success, the return value

            for(int i = 0; i < args.Length; ++i)
            {
                if(args[i][0] == '-')
                {
                    if(args[i] == "-C")
                    {
                        if(command != null)
                        {
                            Console.WriteLine("Another command has already been specified with -C!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        if(i + 1 >= args.Length)
                        {
                            Console.WriteLine("Missing parameter for -C option!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        command = args[i + 1];
                        Console.WriteLine("Will execute: \"" + command + "\"");
                        ++i;
                    }
                    else if(args[i] == "-N")
                        nonDebugNonGuiExitOnError = true;
                    else if(args[i] == "-SI")
                        showIncludes = true;
                    else if(args[i] == "--help")
                    {
                        Console.WriteLine("Displays help");
                        showUsage = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Illegal option: " + args[i]);
                        showUsage = true;
                        errorCode = -1;
                        break;
                    }
                }
                else
                {
                    String filename = args[i];
                    if(!File.Exists(filename))
                    {
                        filename = filename + ".grs";
                        if(!File.Exists(filename))
                        {
                            Console.WriteLine("The script file \"" + args[i] + "\" or \"" + filename + "\" does not exist!");
                            showUsage = true;
                            errorCode = -1;
                            break;
                        }
                    }
                    scriptFilename.Add(filename);
                }
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: GrShell [-C <command>] [<grs-file>]...");
            Console.WriteLine("If called without options, GrShell is started awaiting user input. (Type help for help.)");
            Console.WriteLine("Options:");
            Console.WriteLine("  -C <command> Specifies a command to be executed >first<. Using");
            Console.WriteLine("               ';;' as a delimiter it can actually contain multiple shell commands");
            Console.WriteLine("               Use '#\u00A7' in that case to terminate contained exec.");
            Console.WriteLine("  -N           non-interactive non-gui shell which exits on error instead of waiting for user input");
            Console.WriteLine("  -SI          prints to console when includes are entered and exited");
            Console.WriteLine("  <grs-file>   Includes the grs-file(s) in the given order");
        }

        static int DetermineAndOpenInputSource(String command, List<String> scriptFilename,
            out TextReader reader, out bool showPrompt, out bool readFromConsole)
        {
            if(command != null)
            {
                reader = new StringReader(command);
                showPrompt = false;
                readFromConsole = false;
            }
            else if(scriptFilename.Count != 0)
            {
                try
                {
                    reader = new StreamReader((String)scriptFilename[0]);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
                    reader = null;
                    showPrompt = false;
                    readFromConsole = false;
                    return -1;
                }
                scriptFilename.RemoveAt(0);
                showPrompt = false;
                readFromConsole = false;
            }
            else
            {
                reader = WorkaroundManager.Workaround.In;
                showPrompt = true;
                readFromConsole = true;
            }

            return 0;
        }

        static int HandleEofOrErrorIfNonConsoleShell(List<String> scriptFilename, bool success, bool nonDebugNonGuiExitOnError, 
            GrShell shell, GrShellDriver driver,
            ref TextReader reader, ref bool showPrompt, ref bool readFromConsole)
        {
            if(readFromConsole || (!driver.Eof && success))
                return 0;

            if(nonDebugNonGuiExitOnError && !success)
                return -1;

            if(scriptFilename.Count != 0)
            {
                TextReader newReader;
                try
                {
                    newReader = new StreamReader((String)scriptFilename[0]);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
                    return -1;
                }
                scriptFilename.RemoveAt(0);
                shell.ReInit(newReader);
                driver.Eof = false;
                reader.Close();
                reader = newReader;
            }
            else
            {
                shell.ReInit(WorkaroundManager.Workaround.In);
                driver.tokenSources.Pop();
                driver.tokenSources.Push(shell.token_source);
                showPrompt = true;
                readFromConsole = true;
                driver.Eof = false;
                reader.Close();
            }

            return 0;
        }
    }
}
