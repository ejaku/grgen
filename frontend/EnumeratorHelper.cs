using System.Collections.Generic;
using System.Diagnostics;

internal static class EnumeratorHelper
{
    // emulates the use of Next from a Java enumerator in order to fetch the first element of a collection
    public static T GetFirstElement<T>(ICollection<T> collection)
    {
        IEnumerator<T> it = collection.GetEnumerator();
        bool hasNext = it.MoveNext();
        Debug.Assert(hasNext);
        return it.Current;
    }

    public static T GetFirstElementIfAvailableOrDefault<T>(ICollection<T> collection)
    {
        IEnumerator<T> it = collection.GetEnumerator();
        bool hasNext = it.MoveNext();
        if(hasNext)
            return it.Current;
        else
            return default(T);
    }
}
