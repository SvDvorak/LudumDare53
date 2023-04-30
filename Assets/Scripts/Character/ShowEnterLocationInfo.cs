using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowEnterLocationInfo : MonoBehaviour
{
    public FadeEffect MenuFade;
    public TMP_Text Title;
    public TMP_Text Description;
    
    public void ShowEnterLocation(string characterID, Location currentLocation)
    {
        var gameInfo = GameInfo.Instance;
        var character = gameInfo.Characters.First(x => x.ID == characterID);
        var locationInfo = gameInfo.Locations.First(x => x.ID == currentLocation.LocationID);
        if(character.FailureLocationIDs.Contains(currentLocation.LocationID))
        {
            Title.text = "FAILURE";
            Description.text = locationInfo.Failure;
        }
        else
        {
            Title.text = "SUCCESS";
            Description.text = locationInfo.Success;
        }
        
        MenuFade.FadeInAndEnable();
    }
    
    public void HideInfo()
    {
        if(MenuFade.gameObject.activeSelf)
            MenuFade.FadeOutAndDisable();
    }
}