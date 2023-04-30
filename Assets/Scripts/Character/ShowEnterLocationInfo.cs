using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ShowEnterLocationInfo : MonoBehaviour
{
    public event Action<GameState.Character> CharacterDied;
    public event Action ShowedInfo;
    public event Action HidInfo;

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
            if (GameState.Instance.Characters.TryGetValue(characterID, out var characterInfo))
            {
                characterInfo.IsAlive = false;
                CharacterDied?.Invoke(characterInfo);
            }
        }
        else
        {
            Title.text = "SUCCESS";
            Description.text = locationInfo.Success;
        }
        
        MenuFade.FadeInAndEnable();
        ShowedInfo?.Invoke();
    }
    
    public void HideInfo()
    {
        if (MenuFade.gameObject.activeSelf)
        {
            MenuFade.FadeOutAndDisable();
            HidInfo?.Invoke();
        }
    }
}