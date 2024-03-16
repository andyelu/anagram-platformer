using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            tile.letterBankScript = this; // Sets the letterBankScript reference in each Tile
        }
        fillArr();
        n = 6;

    }


    // later add a text file contianing anagrams on each line, then we can replace the code
    // below and instead add a thing that selects a random line and scrambles those letters

    private void fillArr()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            int randomNum = UnityEngine.Random.Range(0, 26);
            char letter = (char)('A' + randomNum);
            letters[i].SetLetter(letter);
        }


    }

    public void addLetter(char letter)
    {
        if (n >= 6)
        {
            Debug.Log("arr full");
            return;
        }

        letters[n].SetLetter(letter);
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
                References.ValidAnagramScript.addLetter(letters[index].letter);
                removeLetterAtIndex(index);
                Debug.Log(References.ValidAnagramScript.tiles);
            }
            
        }
    }


}
