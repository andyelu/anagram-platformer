using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;
    public LetterBankScript letterBankScript;
    public CreationRowScript creationRowScript;
    public Sprite iceBlock;
    public Sprite emptyBlock;
    public Sprite letterBlock;

    public Image imageComponent;

    public char letter { get; set; }

    private void Update()
    {
        changeSprite();
    }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        //reloadEmptySprite();
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

    public void changeSprite()
    {
        if (letterBankScript != null && letter != ' ')
        {
            imageComponent.sprite = letterBlock;
        }
        else if (creationRowScript != null && letter != ' ')
        {
            imageComponent.sprite = iceBlock;
        } else
        {
            imageComponent.sprite = emptyBlock;
        }
    }

    private void reloadEmptySprite()
    {
        // This method is needed to fix the buggy empty-tile sprite.
        // By instantiating it on runtime, it doesn't lose pixels

        
        

    }
}
