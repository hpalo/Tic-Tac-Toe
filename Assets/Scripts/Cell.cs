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

        //Debug.Log("CellPressed() at " + cellPos + " by " + grid.currentPlayer);
        if (!board.gameOver && board.IsFree(cellPos))
        {
            Sprite sprite;

            if (board.currentPlayer == Board.Player.X)
            {
                board.SetX(cellPos);
                sprite = atlas.GetSprite("x-260_0");
            }
            else
            {
                board.SetO(cellPos);
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

            if (board.CheckWin(board.currentPlayer))
            {
                Debug.Log("Player \"" + board.currentPlayer + "\" Won!");
                board.gameOver = true;
            }
            else
            {
                // Swap players
                if (board.currentPlayer == Board.Player.X)
                {
                    board.currentPlayer = Board.Player.O;
                }
                else
                {
                    board.currentPlayer = Board.Player.X;
                }
            }
        }
    }
    
    public void ResetCell()
    {
        GetComponent<Image>().sprite = atlas.GetSprite("empty-blue-260_0");
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
