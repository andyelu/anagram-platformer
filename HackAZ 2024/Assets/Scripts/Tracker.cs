using System;
using System.Collections.Generic;

public static class Tracker
{
    private static HashSet<string> _words = new HashSet<string>();

    public static void Add(string word)
    {
        return _words.Add(word);
    }

    public static void Reset()
    {
        _words.Clear();
    }
}