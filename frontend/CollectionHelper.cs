using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.util.collection;


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

    // collection comparison helper - emulates JAVA semantics of comparing collection content, structural comparison of collections instead of reference comparison ------------
    // the O(n) iteration is of course ugly, only comparing the entries of same hash key would be nicer... (maybe todo)

    public static bool ContainsSetKey<T,V>(this IDictionary<ISet<T>, V> map, ISet<T> candidateSet)
    {
        foreach(ISet<T> set in map.Keys)
        {
            if(set.SetEquals(candidateSet))
                return true;
        }
        return false;
    }

    public static ISet<T> GetSetKey<T,V>(this IDictionary<ISet<T>, V> map, ISet<T> candidateSet)
    {
        foreach(ISet<T> set in map.Keys)
        {
            if(set.SetEquals(candidateSet))
                return set;
        }
        return null;
    }

    public static bool ContainsListKey<T,V>(this IDictionary<IList<T>, V> map, IList<T> candidateList) where T : class
    {
        foreach(IList<T> list in map.Keys)
        {
            if(ArrayEquals(list, candidateList))
                return true;
        }
        return false;
    }

    public static IList<T> GetListKey<T,V>(this IDictionary<IList<T>, V> map, IList<T> candidateList) where T : class
    {
        foreach(IList<T> list in map.Keys)
        {
            if(ArrayEquals(list, candidateList))
                return list;
        }
        return null;
    }

    public static bool ArrayEquals<T>(IList<T> this_, IList<T> that) where T : class
    {
        if(this_.Count != that.Count)
            return false;
        for(int i = 0; i < this_.Count; ++i)
        {
            if(this_[i] != that[i])
                return false;
        }
        return true;
    }

    public static bool ContainsPairSetKey<T, V>(this IDictionary<Pair<ISet<T>, ISet<T>>, V> map, Pair<ISet<T>, ISet<T>> candidatePairOfSets)
    {
        foreach(Pair<ISet<T>, ISet<T>> pairOfSets in map.Keys)
        {
            if(pairOfSets.first.SetEquals(candidatePairOfSets.first) && pairOfSets.second.SetEquals(candidatePairOfSets.second))
                return true;
        }
        return false;
    }

    public static Pair<ISet<T>, ISet<T>> GetPairSetKey<T, V>(this IDictionary<Pair<ISet<T>, ISet<T>>, V> map, Pair<ISet<T>, ISet<T>> candidatePairOfSets)
    {
        foreach(Pair<ISet<T>, ISet<T>> pairOfSets in map.Keys)
        {
            if(pairOfSets.first.SetEquals(candidatePairOfSets.first) && pairOfSets.second.SetEquals(candidatePairOfSets.second))
                return pairOfSets;
        }
        return null;
    }
}
