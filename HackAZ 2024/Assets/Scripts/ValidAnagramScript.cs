using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ValidAnagramScript : MonoBehaviour
{
    private readonly string baseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";

    public void CheckWord(string word)
    {
        StartCoroutine(IsWordValid(word));
    }

    private IEnumerator IsWordValid(string word)
    {
        string url = $"{baseUrl}{word}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Error when checking word: {word}. {webRequest.error}");
                Debug.Log($"Response Code: {webRequest.responseCode}");
                Debug.Log($"Response Body: {webRequest.downloadHandler.text}");
            }
            else
            {
                if (webRequest.responseCode == 200) 
                {
                    Debug.Log($"Word is valid: {word}");
                    References.isValidWord = true;
                }
                else if (webRequest.responseCode == 404) 
                {
                    Debug.Log($"Word is not valid: {word}");
                    References.isValidWord = false;
                }
            }
        }
    }
}
