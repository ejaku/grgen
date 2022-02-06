/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.IO;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Contains helper code for the constant parser, interacts with the constant parser.
    /// </summary>
    public class ConstantParserHelper
    {
        private DebuggerGraphProcessingEnvironment curShellProcEnv = null;

        private readonly TextWriter errOut = System.Console.Error;

        private IBackend curGraphBackend = LGSPBackend.Instance;


        public ConstantParserHelper(DebuggerGraphProcessingEnvironment curShellProcEnv)
        {
            this.curShellProcEnv = curShellProcEnv;
        }

        private bool BackendExists()
        {
            if(curGraphBackend == null)
            {
                errOut.WriteLine("No backend. Select a backend, first.");
                return false;
            }
            return true;
        }

        private bool GraphExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.NamedGraph == null)
            {
                errOut.WriteLine("No graph. Make a new graph, first.");
                return false;
            }
            return true;
        }

        private bool ActionsExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.Actions == null)
            {
                errOut.WriteLine("No actions. Select an actions object, first.");
                return false;
            }
            return true;
        }

        public INamedGraph CurrentGraph
        {
            get
            {
                if(!GraphExists())
                    return null;
                return curShellProcEnv.ProcEnv.NamedGraph;
            }
        }

        public IActions CurrentActions
        {
            get
            {
                if(!ActionsExists())
                    return null;
                return curShellProcEnv.ProcEnv.Actions;
            }
        }

        public String RemoveTypeSuffix(String value)
        {
            if(value.EndsWith("y") || value.EndsWith("Y")
                || value.EndsWith("s") || value.EndsWith("S")
                || value.EndsWith("l") || value.EndsWith("L"))
            {
                return value.Substring(0, value.Length - 1);
            }
            else
                return value;
        }
    }
}
