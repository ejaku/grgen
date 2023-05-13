/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen base object (base for values of internal, non-node/edge classes)
    /// </summary>
    public interface IBaseObject : IAttributeBearer
    {
        /// <summary>
        /// The BaseObjectType (class) of the object
        /// </summary>
        new BaseObjectType Type { get; }
    }
}
