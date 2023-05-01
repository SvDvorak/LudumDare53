using System.Linq;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ShowEnterLocationInfo : MonoBehaviour
{
    public event Action<GameState.Character> CharacterDied;
    public event Action ShowedInfo;
    public event Action HidInfo;

    public FadeEffect MenuFade;
    public TMP_Text Title;
    public TMP_Text Description;
    public Button YesButton;
    public Button NoButton;

    [SerializeField] private PlayerGroup playerGroup;
    private bool showButtons;

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

        var locationEvent = GameState.Instance.GetLocationEvent(currentLocation.LocationID);
        if(character.FailureLocationIDs.Contains(currentLocation.LocationID))
        {
            Title.text = "Character Lost";
            Description.text = locationInfo.Failure;
            var gamestateCharacter = GameState.Instance.Characters.First(x => x.ID == characterID);
            gamestateCharacter.IsAlive = false;
            CharacterDied?.Invoke(gamestateCharacter);
            
            YesButton.gameObject.SetActive(false);
            NoButton.gameObject.SetActive(false);
            
            MenuFade.FadeInAndEnable();
            ShowedInfo?.Invoke();
            IsShowingEnterLocationInfo = true;
        }
        else if(locationEvent != null)
        {
            Title.text = GameInfo.Instance.Locations.First(x => x.ID == currentLocation.LocationID).Name;
            Description.text = locationEvent.EventText;

            showButtons = locationEvent.ChoiceYesChanges != null || locationEvent.ChoiceNoChanges != null;
            YesButton.gameObject.SetActive(showButtons);
            NoButton.gameObject.SetActive(showButtons);
            
            MenuFade.FadeInAndEnable();
            ShowedInfo?.Invoke();
            IsShowingEnterLocationInfo = true;
        }
        else
        {
            HidInfo?.Invoke();
        }
    }
    
    public void HideInfo()
    {
        if(showButtons)
            return;
        
        if (MenuFade.gameObject.activeSelf)
        {
            MenuFade.FadeOutAndDisable();
            HidInfo?.Invoke();
            IsShowingEnterLocationInfo = false;
        }
    }

    public void ResponseButtonClicked()
    {
        showButtons = false;
        HideInfo();
    }
}