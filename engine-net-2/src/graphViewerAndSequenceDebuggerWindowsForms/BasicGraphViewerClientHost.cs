/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class BasicGraphViewerClientHost : Form, IBasicGraphViewerClientHost
    {
        IBasicGraphViewerClient basicGraphViewerClient;

        public BasicGraphViewerClientHost()
        {
            Size = new System.Drawing.Size((int)(Screen.PrimaryScreen.Bounds.Width * 0.55), (int)(Screen.PrimaryScreen.Bounds.Height * 0.65));
            StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(0, 0);
        }

        public IBasicGraphViewerClient BasicGraphViewerClient
        {
            get { return basicGraphViewerClient; }
            set { basicGraphViewerClient = value; }
        }
    }
}
