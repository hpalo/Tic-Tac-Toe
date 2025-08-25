using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    int[] intBoard = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] Cell[] cellBoard = new Cell[9];
    [SerializeField] Color winColor = new Color(1.0f, 1.0f, 0.2f);
    
    public enum Player
    {
        O,
        X
    }

    public Player currentPlayer = Player.X;

    public bool gameOver = false;

    int GetState(int row, int col)
    {
        return intBoard[row * 3 + col];
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
        // Check another diagonally
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
            //Debug.Log("pos " + pos + " is not free. There is currently a mark by id " + grid[pos] + ".");
            Debug.Log("pos " + pos + " is not empty. There is currently a mark from the player \"" + getPlayer(intBoard[pos]) + "\".");
        }

        return result;
    }

    public void SetX(int pos)
    {
        if (intBoard[pos] == 0)
        {
            intBoard[pos] = 2;
        }
        else
        {
            Debug.Log("The position is reserved.");
        }
    }
    public void SetO(int pos)
    {
        if (intBoard[pos] == 0)
        {
            intBoard[pos] = 1;
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

        for (int i = 0; i < intBoard.Length; i++)
        {
            intBoard[i] = 0;
        }

        foreach (Cell cell in cellBoard)
        {
            cell.ResetCell();
        }
    }
}
