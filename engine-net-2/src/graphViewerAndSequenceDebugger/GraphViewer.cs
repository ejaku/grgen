/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class GraphViewer
    {
        internal class ShowGraphParam
        {
            public readonly String ProgramName;
            public readonly String Arguments;
            public readonly String GraphFilename;
            public readonly bool KeepFile;

            public ShowGraphParam(String programName, String arguments, String graphFilename, bool keepFile)
            {
                ProgramName = programName;
                Arguments = arguments;
                GraphFilename = graphFilename;
                KeepFile = keepFile;
            }
        }

        private static readonly string[] dotExecutables = { "dot", "neato", "fdp", "sfdp", "twopi", "circo" };

        /// <summary>
        /// Tells whether the name is one of the dot renderers from graphviz
        /// </summary>
        public static bool IsDotExecutable(String programName)
        {
            foreach(String dotExecutable in dotExecutables)
            {
                if(programName.Equals(dotExecutable, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Shows the graph dumped in dot format with graphviz (one of its renderers specified by programName, plus the arguments.
        /// The .dot and .pgn files are deleted if !keep (the return value is the filename of the dot file).
        /// </summary>
        public static string ShowGraphWithDot(DebuggerGraphProcessingEnvironment debuggerProcEnv, String programName, String arguments, bool keep)
        {
            String filename = GetUniqueFilename("tmpgraph", "dot");

            DOTDumper dumper = new DOTDumper(filename, debuggerProcEnv.ProcEnv.NamedGraph.Name, debuggerProcEnv.VcgFlags);

            GraphDumper.Dump(debuggerProcEnv.ProcEnv.NamedGraph, dumper, debuggerProcEnv.DumpInfo);
            dumper.FinishDump();

            String pngFilename = filename.Substring(0, filename.Length - ".dot".Length) + ".png";
            if(arguments == null || !arguments.Contains("-T"))
                arguments += " -Tpng";
            if(arguments == null || !arguments.Contains("-o"))
                arguments += " -o " + pngFilename;
            Thread t = new Thread(new ParameterizedThreadStart(ShowGraphThread));
            t.Start(new ShowGraphParam(programName, arguments, filename, keep));
            t.Join();

            try
            {
                Process process = Process.Start(pngFilename);
                if(process != null)
                    process.WaitForExit();
                else
                    Thread.Sleep(1000);
            }
            finally
            {
                if(!keep)
                    File.Delete(pngFilename);
            }

            return filename;
        }

        /// <summary>
        /// Shows the graph dumped in vcg format with the renderer specified by programName (typically yComp), plus the arguments.
        /// The .vcg file is deleted if !keep (the return value is the filename of the vcg file).
        /// </summary>
        public static string ShowVcgGraph(DebuggerGraphProcessingEnvironment debuggerProcEnv, String debugLayout, String programName, String arguments, bool keep)
        {
            String filename = GetUniqueFilename("tmpgraph", "vcg");

            VCGDumper dumper = new VCGDumper(filename, debuggerProcEnv.VcgFlags, debugLayout);

            GraphDumper.Dump(debuggerProcEnv.ProcEnv.NamedGraph, dumper, debuggerProcEnv.DumpInfo);
            dumper.FinishDump();

            Thread t = new Thread(new ParameterizedThreadStart(ShowGraphThread));
            t.Start(new ShowGraphParam(programName, arguments, filename, keep));

            return filename;
        }

        private static string GetUniqueFilename(String baseFilename, String filenameSuffix)
        {
            String filename;
            int id = 0;

            do
            {
                filename = "tmpgraph" + id + "." + filenameSuffix;
                id++;
            }
            while(File.Exists(filename));

            return filename;
        }

        /// <summary>
        /// Executes the specified viewer and deletes the dump file after the viewer has exited
        /// </summary>
        /// <param name="obj">A ShowGraphParam object</param>
        private static void ShowGraphThread(object obj)
        {
            ShowGraphParam param = (ShowGraphParam)obj;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(param.ProgramName,
                    (param.Arguments == null) ? param.GraphFilename : (param.Arguments + " " + param.GraphFilename));
                Process viewer = Process.Start(startInfo);
                viewer.WaitForExit();
            }
            catch(Exception e)
            {
                System.Console.Error.WriteLine(e.Message);
            }
            finally
            {
                if(!param.KeepFile)
                    File.Delete(param.GraphFilename);
            }
        }
    }
}