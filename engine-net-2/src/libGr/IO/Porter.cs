/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.IO;
using System.IO.Compression;
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
        /// Currently available are: .grs/.grsi or .gxl.
        /// Optionally suffixed by .gz; in this case they are saved gzipped.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. The .grs/.grsi exporter is capable of exporting a NamedGraph, i.e. including names.</param>
        /// <param name="filenameParameters">The names of the files to be exported.
        /// The first must be a filename, the following may be used for giving export parameters
        /// (in fact currently no exporter supports multiple files).</param>
        public static void Export(IGraph graph, List<String> filenameParameters)
        {
            String first = ListGet(filenameParameters, 0);
            StreamWriter writer = null;
            if (first.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                FileStream filewriter = new FileStream(first, FileMode.OpenOrCreate,  FileAccess.Write);
                writer = new StreamWriter(new GZipStream(filewriter, CompressionMode.Compress));
                first = first.Substring(0, first.Length - 3);
            } else {
                writer = new StreamWriter(first);
            }

            if(first.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase)) {
                GXLExport.Export(graph, writer);
            } else if (first.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase)
                || first.EndsWith(".grsi", StringComparison.InvariantCultureIgnoreCase)) {
                GRSExport.Export(graph, writer, ListGet(filenameParameters, 1)=="withvariables");
            } else if (first.EndsWith(".grg", StringComparison.InvariantCultureIgnoreCase)) {
                GRGExport.Export(graph, writer);
            } else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Imports a graph from the given files.
        /// If the filenames only specify a model, the graph is empty.
        /// The format is determined by the file extensions.
        /// Currently available are: .grs/.grsi or .gxl or .ecore(/.xmi).
        /// Optionally suffixed by .gz; in this case they are expected to be gzipped.
        /// Any error will be reported by exception.
        /// </summary>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="filenameParameters">The names of the files to be imported.</param>
        /// <returns>The imported graph. 
        /// The .grs/.grsi importer returns a NamedGraph. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>
        public static IGraph Import(IBackend backend, List<String> filenameParameters)
        {
            String first = ListGet(filenameParameters, 0);
            StreamReader reader = null;
            if (first.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                FileStream filereader = new FileStream(first, FileMode.Open,  FileAccess.Read);
                reader = new StreamReader(new GZipStream(filereader, CompressionMode.Decompress));
                first = first.Substring(0, first.Length - 3);
            } else {
                reader = new StreamReader(first);
            }

            if(first.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                return GXLImport.Import(reader, ListGet(filenameParameters, 1), backend);
            else if(first.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase)
                        || first.EndsWith(".grsi", StringComparison.InvariantCultureIgnoreCase))
                return porter.GRSImporter.Import(reader, ListGet(filenameParameters, 1), backend);
            else if(first.EndsWith(".ecore", StringComparison.InvariantCultureIgnoreCase))
            {
                List<String> ecores = new List<String>();
                String grg = null;
                String xmi = null;
                bool noPackageNamePrefix = false;
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
                    else if(filename == "nopackagenameprefix")
                    {
                        noPackageNamePrefix = true;
                    }
                }
                return ECoreImport.Import(backend, ecores, grg, xmi, noPackageNamePrefix);
            }
            else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Imports a graph from the given file.
        /// The format is determined by the file extension.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported, 
        ///     the model specification part will be ignored.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        /// <returns>The imported graph. 
        /// The .grs/.grsi importer returns a NamedGraph. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>
        public static IGraph Import(String importFilename, IBackend backend, IGraphModel graphModel)
        {
            if(importFilename.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                return GXLImport.Import(importFilename, backend, graphModel);
            else if (importFilename.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase)
                        || importFilename.EndsWith(".grsi", StringComparison.InvariantCultureIgnoreCase))
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
