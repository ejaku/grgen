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

    public static void RemoveAll<T, TGen>(this ICollection<T> collection, ICollection<TGen> otherCollection) where T : TGen
    {
        if(collection == null)
            throw new System.NullReferenceException();
        if(otherCollection == null)
            throw new System.NullReferenceException();

        foreach(TGen element in otherCollection)
        {
            if(element is T)
            {
                T elementAsT = (T)element;
                if(collection.Contains(elementAsT))
                    collection.Remove(elementAsT);
            }
        }
    }

    public static ISet<T> CreateEmptySet<T>()
    {
        return new HashSet<T>(); // TODO: Immutable.ImmutableHashSet<T>.Empty;
    }

    public static ISet<T> CreateSingletonSet<T>(T element)
    {
        HashSet<T> singleton = new HashSet<T>();
        singleton.Add(element);
        return singleton;
    }
}
