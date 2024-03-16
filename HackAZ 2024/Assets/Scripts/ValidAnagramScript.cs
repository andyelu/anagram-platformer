using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidAnagramScript : MonoBehaviour
{
    public Tile[] tiles { get; private set; }
    private int n;

    private void Awake()
    {
        References.ValidAnagramScript = this;
    }

    void Start()
    {
        tiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in tiles)
        {
            tile.validAnagramScript = this; // Sets the letterBankScript reference in each Tile
        }
        n = 0;

    }

    public void addLetter(char letter)
    {
        if (n >= 6)
        {
            Debug.Log("arr full");
            return;
        }

        tiles[n].SetLetter(letter);
        n++;
    }

    public void removeLetterAtIndex(int index)
    {
        if (tiles[index].letter == ' ')
        {
            Debug.Log("already removed");
            return;
        }

        tiles[index].SetLetter(' ');
        n--;
    }

    public void OnTileClicked(Tile clickedTile)
    {
        int index = Array.IndexOf(tiles, clickedTile);
        if (index != -1)
        {
            Debug.Log("Clicked tile index (ANAGRAM): " + index);

            if (tiles[index].letter != ' ')
            {
                References.LetterBankScript.addLetter(tiles[index].letter);
                removeLetterAtIndex(index);
            }

        }
    }
}
