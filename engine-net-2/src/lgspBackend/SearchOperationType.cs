/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Describes the type of a search operation.
    /// </summary>
    public enum SearchOperationType
    {
        /// <summary>
        /// Void operation; retype to void to delete operation from ssp quickly.
        /// </summary>
        Void,
        /// <summary>
        /// Preset handed in to action pattern, maybe null, 
        /// in this causing creation of two search plans, one with preset, one with lookup operation
        /// (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule).
        /// </summary>
        ActionPreset,
        /// <summary>
        /// Preset handed in to negative/independent pattern, matched in enclosing pattern
        /// (normal preset is converted into this when reaching schedule, but not before).
        /// </summary>
        NegIdptPreset,
        /// <summary>
        /// Preset handed in to subpattern, never null
        /// (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule).
        /// </summary>
        SubPreset,
        /// <summary>
        /// Def element to be yielded to, postset after matching with match parts of nested/called stuff.
        /// Has no meaning in matching, but it is easier to use them as fake preset pattern elements
        /// in search planning than to introduce special cases in the algorithms and data structures for them.
        /// </summary>
        DefToBeYieldedTo,
        /// <summary>
        /// Draw element from graph.
        /// </summary>
        Lookup,
        /// <summary>
        /// Pick element from storage.
        /// </summary>
        PickFromStorage,
        /// <summary>
        /// Map some given input element to an output graph element.
        /// </summary>
        MapWithStorage,
        /// <summary>
        /// Pick element from storage attribute, needs attribute owner.
        /// </summary>
        PickFromStorageAttribute,
        /// <summary>
        /// Cast to new type (i.e. check if type is correct and uncover attributes of that type), needs old element.
        /// </summary>
        Cast,
        /// <summary>
        /// Follow outgoing edges of given node.
        /// </summary>
        Outgoing,
        /// <summary>
        /// Follow incoming edges of given node.
        /// </summary>
        Incoming,
        /// <summary>
        /// Follow outgoing and incoming edges of given node.
        /// </summary>
        Incident,
        /// <summary>
        /// Get source node from given edge.
        /// </summary>
        ImplicitSource,
        /// <summary>
        /// Get target node from given edge.
        /// </summary>
        ImplicitTarget,
        /// <summary>
        /// Get source and target node from given edge.
        /// </summary>
        Implicit,
        /// <summary>
        /// Check matched pattern by condition.
        /// </summary>
        Condition,
        /// <summary>
        /// All local elements mached, push them to the matches stack for patternpath checking,
        /// serves as a barrier for negative and independent pattern forward scheduling
        /// </summary>
        LockLocalElementsForPatternpath,
        /// <summary>
        /// Try to match negative pattern.
        /// </summary>
        NegativePattern,
        /// <summary>
        /// Try to match independent pattern.
        /// </summary>
        IndependentPattern,
    };
}
