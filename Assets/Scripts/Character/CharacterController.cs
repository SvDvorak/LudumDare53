using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;

    void Start()
    {
        showEnterLocationInfo.CharacterDied += OnDeadCharacter;
        ShowEnterLocationInfo.ShowedInfo += OnDisableMouseMover;
        ShowEnterLocationInfo.HidInfo += OnEnableMouseMover;
    }

    private void OnDestroy()
    {
        ShowEnterLocationInfo.ShowedInfo -= OnDisableMouseMover;
    }

    private void OnEnableMouseMover()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (character.IsAlive)
                GameObject.Find(character.ID).GetComponent<CharacterMouseMover>().Enable();
        }
    }

    private void OnDisableMouseMover()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (character.IsAlive)
                GameObject.Find(character.ID).GetComponent<CharacterMouseMover>().Disable();
        }
    }

    private void OnDeadCharacter(GameState.Character deadCharacter)
    {
        var deadCharacterObject = GameObject.Find(deadCharacter.ID);
        deadCharacterObject.GetComponent<CharacterMouseMover>().Disable();
        
        if (!deadCharacter.IsAlive)
        {
            float darkness = 0.4f;
            deadCharacterObject.GetComponent<Image>().color = new Color(darkness, darkness, darkness);
            deadCharacterObject.transform.Find("Skull").GetComponent<Image>().enabled = true;
        }
    }
}
