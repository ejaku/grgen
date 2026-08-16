using System.Collections.Generic;
using System.Diagnostics;

internal static class MyCollectionHelper
{
    // emulates an AddAll from a Java collection (why is this not available in the TangibleCollectionHelper? maybe TODO: merge with the TangibleCollectionHelper)
    public static void AddAll<T, TExt>(this ICollection<T> collection, ICollection<TExt> otherCollection) where TExt : T
    {
        if(collection == null)
            throw new System.NullReferenceException();
        if(otherCollection == null)
            throw new System.NullReferenceException();

        foreach(TExt element in otherCollection)
        {
            collection.Add(element);
        }
    }
}
