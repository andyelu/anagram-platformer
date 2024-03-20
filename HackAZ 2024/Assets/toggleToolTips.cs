using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleToolTips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ToggleToolTipsVisibility();
    }

    private void Update()
    {
        ToggleToolTipsVisibility();
    }

    void ToggleToolTipsVisibility()
    {
        // Assuming References.toolTipsToggle exists and is accessible.
        bool shouldShow = References.toolTipsToggle;

        // Get all children of this GameObject
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldShow);
        }
    }
}
