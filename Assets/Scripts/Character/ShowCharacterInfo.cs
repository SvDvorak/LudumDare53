using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterInfo : MonoBehaviour
{
    public GameObject CharacterInfoPanel;
    public Image Portrait;
    public TMP_Text Name;
    public TMP_Text Description;

    public Image[] CharacterPortraits;

    public void Start()
    {
        CharacterInfoPanel.SetActive(false);
    }

    public void ShowInfo(int characterID)
    {
        if(!CharacterInfoPanel.activeSelf)
        {
            CharacterInfoPanel.SetActive(true);
            CharacterInfoPanel.GetComponent<FadeEffect>().FadeIn();
        }
        Portrait.sprite = CharacterPortraits[characterID-1].sprite;
        var character = GameInfo.Instance.Characters[characterID-1];
        Name.text = character.Name;
        Description.text = character.Description;
    }
    
    public void HideInfo()
    {
        if(CharacterInfoPanel.activeSelf)
            CharacterInfoPanel.GetComponent<FadeEffect>().FadeOutAndDisable();
    }
}