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
        /// Preset handed in to action pattern, maybe null
        /// (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule).
        /// </summary>
        MaybePreset,
        /// <summary>
        /// Preset handed in to negative pattern, matched in positive pattern
        /// (normal preset is converted into this when reaching schedule, but not before).
        /// </summary>
        NegPreset,
        /// <summary>
        /// Preset handed in to subpattern, never null
        /// (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule).
        /// </summary>
        SubPreset,
        /// <summary>
        /// Draw element from graph.
        /// </summary>
        Lookup,
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
        /// Try to match negative pattern.
        /// </summary>
        NegativePattern,
        /// <summary>
        /// Check matched pattern by condition.
        /// </summary>
        Condition
    };
}
