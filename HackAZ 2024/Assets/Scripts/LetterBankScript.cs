using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LetterBankScript : MonoBehaviour
{
    public Tile[] letters { get; private set; }
    private int n;

    private void Awake()
    {
        References.LetterBankScript = this;
    }

    void Start()
    {
        letters = GetComponentsInChildren<Tile>();
        
        foreach (Tile tile in letters)
        {
            tile.letterBankScript = this;
        }
        fillArr();
        n = 6;

    }


    // later add a text file contianing anagrams on each line, then we can replace the code
    // below and instead add a thing that selects a random line and scrambles those letters

    private void fillArr()
    {
        int len = DictionaryUtils.bank.Count;
        int randomNum = UnityEngine.Random.Range(0, len);
        string[] keysArray = DictionaryUtils.bank.Keys.ToArray();
        string str = keysArray[randomNum];
        References.currentKey = str;
        string shuffledString = ShuffleString(str);

        for (int i = 0; i < letters.Length; i++)
        {
            char letter = shuffledString[i];
            letters[i].SetLetter(letter);
        }


    }

    // Method to shuffle a string
    public static string ShuffleString(string str)
    {
        System.Random rng = new System.Random();
        char[] array = str.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new String(array);
    }

    public void addLetter(char letter)
    {
        if (n >= 6)
        {
            Debug.Log("arr full");
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].letter == ' ')
            {
                letters[i].SetLetter(letter);
                return;
            }
        }
        

        n++;
    }

    public void removeLetterAtIndex(int index)
    {
        if (letters[index].letter == ' ')
        {
            Debug.Log("already removed");
            return;
        }

        letters[index].SetLetter(' ');
        n--;
    }

    public void OnTileClicked(Tile clickedTile)
    {
        int index = Array.IndexOf(letters, clickedTile);
        if (index != -1)
        {
            Debug.Log("Clicked tile index (BANK): " + index);
            
            if (letters[index].letter != ' ')
            {
                References.CreationRowScript.addLetter(letters[index].letter);
                removeLetterAtIndex(index);
                Debug.Log(References.CreationRowScript.tiles);
            }
            
        }
    }


}
