using UnityEngine;

public class Click : MonoBehaviour
{
    public Rect rectangle;
    private Rect buttonPos;
    SpriteRenderer spriteR;
    Sprite xSprite;
    Sprite oSprite;
    Sprite dotSprite;
    Sprite xredSprite;

    void Start()
    {
        rectangle = new Rect(0.0f, 0.0f, 240.0f, 240.0f);

        //Fetch the SpriteRenderer from the GameObject
        spriteR = GetComponent<SpriteRenderer>();

        xSprite = Resources.Load<Sprite>("Images/x-alpha");
        oSprite = Resources.Load<Sprite>("Images/o-alpha");
        //xredSprite = Resources.Load<Sprite>("Images/x-red");

        //spriteR.sprite = xredSprite;
        spriteR.sprite = oSprite;
        //m_SpriteRenderer.sprite = Resources.Load<Sprite>("Images/o-alpha");
        if (spriteR.sprite == null)
            Debug.Log("m_SpriteRenderer.sprite == null ");
        else
            Debug.Log("Image found");
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        if (GUI.Button(rectangle, "Choose next sprite"))
        //if (GUI.Button(buttonPos, "Choose next sprite"))
        {
            spriteR.sprite = xSprite;
        }
    }
}
