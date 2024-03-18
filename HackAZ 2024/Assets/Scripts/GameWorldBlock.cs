using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameWorldBlock : MonoBehaviour
{
    [SerializeField]
    private bool isClicked = false;
    private BoxCollider2D[] boxes;

    private void Start()
    {
        boxes = GetComponentsInChildren<BoxCollider2D>();
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

            Debug.Log("Moving");
            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
            transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                foreach (var collider in boxes)
                {
                    collider.enabled = true;
                }
            }
        }
    }
}
