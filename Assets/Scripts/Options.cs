using UnityEngine;

public class Options : MonoBehaviour
{
    public enum AIPlayer
    {
        easy,
        hard
    }

    public AIPlayer currentAIPlayer = AIPlayer.easy;
}
