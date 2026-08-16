using System.Collections.Generic;
using System.Diagnostics;

internal static class EnumeratorHelper
{
    // emulates the use of Next from a Java enumerator in order to fetch the first element of a collection
    public static T GetFirstElement<T>(ICollection<T> collection)
    {
        IEnumerator<T> it = collection.GetEnumerator();
        bool elementAvailable = it.MoveNext();
        Debug.Assert(elementAvailable);
        return it.Current;
    }
}
