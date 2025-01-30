/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.IO;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.grShell;

namespace ShellExample
{
    // just start in Debug mode, will execute Mutex10.grs
    // in Release mode, a shell console is opened for interactive use, you may paste the content of Mutex10.grs line-by-line
    public class ShellExample
    {
        static int Main(string[] args)
        {
            String scriptFilename = null;
            int errorCode = 0; // 0==success, the return value

            if(args.Length > 0)
                scriptFilename = args[0];

            TextReader reader;
            bool showPrompt;
            bool readFromConsole;

            errorCode = DetermineAndOpenInputSource(ref scriptFilename,
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
            driver.showIncludes = false;
            impl.nonDebugNonGuiExitOnError = false;

            try
            {
                driver.conditionalEvaluationResults.Push(true);

                while(!driver.Quitting && !driver.Eof)
                {
                    if(showPrompt)
                        ConsoleUI.outWriter.Write("> ");

                    bool success = shell.ParseShellCommand();

                    errorCode = HandleEofOrErrorIfNonConsoleShell(ref scriptFilename, success,
                        shell, driver,
                        ref reader, ref showPrompt, ref readFromConsole);
                    if(errorCode != 0)
                        return errorCode;
                }

                driver.conditionalEvaluationResults.Pop();
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("exit due to " + e.Message);
                errorCode = -2;
            }
            finally
            {
                impl.Cleanup();
            }

            return errorCode;
        }

        private static int DetermineAndOpenInputSource(ref String scriptFilename,
            out TextReader reader, out bool showPrompt, out bool readFromConsole)
        {
            if(scriptFilename != null)
            {
                try
                {
                    reader = new StreamReader(scriptFilename);
                }
                catch(Exception e)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unable to read file \"" + scriptFilename + "\": " + e.Message);
                    reader = null;
                    showPrompt = false;
                    readFromConsole = false;
                    return -1;
                }
                scriptFilename = null; // become an interactive shell
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

        private static int HandleEofOrErrorIfNonConsoleShell(ref String scriptFilename, bool success,
            GrShell shell, GrShellDriver driver,
            ref TextReader reader, ref bool showPrompt, ref bool readFromConsole)
        {
            if(readFromConsole || (!driver.Eof && success))
                return 0;

            if(scriptFilename != null)
            {
                TextReader newReader;
                try
                {
                    newReader = new StreamReader(scriptFilename);
                }
                catch(Exception e)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unable to read file \"" + scriptFilename + "\": " + e.Message);
                    return -1;
                }
                scriptFilename = null; // become an interactive shell
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
