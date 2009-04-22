using System;
using System.IO;

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
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(IGraph graph, String exportFilename)
        {
            if(exportFilename.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                GXLExport.Export(graph, exportFilename);
            else if (exportFilename.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase))
                GRSExport.Export(graph, exportFilename);
            else
                throw new NotSupportedException("File format not supported");
        }

        /// <summary>
        /// Imports the given graph from a file with the given filename.
        /// The format is determined by the file extension.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="modelOverride">If not null, overrides the filename of the graph model to be used.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        public static IGraph Import(String importFilename, String modelOverride, IBackend backend)
        {
            if(importFilename.EndsWith(".gxl", StringComparison.InvariantCultureIgnoreCase))
                return GXLImport.Import(importFilename, modelOverride, backend);
            else if (importFilename.EndsWith(".grs", StringComparison.InvariantCultureIgnoreCase))
                return porter.GRSImporter.Import(importFilename, modelOverride, backend);
            else if(importFilename.EndsWith(".ecore", StringComparison.InvariantCultureIgnoreCase))
                return ECoreImport.Import(importFilename, modelOverride, backend);
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
    }
}
