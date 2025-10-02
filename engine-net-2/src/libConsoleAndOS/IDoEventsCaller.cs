/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libConsoleAndOS
{
    /// <summary>
    /// interface that allows to execute the WindowsForms message loop (Application.DoEvents).
    /// </summary>
    public interface IDoEventsCaller
    {
        void DoEvents();
    }
}
