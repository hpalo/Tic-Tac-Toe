using UnityEngine;
using UnityEngine.UIElements;

public class ChangeOptions : MonoBehaviour
{
    public Options options;

    [SerializeField] private string message;

    public void SendMessage(bool toggleValue)
    {
        if (toggleValue)
        {
            if (message == "Hard")
            {
                options.currentAIPlayer = Options.AIPlayer.hard;
            }
            else if (message == "Easy")
            {
                options.currentAIPlayer = Options.AIPlayer.easy;
            }
        }
    }
}


