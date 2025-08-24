using UnityEngine;

public class Grid : MonoBehaviour
{
    int[] grid = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public enum Player
    {
        o,
        x
    }

    public Player currentPlayer = Player.x;
    public Player prevPlayer = Player.o;

    public bool IsFree(int pos)
    {
        bool result = (grid[pos] == 0);
        if (!result)
        {
            //Debug.Log("pos " + pos + " is not free. There is currently a mark by id " + grid[pos] + ".");
            Debug.Log("pos " + pos + " is not free. There is currently a mark \"" + prevPlayer + "\".");
        }

        return result;
    }

    public void SetX(int pos)
    {
        if (grid[pos] == 0)
        {
            grid[pos] = 2;
        }
        else
        {
            Debug.Log("The position is reserved.");
        }
    }
    public void SetO(int pos)
    {
        if (grid[pos] == 0)
        {
            grid[pos] = 1;
        }
        else
        {
            Debug.Log("The position is reserved.");
        }
    }
}
