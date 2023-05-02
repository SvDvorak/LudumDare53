using System.Linq;
using UnityEngine;

public class GameStateChangeTracker : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;
    public GameOver gameOver;
    public PlayerGroup playerGroup;

    public void OnEnable()
    {
        showEnterLocationInfo.CompletedLocationEvent += UpdateChanges;
        showEnterLocationInfo.AnsweredLocationEvent += UpdateChanges;
    }

    public void OnDisable()
    {
        showEnterLocationInfo.CompletedLocationEvent -= UpdateChanges;
        showEnterLocationInfo.AnsweredLocationEvent -= UpdateChanges;
    }

    private void UpdateChanges(GameInfo.ItemEvent itemEvent)
    {
        PerformChanges(itemEvent.Changes);
    }

    private void UpdateChanges(bool answeredYes, GameInfo.ItemEvent itemEvent)
    {
        PerformChanges(answeredYes ? itemEvent.ChoiceYesChanges : itemEvent.ChoiceNoChanges);
    }

    private void PerformChanges(string[] locationEventChanges)
    {
        if (locationEventChanges == null)
            return;

        foreach (var change in locationEventChanges)
        {
            if (change.StartsWith("ENDING"))
            {
                var text = change.Length > 6 ? change.Substring(7) : "";
                gameOver.Show(false, "The End", text);
            }
            else if (change.StartsWith("TEXT"))
            {
                showEnterLocationInfo.QueueShowingText(change.Substring(5));
            }
            else if (change[0] == '+')
            {
                if (!GameState.Instance.CarriedItems.Contains(change.Substring(1)))
                    GameState.Instance.CarriedItems.Add(change.Substring(1));
            }
            else if (change[0] == '-')
            {
                GameState.Instance.CarriedItems.Remove(change.Substring(1));
                GameState.Instance.DeliveredItems.Add(change.Substring(1));
            }
        }
    }
}