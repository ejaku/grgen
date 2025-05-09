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
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class GrShellMain
    {
        static int Main(string[] args)
        {
            PrintVersion();

            GrShellConfigurationAndControlState config;
            GrShellComponents components;
            int errorCode = GrShellMainHelper.ConstructShell(args, out config, out components);
            if(errorCode != 0)
                return errorCode;

            return GrShellMainHelper.ExecuteShell(config, components);
        }

        private static void PrintVersion()
        {
            ConsoleUI.outWriter.WriteLine(GrShellDriver.VersionString + " (enter \"help\" for a list of commands)");
        }
    }
}
