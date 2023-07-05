/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class GrShellDriver
    {
        public const String VersionString = "GrShell v6.7";

        // stack of token sources, for a new file included, a new token source is created, while the old ones are kept so we can restore its state
        public readonly Stack<GrShellTokenManager> tokenSources = new Stack<GrShellTokenManager>();

        // stack of results of evaluating "if expr commands endif" statements; entire file/session is enclosed in true (safing us from special case handling) 
        public readonly Stack<bool> conditionalEvaluationResults = new Stack<bool>();

        public bool Quitting = false;
        public bool Eof = false;

        public bool showIncludes = false;

        private readonly GrShell grShell;
        private readonly IGrShellImplForDriver impl;


        public GrShellDriver(GrShell grShell, IGrShellImplForDriver impl)
        {
            this.grShell = grShell;
            this.impl = impl;
        }

        public bool Include(String filename, String from, String to)
        {
            try
            {
                if(showIncludes)
                    ConsoleUI.outWriter.WriteLine("Including " + filename);

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
                                ConsoleUI.errorOutWriter.WriteLine("Shell command parsing failed in include of \"" + filename + "\" (at nesting level " + tokenSources.Count + ")");
                                return false;
                            }
                        }
                        Eof = false;
                    }
                    finally
                    {
                        if(showIncludes)
                            ConsoleUI.outWriter.WriteLine("Leaving " + filename);

                        tokenSources.Pop();
                        grShell.ReInit(tokenSources.Peek());
                    }
                }
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Error during include of \"" + filename + "\": " + e.Message);
                return false;
            }

            return true;
        }

        public void Quit()
        {
            impl.QuitDebugMode();

            Quitting = true;

            ConsoleUI.outWriter.WriteLine("Bye!\n");
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
    }
}
