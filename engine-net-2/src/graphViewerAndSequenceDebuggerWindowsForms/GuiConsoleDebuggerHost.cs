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
    public partial class GuiConsoleDebuggerHost : Form, IGuiConsoleDebuggerHost
    {
        IDebugger debugger;

        public GuiConsoleDebuggerHost(bool twoPane)
        {
            InitializeComponent();
            TwoPane = twoPane;
            theGuiConsoleControl.Select(); // ensure it receives keyboard input
        }

        // IGuiConsoleDebuggerHost ----------------------------------

        public ITwinConsoleUICombinedConsole GuiConsoleControl
        {
            get { return theGuiConsoleControl; }
        }
        public ITwinConsoleUICombinedConsole OptionalGuiConsoleControl
        {
            get { return theOptionalGuiConsoleControl; }
        }

        public bool TwoPane
        {
            get { return theOptionalGuiConsoleControl.Visible; }
            set
            {
                if(value)
                {
                    theOptionalGuiConsoleControl.Visible = true;
                    theOptionalGuiConsoleControl.EnableClear = true;
                    theOptionalSplitter.Visible = true;
                }
                else
                {
                    theOptionalGuiConsoleControl.Visible = false;
                    theOptionalGuiConsoleControl.EnableClear = false;
                    theOptionalSplitter.Visible = false;
                }
            }
        }

        //public void Show(); by the Form

        //public void Close(); by the Form
        
        public IDebugger Debugger
        {
            get { return debugger; }
            set { debugger = value; }
        }

        // ----------------------------------

        private void GuiConsoleDebuggerHost_FormClosed(object sender, FormClosedEventArgs e)
        {
            theGuiConsoleControl.Cancel();
        }
    }
}
