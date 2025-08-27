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

    public bool gameOver = false;
    public bool isHumanTurn = true;
    public Options options;

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
        if (isHumanTurn && !gameOver && IsEmpty(cellPos))
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

            if (UpdateSpritesOnWin(Player.X))
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
            AITurn(options.currentAIPlayer);
        }
    }


    public bool CheckWin(Player curPlayer)
    {
        for (int i = 0; i < 3; i++)
        {
            // Check horizontally
            if (GetState(i, 0) == (int)(curPlayer + 1) && GetState(i, 1) == (int)(curPlayer + 1) && GetState(i, 2) == (int)(curPlayer + 1))
            {
                 return true;
            }
            // Check vertically
            if (GetState(0, i) == (int)(curPlayer + 1) && GetState(1, i) == (int)(curPlayer + 1) && GetState(2, i) == (int)(curPlayer + 1))
            {
                return true;
            }
        }
        // Check diagonally
        if (GetState(0, 0) == (int)(curPlayer + 1) && GetState(1, 1) == (int)(curPlayer + 1) && GetState(2, 2) == (int)(curPlayer + 1))
        {
             return true;
        }
        // Another diagonal
        if (GetState(0, 2) == (int)(curPlayer + 1) && GetState(1, 1) == (int)(curPlayer + 1) && GetState(2, 0) == (int)(curPlayer + 1))
        {
             return true;
        }

        return false;
    }

    public bool UpdateSpritesOnWin(Player curPlayer)
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

    public bool IsEmpty(int pos)
    {
        bool result = (intBoard[pos] == 0);
        if (!result)
        {
            Debug.Log("Pos " + pos + " is not empty. There is a mark from player \"" + getPlayer(intBoard[pos]) + "\".");
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

    void AITurn(Options.AIPlayer aiPlayer)
    {
        if (aiPlayer == Options.AIPlayer.easy)
            AITurnEasy();
        else
            AITurnHard();
    }

    void AITurnEasy()
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

            if (UpdateSpritesOnWin(Player.O))
            {
                Debug.Log("Player \"O\" won!");
                gameOver = true;
            }

            isHumanTurn = true;
            return;
        }
    }

    void AITurnHard()
    {
        if (gameOver)
            return;
        if (IsFull(intBoard))
        {
            Debug.Log("Draw.");
            return;
        }

        int bestScore = int.MinValue;

        int bestMove = -1;

        for (int i = 0; i < 9; i++)
        {
            if (intBoard[i] == 0)
            {
                intBoard[i] = 1;
                int score = MinMax(intBoard, 0, false);
                intBoard[i] = 0;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }

        intBoard[bestMove] = 1;
        Sprite sprite = atlas2.GetSprite("o-260_0");
        cellBoard[bestMove].GetComponent<Image>().sprite = sprite;

        if (UpdateSpritesOnWin(Player.O))
        {
            Debug.Log("Player \"O\" won!");
        }
        else if (IsFull(intBoard))
        {
            Debug.Log("Draw.");
        }

        isHumanTurn = true;
    }

    int MinMax(int[] state, int depth, bool isMaximizing)
    {
        if (CheckWin(Player.O)) return 10 - depth;
        if (CheckWin(Player.X)) return depth - 10;
        if (IsFull(intBoard)) return 0;

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        for (int i = 0; i < 9; i++)
        {
            if (intBoard[i] == 0)
            {
                intBoard[i] = isMaximizing ? 1 : 2;
                int score = MinMax(intBoard, depth + 1, !isMaximizing);
                intBoard[i] = 0;

                bestScore = isMaximizing
                    ? Mathf.Max(score, bestScore)
                    : Mathf.Min(score, bestScore);
            }
        }

        return bestScore;
    }
}
