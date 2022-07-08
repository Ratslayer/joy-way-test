using System;
using System.Collections.Generic;

public static class ListExtensionMethods
{
    public static bool TryGet<T>(this List<T> list, Predicate<T> condition, out T result)
    {
        foreach (var item in list)
            if (condition(item))
            {
                result = item;
                return true;
            }
        result = default;
        return false;
    }
}
