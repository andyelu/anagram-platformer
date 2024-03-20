using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWorldBlock : MonoBehaviour
{
    //[SerializeField]
    //private Color green = new Color(0.81f, 0.94f, 0.75f, 1);
    private bool isClicked = false;
    private BoxCollider2D[] allColliders;
    private List<BoxCollider2D> boxes;
    private List<BoxCollider2D> triggers;
    private TextMeshProUGUI[] texts;
    private Image[] images;
    public bool playerInTrigger = false;
    private void Start()
    {
        allColliders = GetComponentsInChildren<BoxCollider2D>();
        triggers = new List<BoxCollider2D>();
        boxes = new List<BoxCollider2D>();

        foreach (var collider in allColliders)
        {
            if (collider.isTrigger)
            {
                triggers.Add(collider); // Add to triggers list
            }
            else
            {
                boxes.Add(collider); // Add to solid colliders list
            }
        }

        // initally disable colliders
        foreach (var collider in boxes)
        {
            collider.enabled = false;
        }

        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>(); // Get all TextMeshProUGUI components in children

        if (References.currentString.Length >= texts.Length)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (References.counter < References.currentString.Length)
                {
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
        Debug.Log(playerInTrigger);

        if (!isClicked)
        {
            // Debug.Log("Moving");
            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
            transform.position = worldPosition;



            if (Input.GetMouseButtonDown(0) && !playerInTrigger)
            {
                ResetCounterAndString();
                isClicked = true;



                foreach (var collider in boxes)
                {
                    collider.enabled = true;
                }

                ColorizeAllChildImages();
            }
            
        }
    }

    private void ResetCounterAndString()
    {
        // add string to set of used words
        Tracker.Add(References.currentString);
        References.currentString = string.Empty;
        References.counter = 0;
    }

    public void GrayOutAllChildImages()
    {
        foreach (Image image in images)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1); ;
        }
    }

    private void ColorizeAllChildImages()
    {
        foreach (Image image in images)
        {
            image.color = new Color(1f, 1f, 1f, 1);
        }
    }

    

}
