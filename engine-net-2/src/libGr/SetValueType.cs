/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A dummy type used as value type for dictionaries representing sets.
    /// </summary>
    public abstract class SetValueType : IEquatable<SetValueType>
    {
        public bool Equals(SetValueType other) { return true; }
    }
}
