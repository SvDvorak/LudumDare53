using UnityEngine;

public class LocationChangeController : MonoBehaviour
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

    private void UpdateChanges()
    {
        var location = playerGroup.currentLocation;
        var locationEvent = GameState.Instance.GetLocationEvent(location.LocationID);
        if(locationEvent != null)
        {
            foreach(var change in locationEvent.Changes)
            {
                if(change == "VICTORY")
                    Debug.Log("SHOW VICTORY SCREEN");
                else if(change == "DEFEAT")
                    Debug.Log("SHOW DEFEAT SCREEN");
                else if(change[0] == '+')
                    Debug.Log($"GET ITEM {change.Substring(1)}");
                else if(change[0] == '-')
                    Debug.Log($"DROP ITEM {change.Substring(1)}");
            }
        }
    }
}