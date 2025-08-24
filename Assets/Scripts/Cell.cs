using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] Grid grid;
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] int cellPos;

    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite("empty-blue-260_0"); // Default sprite
    }

    public void CellPressed()
    {

        //Debug.Log("CellPressed() at " + cellPos + " by " + grid.currentPlayer);
        if (grid.IsFree(cellPos))
        {
            Sprite sprite;

            if (grid.currentPlayer == Grid.Player.x)
            {
                grid.SetX(cellPos);
                sprite = atlas.GetSprite("x-260_0");
            }
            else
            {
                grid.SetO(cellPos);
                sprite = atlas.GetSprite("o-260_0");
            }

            if (sprite == null)
            {
                Debug.Log("sprite is null");
            }
            else
            {
                GetComponent<Image>().sprite = sprite;
            }

            grid.prevPlayer = grid.currentPlayer;
            // Swap player
            if (grid.currentPlayer == Grid.Player.x)
            {
                grid.currentPlayer = Grid.Player.o;
            }
            else
            {
                grid.currentPlayer = Grid.Player.x;
            }
        }
    }
}
