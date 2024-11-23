/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public partial class GuiConsoleDebuggerHost : Form, IGuiConsoleDebuggerHost
    {
        public GuiConsoleDebuggerHost(bool twoPane)
        {
            InitializeComponent();
            TwoPane = twoPane;
        }
    }
}
