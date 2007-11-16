namespace de.unika.ipd.grGen.lgsp
{
    public enum SearchOperationType
    {
        Void, // void operation; retype to void to delete operation from ssp quickly
        MaybePreset,
        NegPreset,
        Lookup,
        Outgoing,
        Incoming,
        ImplicitSource,
        ImplicitTarget,
        NegativePattern,
        Condition
    };
}
