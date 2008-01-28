namespace de.unika.ipd.grGen.lgsp
{
    public enum SearchOperationType
    {
        Void, // void operation; retype to void to delete operation from ssp quickly
        MaybePreset, // preset handed in to action pattern, maybe null (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule)
        NegPreset, // preset handed in to negative pattern, matched in positive pattern (normal preset is converted into this when reaching schedule, but not before)
        PatPreset, // preset handed in to subpattern, never null (might occur in enclosed negative pattern, too, but replaced by neg preset in schedule)
        Lookup, // draw element from graph
        Outgoing, // follow outgoing edge
        Incoming, // follow incoming edge
        ImplicitSource, // get source from edge
        ImplicitTarget, // get target from edge
        NegativePattern, // try to match negative pattern
        Condition // check matched pattern by condition 
    };
}
