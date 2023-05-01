using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;

    void Start()
    {
        ShowEnterLocationInfo.HidInfo += OnUpdateDeadCharacters;
        showEnterLocationInfo.CharacterDied += OnDeadCharacter;
        ShowEnterLocationInfo.ShowedInfo += OnDisableMouseMover;
        ShowEnterLocationInfo.HidInfo += OnEnableMouseMover;
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
        GameObject.Find(deadCharacter.ID).GetComponent<CharacterMouseMover>().Disable();
    }

    private void OnUpdateDeadCharacters()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (!character.IsAlive)
            {
                var deadCharacter = GameObject.Find(character.ID);
                float darkness = 0.4f;
                deadCharacter.GetComponent<Image>().color = new Color(darkness, darkness, darkness);
                deadCharacter.transform.Find("Skull").GetComponent<Image>().enabled = true;
            }
        }
    }
}
