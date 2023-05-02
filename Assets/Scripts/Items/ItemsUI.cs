using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerGroup playerGroup;
    [SerializeField] private ShowEnterLocationInfo showEnterLocationInfo;

    void Start()
    {
        text.text = "Items\n\n";
        GameStateChangeTracker.ItemsChanged += OnUpdateUI;
        showEnterLocationInfo.AnsweredLocationEvent += OnUpdateUI;
        playerGroup.EnteredLocation += OnUpdateUI;
    }

    private void OnDestroy()
    {
        GameStateChangeTracker.ItemsChanged -= OnUpdateUI;
        showEnterLocationInfo.AnsweredLocationEvent -= OnUpdateUI;
        playerGroup.EnteredLocation -= OnUpdateUI;
    }

    private void OnUpdateUI()
    {
        text.text = "Items\n\n";
        foreach (var item in GameState.Instance.GetActiveItems())
        {
            text.text += item.Name + "\n";
        }
    }

    private void OnUpdateUI(bool arg1, GameInfo.ItemEvent arg2)
    {
        OnUpdateUI();
    }

    private void OnUpdateUI(string characterID, Location currentLocation)
    {
        OnUpdateUI();
    }
}
