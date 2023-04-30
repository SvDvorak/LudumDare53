using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;

    void Start()
    {
        showEnterLocationInfo.HidInfo += OnUpdateDeadCharacters;
        showEnterLocationInfo.CharacterDied += OnDeadCharacter;
        showEnterLocationInfo.ShowedInfo += OnDisableMouseMover;
        showEnterLocationInfo.HidInfo += OnEnableMouseMover;
    }

    private void OnEnableMouseMover()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (character.Value.IsAlive)
                GameObject.Find(character.Key).GetComponent<CharacterMouseMover>().Enable();
        }
    }

    private void OnDisableMouseMover()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (character.Value.IsAlive)
                GameObject.Find(character.Key).GetComponent<CharacterMouseMover>().Disable();
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
            if (!character.Value.IsAlive)
            {
                var deadCharacter = GameObject.Find(character.Key);
                float darkness = 0.4f;
                deadCharacter.GetComponent<Image>().color = new Color(darkness, darkness, darkness);
            }
        }
    }

    void Update()
    {
        
    }
}
