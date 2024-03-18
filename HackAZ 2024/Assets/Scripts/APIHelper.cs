using UnityEngine;
using System.Net;
using System.IO;

public static class APIHelper
{


    public static bool GetStatus(string word)
    {

        string url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        if (res.StatusCode == HttpStatusCode.OK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}