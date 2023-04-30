using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCharacterInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CharacterInfoPanel;
    public Image Portrait;
    public TMP_Text Name;
    public TMP_Text Description;

    [SerializeField] private Image[] characters;

    private bool isMouseHovering = false;

    public void Start()
    {
        for (int i = 0; i < GameInfo.Instance.Characters.Length; i++)
            characters[i].gameObject.name = GameInfo.Instance.Characters[i].ID;

        CharacterInfoPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isMouseHovering && Input.GetMouseButtonDown(0))
            HideInfo();
    }

    public void ShowInfo(Button button)
    {
        if (GameState.Instance.IsGameOver() || ShowEnterLocationInfo.IsShowingEnterLocationInfo)
            return;

        // We don't want to fade out the info box when we click on another character
        isMouseHovering = true;

        if(!CharacterInfoPanel.activeSelf)
            CharacterInfoPanel.GetComponent<FadeEffect>().FadeInAndEnable();

        Portrait.sprite = button.GetComponent<Image>().sprite;
        var character = GameInfo.Instance.Characters.Where(x => x.ID == button.gameObject.name).First();
        Name.text = character.Name;
        Description.text = character.Description;
    }
    
    public void HideInfo()
    {
        if(CharacterInfoPanel.activeSelf)
            CharacterInfoPanel.GetComponent<FadeEffect>().FadeOutAndDisable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHovering = true;
    }
}