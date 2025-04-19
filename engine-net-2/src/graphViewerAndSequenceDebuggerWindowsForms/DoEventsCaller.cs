/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class DoEventsCaller : IDoEventsCaller
    {
        public void DoEvents()
        {
            Application.DoEvents();
        }
    }
}
