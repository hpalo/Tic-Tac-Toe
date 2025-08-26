using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int[] intBoard = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] Cell[] cellBoard = new Cell[9];
    [SerializeField] Color winColor = new Color(1.0f, 1.0f, 0.2f);
    [SerializeField] SpriteAtlas atlas2;

    public enum Player
    {
        O,
        X
    }

    public Player currentPlayer = Player.X;

    public bool gameOver = false;
    public bool isHumanTurn = true;

    int GetState(int row, int col)
    {
        return intBoard[row * 3 + col];
    }

    List<int> GetAvailableMovesForAI()
    {
        List<int> AImoves = new List<int> ();
        for (int i = 0; i < cellBoard.Length; i++)
        {
            if (intBoard[i] == 0)
                AImoves.Add(i);
        }
        return AImoves;
    }

    public void CellClicked(int cellPos, GameObject cellGO)
    {
        if (isHumanTurn && !gameOver && IsFree(cellPos))
        {
            Sprite sprite;

            SetId(cellPos, 2);
            sprite = atlas2.GetSprite("x-260_0");

            if (sprite == null)
            {
                Debug.Log("sprite is null");
            }
            else
            {
                cellGO.GetComponent<Image>().sprite = sprite;
            }

            if (CheckWin(Player.X))
            {
                Debug.Log("Player \"" + Player.X + "\" won!");
                gameOver = true;
            }
            else if (IsFull(intBoard))
            {
                Debug.Log("Draw.");
                gameOver = true;
                return;
            }

            isHumanTurn = false;
            AITurn();
        }
    }


    public bool CheckWin(Player curPlayer)
    {
        for (int i = 0; i < 3; i++)
        {
            // Check horizontally
            if (GetState(i, 0) == (int)(curPlayer + 1) && GetState(i, 1) == (int)(curPlayer + 1) && GetState(i, 2) == (int)(curPlayer + 1))
            {
                cellBoard[i * 3 + 0].GetComponent<Image>().color = winColor;
                cellBoard[i * 3 + 1].GetComponent<Image>().color = winColor;
                cellBoard[i * 3 + 2].GetComponent<Image>().color = winColor;

                return true;
            }
            // Check vertically
            if (GetState(0, i) == (int)(curPlayer + 1) && GetState(1, i) == (int)(curPlayer + 1) && GetState(2, i) == (int)(curPlayer + 1))
            {
                cellBoard[0 * 3 + i].GetComponent<Image>().color = winColor;
                cellBoard[1 * 3 + i].GetComponent<Image>().color = winColor;
                cellBoard[2 * 3 + i].GetComponent<Image>().color = winColor;
                return true;
            }
        }
        // Check diagonally
        if (GetState(0, 0) == (int)(curPlayer + 1) && GetState(1, 1) == (int)(curPlayer + 1) && GetState(2, 2) == (int)(curPlayer + 1))
        {
            cellBoard[0 * 3 + 0].GetComponent<Image>().color = winColor;
            cellBoard[1 * 3 + 1].GetComponent<Image>().color = winColor;
            cellBoard[2 * 3 + 2].GetComponent<Image>().color = winColor;

            return true;
        }
        // Check another diagonal
        if (GetState(0, 2) == (int)(curPlayer + 1) && GetState(1, 1) == (int)(curPlayer + 1) && GetState(2, 0) == (int)(curPlayer + 1))
        {
            cellBoard[0 * 3 + 2].GetComponent<Image>().color = winColor;
            cellBoard[1 * 3 + 1].GetComponent<Image>().color = winColor;
            cellBoard[2 * 3 + 0].GetComponent<Image>().color = winColor;

            return true;
        }

        return false;
    }

    public bool IsFree(int pos)
    {
        bool result = (intBoard[pos] == 0);
        if (!result)
        {
            Debug.Log("pos " + pos + " is not empty. There is currently a mark from the player \"" + getPlayer(intBoard[pos]) + "\".");
        }

        return result;
    }

    public void SetId(int pos, int id)
    {
        if (intBoard[pos] == 0)
        {
            intBoard[pos] = id;
        }
        else
        {
            Debug.Log("The position is reserved.");
        }
    }

    Player getPlayer(int id)
    {
        if (id == 1)
            return Player.O;
        else return Player.X;
    }

    public void Reset()
    {
        gameOver = false;
        currentPlayer = Player.X;
        isHumanTurn = true;

        for (int i = 0; i < intBoard.Length; i++)
        {
            intBoard[i] = 0;
        }

        foreach (Cell cell in cellBoard)
        {
            cell.ResetCell();
        }
    }

    public bool IsFull(int[] state)
    {
        for (int i = 0; i < 9; i++)
            if (state[i] == 0) return false;
        return true;
    }

    void AITurn()
    {
        if (gameOver)
            return;

        if (IsFull(intBoard))
        {
            Debug.Log("Draw.");
            return;
        }

        List<int> moves = GetAvailableMovesForAI();

        if (moves.Count > 0)
        {
            int randomCellPos = Random.Range(0, moves.Count);
            SetId(moves[randomCellPos], 1);
            Sprite sprite = atlas2.GetSprite("o-260_0");
            cellBoard[moves[randomCellPos]].GetComponent<Image>().sprite = sprite;
            
            if (CheckWin(Player.O))
            {
                Debug.Log("Player \"O\" won!");
                gameOver = true;
            }

            isHumanTurn = true;
            return;
        }
    }
}
