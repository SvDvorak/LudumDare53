using UnityEngine;

public class GameStateChangeTracker : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;
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

    private static void PerformChanges(string[] locationEventChanges)
    {
        foreach(var change in locationEventChanges)
        {
            if(change.StartsWith("VICTORY"))
                Debug.Log("SHOW VICTORY SCREEN");
            else if(change.StartsWith("DEFEAT"))
                Debug.Log("SHOW DEFEAT SCREEN");
            else if(change[0] == '+')
                Debug.Log($"GET ITEM {change.Substring(1)}");
            else if(change[0] == '-')
                Debug.Log($"DROP ITEM {change.Substring(1)}");
        }
    }

    private GameInfo.ItemEvent GetLocationEvent()
    {
        var location = playerGroup.currentLocation;
        return GameState.Instance.GetLocationEvent(location.LocationID);
    }
}