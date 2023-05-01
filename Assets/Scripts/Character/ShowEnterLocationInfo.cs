using System.Linq;
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

    [SerializeField] private PlayerGroup playerGroup;

    public static bool IsShowingEnterLocationInfo { get; private set; }

    private void Start()
    {
        playerGroup.EnteredLocation += ShowEnterLocation;
    }

    private void OnDestroy()
    {
        playerGroup.EnteredLocation -= ShowEnterLocation;
    }

    public void ShowEnterLocation(string characterID, Location currentLocation)
    {
        var gameInfo = GameInfo.Instance;
        var character = gameInfo.Characters.First(x => x.ID == characterID);
        var locationInfo = gameInfo.Locations.FirstOrDefault(x => x.ID == currentLocation.LocationID);
        if(locationInfo == null)
        {
            Debug.LogWarning("Location event not found");
            return;
        }
        
        if(character.FailureLocationIDs.Contains(currentLocation.LocationID))
        {
            Title.text = "FAILURE";
            Description.text = locationInfo.Failure;
            var gamestateCharacter = GameState.Instance.Characters.First(x => x.ID == characterID);
            gamestateCharacter.IsAlive = false;
            CharacterDied?.Invoke(gamestateCharacter);
        }
        else
        {
            Title.text = "SUCCESS";
            Description.text = locationInfo.Success;
        }
        
        MenuFade.FadeInAndEnable();
        ShowedInfo?.Invoke();
        IsShowingEnterLocationInfo = true;
    }
    
    public void HideInfo()
    {
        if (MenuFade.gameObject.activeSelf)
        {
            MenuFade.FadeOutAndDisable();
            HidInfo?.Invoke();
            IsShowingEnterLocationInfo = false;
        }
    }
}