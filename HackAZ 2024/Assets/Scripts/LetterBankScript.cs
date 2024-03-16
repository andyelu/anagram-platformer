using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBankScript : MonoBehaviour
{
    char[] letters;

    void Start()
    {
        letters = new char[6];
        fillArr();
        
    }

    private void fillArr()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomNum = Random.Range(0, 27);
            char letter = (char)('a' + randomNum);
            letters[i] = letter;
        }


    }
}
