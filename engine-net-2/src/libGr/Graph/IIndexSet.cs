/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections.Generic;

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
        IEnumerable<IGraphElement> LookupElements(object value);

        /// <summary>
        /// Lookup all graph elements in the index in ascending order.
        /// Index lookup begins at the graph element with the minimum attribute value.
        /// Index lookup ends at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscending();
        
        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher or equal than the from value specified,
        /// in ascending order.
        /// Index lookup ends at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromInclusive(object from);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the from value specified,
        /// in ascending order.
        /// Index lookup ends at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromExclusive(object from);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower or equal than the to value specified,
        /// in ascending order.
        /// Index lookup begins at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingToInclusive(object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the to value specified, 
        /// in ascending order.
        /// Index lookup begins at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingToExclusive(object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher or equal than the from value specified, and
        /// lower or equal than the to value specified, 
        /// in ascending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromInclusiveToInclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher or equal than the from value specified, and
        /// lower than the to value specified,
        /// in ascending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromInclusiveToExclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the from value specified, and
        /// lower or equal than the to value specified, 
        /// in ascending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromExclusiveToInclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the from value specified, and
        /// lower than the to value specified, 
        /// in ascending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsAscendingFromExclusiveToExclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index in descending order.
        /// Index lookup begins at the graph element with the maximum attribute value.
        /// Index lookup ends at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescending();

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower or equal than the from value specified, 
        /// in descending order.
        /// Index lookup ends at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromInclusive(object from);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the from value specified, 
        /// in descending order.
        /// Index lookup ends at the graph element with the minimum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromExclusive(object from);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher or equal than the to value specified, 
        /// in descending order.
        /// Index lookup begins at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingToInclusive(object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// higher than the to value specified, 
        /// in descending order.
        /// Index lookup begins at the graph element with the maximum attribute value.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingToExclusive(object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower or equal than the from value specified, and
        /// higher or equal than the to value specified, 
        /// in descending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromInclusiveToInclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower or equal than the from value specified, and
        /// higher than the to value specified, 
        /// in descending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromInclusiveToExclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the from value specified, and
        /// higher or equal than the to value specified, 
        /// in descending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromExclusiveToInclusive(object from, object to);

        /// <summary>
        /// Lookup all graph elements in the index whose indexed attribute value is:
        /// lower than the from value specified, and
        /// higher than the to value specified, 
        /// in descending order.
        /// </summary>
        IEnumerable<IGraphElement> LookupElementsDescendingFromExclusiveToExclusive(object from, object to);
    }

    /// <summary>
    /// A single incidence count index.
    /// </summary>
    public interface IIncidenceCountIndex : IAttributeIndex
    {
        /// <summary>
        /// Lookup the incidence count of the graph element given.
        /// </summary>
        int GetIncidenceCount(IGraphElement element);
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

    /// <summary>
    /// Base class for uniqueness handlers, only needed as a kind of handle on libGr level, to transfer real implementation objects.
    /// </summary>
    public interface IUniquenessHandler
    {
    }
}
