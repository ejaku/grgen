/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using de.unika.ipd.grGen.libConsoleAndOS;

namespace de.unika.ipd.grGen.grShell
{
    // TODO: errorOutWriter
    public class GrShellConfigurationAndControlState
    {
        public String command;
        public List<String> scriptFilenames; // list of files not yet processed/to be processed, is filled and then reduced filename by filename
        public bool showUsage;
        public bool nonDebugNonGuiExitOnError;
        public bool showIncludes;

        public bool showPrompt;
        public bool readFromConsole;

        public GrShellConfigurationAndControlState()
        {
            Init();
        }

        public void Init()
        {
            command = null;
            scriptFilenames = new List<String>();
            showUsage = false;
            nonDebugNonGuiExitOnError = false;
            showIncludes = false;

            showPrompt = true;
            readFromConsole = true;
        }
    }

    public class GrShellComponents
    {
        public TextReader reader;

        public GrShell shell;
        public GrShellImpl shellImpl;
        public IGrShellImplForDriver impl;
        public GrShellDriver driver;
    }

    public class GrShellMainHelper
    {
        public static int ConstructShell(string[] args, out GrShellConfigurationAndControlState config, out GrShellComponents components)
        {
            int errorCode = 0; // 0==success

            config = new GrShellConfigurationAndControlState();
            components = null;
            errorCode = ParseArguments(args, ref config);

            if(config.showUsage)
            {
                PrintUsage();
                return errorCode;
            }

            components = new GrShellComponents();
            errorCode = DetermineAndOpenInputSource(config, components);
            if(errorCode != 0)
                return errorCode;

            CreateAndWireComponents(components);
            components.driver.showIncludes = config.showIncludes;
            components.impl.nonDebugNonGuiExitOnError = config.nonDebugNonGuiExitOnError;

            return errorCode;
        }

        public static int ExecuteShell(GrShellConfigurationAndControlState config, GrShellComponents components)
        {
            int errorCode = 0; // 0==success

            try
            {
                components.driver.conditionalEvaluationResults.Push(true);

                while(!components.driver.Quitting && !components.driver.Eof)
                {
                    ShowPromptAsNeeded(config.showPrompt);

                    bool success = components.shell.ParseShellCommand();

                    errorCode = HandleEofOrErrorIfNonConsoleShell(success, config, components);
                    if(errorCode != 0)
                        return errorCode;
                }

                components.driver.conditionalEvaluationResults.Pop();
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
                components.impl.Cleanup();
            }

            return errorCode;
        }

        public static void ShowPromptAsNeeded(bool showPrompt)
        {
            // form TODO: bring window to front, debugger window could hide it
            if(showPrompt)
                ConsoleUI.outWriter.Write("> ");
        }

        private static int ParseArguments(string[] args, ref GrShellConfigurationAndControlState config)
        {
            int errorCode = 0; // 0==success

            config.Init();

            for(int i = 0; i < args.Length; ++i)
            {
                if(args[i][0] == '-')
                {
                    if(args[i] == "-C")
                    {
                        if(config.command != null)
                        {
                            ConsoleUI.outWriter.WriteLine("Another command has already been specified with -C!");
                            errorCode = -1;
                            config.showUsage = true;
                            break;
                        }
                        if(i + 1 >= args.Length)
                        {
                            ConsoleUI.outWriter.WriteLine("Missing parameter for -C option!");
                            errorCode = -1;
                            config.showUsage = true;
                            break;
                        }
                        config.command = args[i + 1];
                        ConsoleUI.outWriter.WriteLine("Will execute: \"" + config.command + "\"");
                        ++i;
                    }
                    else if(args[i] == "-N")
                        config.nonDebugNonGuiExitOnError = true;
                    else if(args[i] == "-SI")
                        config.showIncludes = true;
                    else if(args[i] == "--help")
                    {
                        ConsoleUI.outWriter.WriteLine("Displays help");
                        config.showUsage = true;
                        break;
                    }
                    else
                    {
                        ConsoleUI.outWriter.WriteLine("Illegal option: " + args[i]);
                        config.showUsage = true;
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
                            config.showUsage = true;
                            errorCode = -1;
                            break;
                        }
                    }
                    config.scriptFilenames.Add(filename);
                }
            }

            return errorCode;
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

        private static int DetermineAndOpenInputSource(GrShellConfigurationAndControlState config, GrShellComponents components)
        {
            if(config.command != null)
            {
                components.reader = new StringReader(config.command);
                config.showPrompt = false;
                config.readFromConsole = false;
            }
            else if(config.scriptFilenames.Count != 0)
            {
                try
                {
                    components.reader = new StreamReader((String)config.scriptFilenames[0]);
                }
                catch(Exception e)
                {
                    ConsoleUI.outWriter.WriteLine("Unable to read file \"" + config.scriptFilenames[0] + "\": " + e.Message);
                    components.reader = null;
                    config.showPrompt = false;
                    config.readFromConsole = false;
                    return -1;
                }
                config.scriptFilenames.RemoveAt(0);
                config.showPrompt = false;
                config.readFromConsole = false;
            }
            else
            {
                components.reader = ConsoleUI.inReader;
                config.showPrompt = true;
                config.readFromConsole = true;
            }

            return 0;
        }

        private static void CreateAndWireComponents(GrShellComponents components)
        {
            components.shell = new GrShell(components.reader);
            components.shellImpl = new GrShellImpl();
            components.impl = components.shellImpl;
            components.driver = new GrShellDriver(components.shell, components.impl);
            components.shell.SetImpl(components.shellImpl);
            components.shell.SetDriver(components.driver);
            components.driver.tokenSources.Push(components.shell.token_source);
        }

        public static int HandleEofOrErrorIfNonConsoleShell(bool success,
            GrShellConfigurationAndControlState config, GrShellComponents components)
        {
            if(config.readFromConsole || (!components.driver.Eof && success))
                return 0;

            if(config.nonDebugNonGuiExitOnError && !success)
                return -1;

            if(config.scriptFilenames.Count != 0)
            {
                TextReader newReader;
                try
                {
                    newReader = new StreamReader((String)config.scriptFilenames[0]);
                }
                catch(Exception e)
                {
                    ConsoleUI.outWriter.WriteLine("Unable to read file \"" + config.scriptFilenames[0] + "\": " + e.Message);
                    return -1;
                }
                config.scriptFilenames.RemoveAt(0);
                components.shell.ReInit(newReader);
                components.driver.Eof = false;
                components.reader.Close();
                components.reader = newReader;
            }
            else
            {
                components.shell.ReInit(ConsoleUI.inReader);
                components.driver.tokenSources.Pop();
                components.driver.tokenSources.Push(components.shell.token_source);
                config.showPrompt = true;
                config.readFromConsole = true;
                components.driver.Eof = false;
                components.reader.Close();
            }

            return 0;
        }
    }
}
