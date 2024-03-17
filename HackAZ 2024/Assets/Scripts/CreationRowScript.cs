using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationRowScript : MonoBehaviour
{
    public Tile[] tiles { get; private set; }
    private int n;

    [SerializeField]
    private ValidAnagramScript validAnagramScript;


    private void Awake()
    {
        References.CreationRowScript = this;
    }

    void Start()
    {
        tiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in tiles)
        {
            tile.creationRowScript = this; // Sets the letterBankScript reference in each Tile
            tile.SetLetter(' ');
        }
        //initalizeEmptyBoxes();
        n = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Initiating word validation");
            validateWord();
        }

        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Delete key pressed");
            transferLetterToBank();
        }
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

            if (n != 0 && tiles[index].letter != ' ')
            {
                transferLetterToBank();
            }

        }
    }

    public void transferLetterToBank()
    {
        References.LetterBankScript.addLetter(tiles[n - 1].letter);
        removeLetterAtIndex(n - 1);
    }

    public void validateWord()
    {
        string word = "";
        
        for (int i = 0; i < n; i++)
        {
            word += tiles[i].letter;
        }

        if (!string.IsNullOrWhiteSpace(word) && validAnagramScript != null)
        {
            validAnagramScript.CheckWord(word);
        }
        else
        {
            Debug.Log("No word to validate or ValidAnagramScript not found.");
        }
    }

}
