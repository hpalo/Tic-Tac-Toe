using UnityEngine;
using UnityEngine.U2D;  // For SpriteAtlas
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] int cellPos;

    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite("empty-blue-260_0"); // Default sprite
    }

    public void CellPressed()
    {
        board.CellClicked(cellPos, gameObject);
    }
    
    public void ResetCell()
    {
        GetComponent<Image>().sprite = atlas.GetSprite("empty-blue-260_0");
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
