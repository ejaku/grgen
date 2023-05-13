/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for search program check operations
    /// contains list anchor for operations to execute when check failed
    /// (check is not a search operation, thus the check failed operations are not search nested operations)
    /// </summary>
    abstract class CheckOperation : SearchProgramOperation
    {
        // (nested) operations to execute when check failed
        public SearchProgramList CheckFailedOperations;
    }
}
