/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Base class for a single index, the different kinds of indices.
    /// (You must typecheck and cast to the concrete index type for more information).
    /// </summary>
    public interface IIndex
    {
        /// <summary>
        /// The description of the index.
        /// </summary>
        IndexDescription Description { get; }
    }

    /// <summary>
    /// A single attribute index.
    /// </summary>
    public interface IAttributeIndex : IIndex
    {
        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is equal to the value given.
        /// </summary>
        IEnumerable<IGraphElement> Lookup(object value);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the from value specified, or also equal in case of fromEqual, but
        /// lower than the to value specified, or also equal in case of toEqual,
        /// in ascending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupAscendingFromTo(object from, bool fromEqual, object to, bool toEqual);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the from value specified, or also equal in case of fromEqual, 
        /// in ascending order.
        /// Index lookup ends at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupAscendingFrom(object from, bool fromEqual);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the to value specified, or also equal in case of toEqual,
        /// in ascending order.
        /// Index lookup begins at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupAscendingTo(object to, bool toEqual);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the from value specified, or also equal in case of fromEqual, but
        /// higher than the to value specified, or also equal in case of toEqual
        /// in descending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupDescendingFromTo(object from, bool fromEqual, object to, bool toEqual);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the from value specified, or also equal in case of fromEqual, 
        /// in descending order.
        /// Index lookup ends at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupDescendingFrom(object from, bool fromEqual);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the to value specified, or also equal in case of toEqual
        /// in descending order.
        /// Index lookup begins at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupDescendingTo(object to, bool toEqual);
    }

    /// <summary>
    /// The index set applied on a graph.
    /// </summary>
    public interface IIndexSet
    {
        /// <summary>
        /// Returns the index of the given name associated with the graph,
        /// or null in case no index of the given name is known.
        /// </summary>
        IIndex GetIndex(string indexName);
    }
}
