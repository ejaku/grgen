/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class GrShellMain
    {
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
                    ShowPromptAsNeeded(showPrompt);

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
                ConsoleUI.outWriter.WriteLine("exit due to " + e.Message);
                ConsoleUI.outWriter.WriteLine(e.StackTrace);
                ConsoleUI.outWriter.WriteLine(e.Source);
                errorCode = -2;
            }
            finally
            {
                impl.Cleanup();
            }

            return errorCode;
        }

        private static void PrintVersion()
        {
            ConsoleUI.outWriter.WriteLine(GrShellDriver.VersionString + " (enter \"help\" for a list of commands)");
        }

        private static void ShowPromptAsNeeded(bool showPrompt)
        {
            // form TODO: bring window to front, debugger window could hide it
            if(showPrompt)
                ConsoleUI.outWriter.Write("> ");
        }

        private static void ParseArguments(string[] args,
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
                            ConsoleUI.outWriter.WriteLine("Another command has already been specified with -C!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        if(i + 1 >= args.Length)
                        {
                            ConsoleUI.outWriter.WriteLine("Missing parameter for -C option!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        command = args[i + 1];
                        ConsoleUI.outWriter.WriteLine("Will execute: \"" + command + "\"");
                        ++i;
                    }
                    else if(args[i] == "-N")
                        nonDebugNonGuiExitOnError = true;
                    else if(args[i] == "-SI")
                        showIncludes = true;
                    else if(args[i] == "--help")
                    {
                        ConsoleUI.outWriter.WriteLine("Displays help");
                        showUsage = true;
                        break;
                    }
                    else
                    {
                        ConsoleUI.outWriter.WriteLine("Illegal option: " + args[i]);
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
                            ConsoleUI.outWriter.WriteLine("The script file \"" + args[i] + "\" or \"" + filename + "\" does not exist!");
                            showUsage = true;
                            errorCode = -1;
                            break;
                        }
                    }
                    scriptFilename.Add(filename);
                }
            }
        }

        private static void PrintUsage()
        {
            ConsoleUI.outWriter.WriteLine("Usage: GrShell [-C <command>] [<grs-file>]...");
            ConsoleUI.outWriter.WriteLine("If called without options, GrShell is started awaiting user input. (Type help for help.)");
            ConsoleUI.outWriter.WriteLine("Options:");
            ConsoleUI.outWriter.WriteLine("  -C <command> Specifies a command to be executed >first<. Using");
            ConsoleUI.outWriter.WriteLine("               ';;' as a delimiter it can actually contain multiple shell commands");
            ConsoleUI.outWriter.WriteLine("               Use '#\u00A7' in that case to terminate contained exec.");
            ConsoleUI.outWriter.WriteLine("  -N           non-interactive non-gui shell which exits on error instead of waiting for user input");
            ConsoleUI.outWriter.WriteLine("  -SI          prints to console when includes are entered and exited");
            ConsoleUI.outWriter.WriteLine("  <grs-file>   Includes the grs-file(s) in the given order");
        }

        private static int DetermineAndOpenInputSource(String command, List<String> scriptFilename,
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
                    ConsoleUI.outWriter.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
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
                reader = ConsoleUI.inReader;
                showPrompt = true;
                readFromConsole = true;
            }

            return 0;
        }

        private static int HandleEofOrErrorIfNonConsoleShell(List<String> scriptFilename, bool success, bool nonDebugNonGuiExitOnError, 
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
                    ConsoleUI.outWriter.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
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
                shell.ReInit(ConsoleUI.inReader);
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
