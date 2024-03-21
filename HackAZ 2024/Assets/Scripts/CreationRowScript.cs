using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreationRowScript : MonoBehaviour
{
    public Tile[] tiles { get; private set; }
    private int n;

    public GameObject prefabLenOne;
    public GameObject prefabLenTwo;
    public GameObject prefabLenThree;
    public GameObject prefabLenFour;
    public GameObject prefabLenFive;
    public GameObject prefabLenSix;
    GameObject[] prefabs;

    public Canvas worldSpaceCanvas;
    public Camera mainCamera;
    public TextMeshProUGUI invalidWordText;

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
        prefabs = new GameObject[] { prefabLenOne, prefabLenTwo, prefabLenThree, prefabLenFour, prefabLenFive, prefabLenSix };


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Initiating word validation");
            if (validateWord())
            {
                Vector3 screenPoint = Input.mousePosition;
                screenPoint.z = mainCamera.nearClipPlane;
                Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
                worldPoint.z = 0;
                Debug.Log(worldPoint);

                // place prefab into the game canvas
                setCurrentStringGlobalReference();
                Tracker.Add(References.currentString);
                Instantiate(prefabs[n - 1], worldPoint, Quaternion.identity, worldSpaceCanvas.transform);
                transferAllLettersToBank();
           
            }
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

    public void setCurrentStringGlobalReference()
    {
        string str = "";

        for (int i = 0; i < n; i++)
        {
            str += tiles[i].letter;
        }

        References.currentString = str;
        References.counter = 0;
        Debug.Log("n:" +  n);
        Debug.Log(References.counter);
        Debug.Log(References.currentString);
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

    public void transferAllLettersToBank()
    {
        while (n > 0)
        {
            transferLetterToBank();
        }
    }

    public void transferLetterToBank()
    {
        References.LetterBankScript.addLetter(tiles[n - 1].letter);
        removeLetterAtIndex(n - 1);
    }

    public bool validateWord()
    {
        string word = "";

        for (int i = 0; i < n; i++)
        {
            word += tiles[i].letter;
        }

        if (!string.IsNullOrWhiteSpace(word))
        {
            if (Tracker.Contains(word))
            {
                Debug.Log("in tracker");
                invalidWord(true);
                return false;
            }
            // Check if the dictionary contains the key and if the set associated with this key contains the word.
            if (DictionaryUtils.bank.ContainsKey(References.currentKey) && DictionaryUtils.bank[References.currentKey].Contains(word))
            {
                return true;
            }
        }

        invalidWord(false) ;
        return false;
    }

    private void invalidWord(bool inTracker)
    {
        string errorMsg;
        if (inTracker)
        {
            errorMsg = "you  already  used  that!";
        } else
        {
            errorMsg = "invalid  word!";
        }

        // Start the blink coroutine for each tile
        foreach (Tile tile in tiles)
        {
            StartCoroutine(BlinkTile(tile, errorMsg));
        }
    }

    private IEnumerator BlinkTile(Tile tile, string errorMsg)
    {
        Color originalColor = Color.white;
        Color blinkColor = Color.red;
        Image tileImage = tile.imageComponent; 
        int blinkTimes = 3;
        float blinkDuration = 0.2f;

        // Show invalid word text
        invalidWordText.text = errorMsg;
        invalidWordText.gameObject.SetActive(true);

        for (int i = 0; i < blinkTimes; i++)
        {
            tileImage.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            tileImage.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        invalidWordText.gameObject.SetActive(false);

    }

}
