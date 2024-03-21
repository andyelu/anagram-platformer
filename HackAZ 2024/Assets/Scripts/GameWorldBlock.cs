using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWorldBlock : MonoBehaviour
{
    public bool isClicked = false;
    private BoxCollider2D parentCollider;
    private List<BoxCollider2D> triggers;
    private TextMeshProUGUI[] texts;
    private Image[] images;
    public bool playerInTrigger = false;
    public GameObject toolTips;

    private void Start()
    {
        parentCollider = GetComponent<BoxCollider2D>();
        if (parentCollider == null)
        {
            Debug.LogError("No BoxCollider2D found on the parent object.");
            return;
        }

        // Initially disable the parent collider
        parentCollider.enabled = false;

        triggers = new List<BoxCollider2D>(GetComponentsInChildren<BoxCollider2D>()); // Get all BoxCollider2D components in children
        triggers.RemoveAll(c => !c.isTrigger); // Remove the parent collider from the list if it's there

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
            References.toolTipsToggle = true;

            // if right clicked, cancel placement
            // remove word from tracker
            if (Input.GetMouseButtonDown(1))
            {
                Tracker.Remove(References.currentString);
                References.toolTipsToggle = false;
                Destroy(gameObject);
            }
           
            if (Input.GetMouseButtonDown(0) && !playerInTrigger)
            {
                ResetCounterAndString();
                isClicked = true;
                References.toolTipsToggle = false;

                // Enable the parent collider
                parentCollider.enabled = true;

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
            image.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    public void ColorizeAllChildImages()
    {
        foreach (Image image in images)
        {
            image.color = new Color(1f, 1f, 1f, 1);
        }
    }
}
