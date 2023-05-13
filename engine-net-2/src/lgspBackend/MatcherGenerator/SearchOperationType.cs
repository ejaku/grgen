/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

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
        /// Has no meaning in search planning, but is easier to handle with fake preset pattern elements there.
        /// Is used afterwards in scheduling to insert the def-initialization after the real preset elements.
        /// </summary>
        DefToBeYieldedTo,
        /// <summary>
        /// Draw element from graph.
        /// </summary>
        Lookup,
        /// <summary>
        /// Pick element from storage, independent from previously matched elements.
        /// </summary>
        PickFromStorage,
        /// <summary>
        /// Pick element from storage, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        PickFromStorageDependent,
        /// <summary>
        /// Map some given input to an output graph element, independent from previously matched elements.
        /// </summary>
        MapWithStorage,
        /// <summary>
        /// Map some given input to an output graph element, depending on a previously matched element.
        /// </summary>
        MapWithStorageDependent,
        /// <summary>
        /// Pick element from index, independent from previously matched elements.
        /// </summary>
        PickFromIndex,
        /// <summary>
        /// Pick element from index, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        PickFromIndexDependent,
        /// <summary>
        /// Pick element by name, independent from previously matched elements.
        /// </summary>
        PickByName,
        /// <summary>
        /// Pick element by name, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        PickByNameDependent,
        /// <summary>
        /// Pick element by unique id, independent from previously matched elements.
        /// </summary>
        PickByUnique,
        /// <summary>
        /// Pick element by unique id, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        PickByUniqueDependent,
        /// <summary>
        /// Cast to new type (i.e. check if type is correct and uncover attributes of that type), needs old element.
        /// </summary>
        Cast,
        /// <summary>
        /// Assign element from old element, needs old element (used for former parameters of inlined subpatterns).
        /// </summary>
        Assign,
        /// <summary>
        /// Check that element is identical to other element (used for former parameters of inlined subpatterns, which were scheduled without assignments).
        /// </summary>
        Identity,
        /// <summary>
        /// Assign var from expression (used for former var parameters of inlined subpatterns).
        /// </summary>
        AssignVar,
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
        /// <summary>
        /// Check for a duplicate match arising from inlining an independent.
        /// </summary>
        InlinedIndependentCheckForDuplicateMatch,
        /// <summary>
        /// Write the preset (node or edge) for the body of a parallelized action, in the head of the parallelized action.
        /// </summary>
        WriteParallelPreset,
        /// <summary>
        /// Preset (node or edge) handed in to the body of a parallelized action.
        /// </summary>
        ParallelPreset,
        /// <summary>
        /// Write the preset of variable type for the body of a parallelized action, in the head of the parallelized action.
        /// To forward var parameters normally only available as parameters outside schedule from head to body.
        /// </summary>
        WriteParallelPresetVar,
        /// <summary>
        /// Preset of variable type handed in to the body of a parallelized action.
        /// To forward var parameters normally only available as parameters outside schedule from head to body.
        /// </summary>
        ParallelPresetVar,
        /// <summary>
        /// Setup a parallelized lookup element in graph operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelLookup,
        /// <summary>
        /// A parallelized lookup element in graph operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelLookup,
        /// <summary>
        /// Setup a parallelized pick element from storage operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelPickFromStorage,
        /// <summary>
        /// A parallelized pick element from storage operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelPickFromStorage,
        /// <summary>
        /// Setup a parallelized pick element from storage operation, depending on a previously matched element (e.g. an attribute owner), in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelPickFromStorageDependent,
        /// <summary>
        /// A parallelized pick element from storage operation, depending on a previously matched element (e.g. an attribute owner), in the body of a parallelized action matcher.
        /// </summary>
        ParallelPickFromStorageDependent,
        /// <summary>
        /// Setup a parallelized pick element from index operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelPickFromIndex,
        /// <summary>
        /// A parallelized pick element from index operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelPickFromIndex,
        /// <summary>
        /// Setup a parallelized pick element from index operation, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        SetupParallelPickFromIndexDependent,
        /// <summary>
        /// A parallelized pick element from index operation, depending on a previously matched element (e.g. an attribute owner).
        /// </summary>
        ParallelPickFromIndexDependent,
        /// <summary>
        /// Setup a parallelized follow outgoing edges of given node operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelOutgoing,
        /// <summary>
        /// A parallelized follow outgoing edges of given node operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelOutgoing,
        /// <summary>
        /// Setup a parallelized follow incoming edges of given node operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelIncoming,
        /// <summary>
        /// A parallelized follow incoming edges of given node operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelIncoming,
        /// <summary>
        /// Setup a parallelized follow outgoing and incoming edges of given node operation, in the head of a parallelized action matcher.
        /// </summary>
        SetupParallelIncident,
        /// <summary>
        /// A parallelized follow outgoing and incoming edges of given node operation, in the body of a parallelized action matcher.
        /// </summary>
        ParallelIncident
    };
}
