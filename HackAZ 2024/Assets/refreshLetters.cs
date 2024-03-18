using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshLetters : MonoBehaviour
{
    public BoxCollider2D col;
    public LetterBankScript letters;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tracker.Reset();
        letters.fillArr("WYDOSR");
        Debug.Log("refreshed");
    }
}
