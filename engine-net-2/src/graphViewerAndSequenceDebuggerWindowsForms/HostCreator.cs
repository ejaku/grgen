/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class HostCreator : IHostCreator
    {
        public IBasicGraphViewerClientHost CreateBasicGraphViewerClientHost()
        {
            return new BasicGraphViewerClientHost();
        }

        public IGuiConsoleDebuggerHost CreateGuiConsoleDebuggerHost(bool twoPane)
        {
            return new GuiConsoleDebuggerHost(twoPane);
        }

        public IGuiDebuggerHost CreateGuiDebuggerHost()
        {
            return new GuiDebuggerHost();
        }
    }
}
