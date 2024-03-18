using System;
using System.Collections.Generic;
using UnityEngine;

public static class Tracker
{
    private static HashSet<string> _words = new HashSet<string>();

    public static void Add(string word)
    {
        _words.Add(word);
    }

    public static void Reset()
    {
        _words.Clear();
        Debug.Log("Words collection has been reset.");
    }

    public static bool Contains(string word)
    {
        return _words.Contains(word);
    }
}
