using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static string PersistentDataPath(this string fileName) => string.Concat(Application.persistentDataPath, '/', fileName);

    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        foreach(T item in list)
        {
            action(item);
        }
    }
}
