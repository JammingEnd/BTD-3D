using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
    {
        if(source == null)
        {
            yield break;
        }

        foreach (var item in source)
        {
            yield return item;
        }
    }
    public static string RemoveWord(this string original, string removedWord)
    {
       return original.Replace(removedWord, "").Replace("  ", " ");
    }
}
