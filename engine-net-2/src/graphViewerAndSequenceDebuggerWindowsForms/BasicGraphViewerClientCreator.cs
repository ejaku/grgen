/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class BasicGraphViewerClientCreator : IBasicGraphViewerClientCreator
    {
        public IBasicGraphViewerClient Create(GraphViewerTypes graphViewerType, IBasicGraphViewerClientHost host)
        {
            if(graphViewerType == GraphViewerTypes.MSAGL)
                return new MSAGLClient((System.Windows.Forms.Form)host);
            else
                return null;
        }
    }
}
