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
        PatPreset,
        /// <summary>
        /// Draw element from graph.
        /// </summary>
        Lookup,
        /// <summary>
        /// Follow outgoing edge.
        /// </summary>
        Outgoing,
        /// <summary>
        /// Follow incoming edge.
        /// </summary>
        Incoming,
        /// <summary>
        /// Get source from edge.
        /// </summary>
        ImplicitSource,
        /// <summary>
        /// Get target from edge.
        /// </summary>
        ImplicitTarget,
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
