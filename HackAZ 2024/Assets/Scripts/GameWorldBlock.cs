using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameWorldBlock : MonoBehaviour
{
    [SerializeField]
    private bool isClicked = false;
    private BoxCollider2D[] boxes;
    private TextMeshProUGUI[] texts; // To store TextMeshProUGUI components of children

    // Removed the letter property since we now deal with multiple letters

    private void Start()
    {
        boxes = GetComponentsInChildren<BoxCollider2D>();
        texts = GetComponentsInChildren<TextMeshProUGUI>(); // Get all TextMeshProUGUI components in children

        // Ensure we have enough characters in our string for all children
        if (References.currentString.Length >= texts.Length)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (References.counter < References.currentString.Length)
                {
                    // Assign a letter to each TextMeshProUGUI component from the string
                    char letter = References.currentString[References.counter];
                    texts[i].text = letter.ToString();
                    References.counter++; // Move to the next character for the next child
                }
            }
        }
        else
        {
            Debug.LogError("Not enough letters in References.currentString for all children.");
        }
    }

    void Update()
    {
        if (!isClicked)
        {
            // Disable all child colliders
            foreach (var collider in boxes)
            {
                collider.enabled = false;
            }

            // Debug.Log("Moving");
            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
            transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0))
            {
                ResetCounterAndString();
                isClicked = true;
                foreach (var collider in boxes)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    private void ResetCounterAndString()
    {
        References.currentString = string.Empty;
        References.counter = 0;
    }

    // Removed the SetLetter method since it's no longer needed for this use case
}
