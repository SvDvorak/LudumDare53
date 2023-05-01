using UnityEngine;

public class GameStateChangeTracker : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;
    public GameOver gameOver;
    public PlayerGroup playerGroup;
    
    public void OnEnable()
    {
        showEnterLocationInfo.HidInfo += UpdateChanges;
    }

    public void OnDisable()
    {
        showEnterLocationInfo.HidInfo -= UpdateChanges;
    }

    public void OnYesResponse()
    {
        PerformChanges(GetLocationEvent().ChoiceYesChanges);
        showEnterLocationInfo.ResponseButtonClicked();
    }
    
    public void OnNoResponse()
    {
        PerformChanges(GetLocationEvent().ChoiceNoChanges);
        showEnterLocationInfo.ResponseButtonClicked();
    }

    private void UpdateChanges()
    {
        var locationEvent = GetLocationEvent();
        if(locationEvent != null)
        {
            PerformChanges(locationEvent.Changes);
        }
    }

    private void PerformChanges(string[] locationEventChanges)
    {
        if(locationEventChanges == null)
            return;
        
        foreach(var change in locationEventChanges)
        {
            if(change.StartsWith("ENDING"))
            {
                var text = change.Length > 6 ? change.Substring(7) : "";
                gameOver.Show(false, text);
            }
            else if(change.StartsWith("TEXT"))
            {
                
            }
            else if(change[0] == '+')
            {
                GameState.Instance.CarriedItems.Add(change.Substring(1));
            }
            else if(change[0] == '-')
            {
                GameState.Instance.CarriedItems.Remove(change.Substring(1));
                GameState.Instance.DeliveredItems.Add(change.Substring(1));
            }
        }
    }

    private GameInfo.ItemEvent GetLocationEvent()
    {
        var location = playerGroup.currentLocation;
        return GameState.Instance.GetLocationEvent(location.LocationID);
    }
}