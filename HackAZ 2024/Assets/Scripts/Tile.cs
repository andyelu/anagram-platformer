using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;
    public LetterBankScript letterBankScript;
    public CreationRowScript creationRowScript;

    public char letter { get; set; }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (letterBankScript != null)
        {
            letterBankScript.OnTileClicked(this);
        }

        if (creationRowScript != null)
        {
            creationRowScript.OnTileClicked(this);
        }
    }
}
