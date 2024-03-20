using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1); // RGB(128, 128, 128) equivalent

    void Start()
    {
        GrayOutAllChildImages();
    }

    void GrayOutAllChildImages()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.color = grayColor;
        }
    }
}