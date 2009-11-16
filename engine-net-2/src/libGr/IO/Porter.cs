/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Import and export support for graphs.
    /// </summary>
    public class Porter
    {
        /// <summary>
        /// Exports the given graph to a file with the given filename.
        /// The format is determined by the file extension.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="filenameParameters">The names of the files to be exported.
        /// The first must be a filename, the following may be used for giving export parameters
        /// (in fact currently no exporter supports multiple files).</param>
        public static void Export(IGraph graph, List<String> filenameParameters)
        {
            String first = ListGet(filenameParameters, 0);
            if(first.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                GXLExport.Export(graph, first);
            else if (first.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase))
                GRSExport.Export(graph, first, ListGet(filenameParameters, 1)=="withvariables");
            else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Imports a graph from the given files.
        /// If the filenames only specify a model, the graph is empty.
        /// The format is determined by the file extensions.
        /// Any error will be reported by exception.
        /// </summary>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="filenameParameters">The names of the files to be imported.</param>
        /// <returns>The imported graph.</returns>
        public static IGraph Import(IBackend backend, List<String> filenameParameters)
        {
            String first = ListGet(filenameParameters, 0);
            if(first.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                return GXLImport.Import(first, ListGet(filenameParameters, 1), backend);
            else if(first.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase))
                return porter.GRSImporter.Import(first, ListGet(filenameParameters, 1), backend);
            else if(first.EndsWith(".ecore", StringComparison.InvariantCultureIgnoreCase))
            {
                List<String> ecores = new List<String>();
                String grg = null;
                String xmi = null;
                foreach(String filename in filenameParameters)
                {
                    if(filename.EndsWith(".ecore")) ecores.Add(filename);
                    else if(filename.EndsWith(".grg"))
                    {
                        if(grg != null)
                            throw new NotSupportedException("Only one .grg file supported");
                        grg = filename;
                    }
                    else if(filename.EndsWith(".xmi"))
                    {
                        if(xmi != null)
                            throw new NotSupportedException("Only one .xmi file supported");
                        xmi = filename;
                    }
                }
                return ECoreImport.Import(backend, ecores, grg, xmi);
            }
            else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Imports the given graph from a file with the given filename.
        /// The format is determined by the file extension.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported, 
        ///     the model specification part will be ignored.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        public static IGraph Import(String importFilename, IBackend backend, IGraphModel graphModel)
        {
            if(importFilename.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                return GXLImport.Import(importFilename, backend, graphModel);
            else if (importFilename.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase))
                return porter.GRSImporter.Import(importFilename, backend, graphModel);
            else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Returns the string at the given index, or null if the index is out of bounds.
        /// </summary>
        private static String ListGet(List<String> list, int index)
        {
            if(0 <= index && index < list.Count)
                return list[index];
            return null;
        }
    }
}
