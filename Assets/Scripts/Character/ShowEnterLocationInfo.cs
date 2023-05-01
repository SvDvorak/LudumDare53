using System.Linq;
using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowEnterLocationInfo : MonoBehaviour
{
    public event Action<GameState.Character> CharacterDied;
    public static event Action ShowedInfo;
    public static event Action HidInfo;
    public static event Action LostCharacter;
    public static event Action Success;
    public event Action<GameInfo.ItemEvent> CompletedLocationEvent;
    public event Action<bool, GameInfo.ItemEvent> AnsweredLocationEvent;

    public FadeEffect MenuFade;
    public TMP_Text Title;
    public TMP_Text Description;
    public Button YesButton;
    public Button NoButton;

    [SerializeField] private PlayerGroup playerGroup;
    private bool forceMenuButtonClick;

    private void Start()
    {
        playerGroup.EnteredLocation += ShowEnterLocation;
        MenuFade.gameObject.SetActive(false);
        YesButton.onClick.AddListener(OnYesResponse);
        NoButton.onClick.AddListener(OnNoResponse);
    }

    private void OnDestroy()
    {
        playerGroup.EnteredLocation -= ShowEnterLocation;
    }

    private class QueuedLocationInfo
    {
        public string Title;
        public string Description;
        public Action OnShow;
        public bool ShowButtons;
        public GameInfo.ItemEvent LocationEvent;
    }
    
    private QueuedLocationInfo currentLocationInfo;
    
    private List<QueuedLocationInfo> queuedLocationInfos = new();

    public void ShowEnterLocation(string characterID, Location currentLocation)
    {
        var gameInfo = GameInfo.Instance;
        var character = gameInfo.Characters.First(x => x.ID == characterID);
        var locationInfo = gameInfo.Locations.FirstOrDefault(x => x.ID == currentLocation.LocationID);
        if(locationInfo == null)
        {
            Debug.LogWarning($"Location event not found for {currentLocation.LocationID} and character {characterID}");
            return;
        }

        var locationEvents = GameState.Instance.GetLocationEvents(currentLocation.LocationID);
        if (character.FailureLocationIDs.Contains(currentLocation.LocationID))
        {
            LostCharacter?.Invoke();
            queuedLocationInfos.Add(new QueuedLocationInfo()
            {
                Title = "Character Lost",
                Description = locationInfo.Failure,
                ShowButtons = false,
                OnShow = () =>
                {
                    var gamestateCharacter = GameState.Instance.Characters.First(x => x.ID == characterID);
                    gamestateCharacter.IsAlive = false;
                    CharacterDied?.Invoke(gamestateCharacter);
                }
            });
        }
        else
            Success?.Invoke();

        foreach(var locationEvent in locationEvents)
        {
            queuedLocationInfos.Add(new QueuedLocationInfo()
            {
                Title = GameInfo.Instance.Locations.First(x => x.ID == currentLocation.LocationID).Name,
                Description = locationEvent.EventText,
                ShowButtons = locationEvent.ChoiceYesChanges != null || locationEvent.ChoiceNoChanges != null,
                LocationEvent = locationEvent,
                OnShow = null
            });
        }

        var isShowingNew = TryShowingNextLocationInfo();
        if (!isShowingNew && MenuFade.gameObject.activeSelf)
        {
            MenuFade.FadeOutAndDisable();
        }
    }

    public void QueueShowingText(string text)
    {
        queuedLocationInfos.Insert(0, new QueuedLocationInfo()
        {
            Title = "",
            Description = text,
        });
    }

    private bool TryShowingNextLocationInfo()
    {
        if(queuedLocationInfos.Count <= 0)
            return false;
        
        var info = queuedLocationInfos[0];
        queuedLocationInfos.RemoveAt(0);
            
        Title.text = info.Title;
        Description.text = info.Description;
            
        info.OnShow?.Invoke();
            
        YesButton.gameObject.SetActive(info.ShowButtons);
        NoButton.gameObject.SetActive(info.ShowButtons);
            
        if(!MenuFade.gameObject.activeSelf)
            MenuFade.FadeInAndEnable();
        ShowedInfo?.Invoke();
        forceMenuButtonClick = info.ShowButtons;
        
        currentLocationInfo = info;
        
        return true;
    }

    public void HideInfo()
    {
        if(forceMenuButtonClick)
            return;
        
        if(currentLocationInfo != null && currentLocationInfo.LocationEvent != null)
            CompletedLocationEvent?.Invoke(currentLocationInfo.LocationEvent);
        
        var isShowingNew = TryShowingNextLocationInfo();
        if (!isShowingNew && MenuFade.gameObject.activeSelf)
        {
            MenuFade.FadeOutAndDisable();
            HidInfo?.Invoke();
        }
    }

    public void OnYesResponse()
    {
        AnsweredLocationEvent?.Invoke(true, currentLocationInfo.LocationEvent);
        ResponseButtonClicked();
    }

    public void OnNoResponse()
    {
        AnsweredLocationEvent?.Invoke(false, currentLocationInfo.LocationEvent);
        ResponseButtonClicked();
    }

    public void ResponseButtonClicked()
    {
        forceMenuButtonClick = false;
        HideInfo();
    }
}