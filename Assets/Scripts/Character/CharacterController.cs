using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;

    [SerializeField] private GameObject characters;

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
            {
                foreach (Transform child in characters.transform)
                {
                    if (child.gameObject.name == character.ID)
                        child.GetComponent<CharacterMouseMover>().Enable();
                }
            }
        }
    }

    private void OnDisableMouseMover()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (character.IsAlive)
            {
                foreach (Transform child in characters.transform)
                {
                    if (child.gameObject.name == character.ID)
                        child.GetComponent<CharacterMouseMover>().Disable();
                }
            }
        }
    }

    private void OnDeadCharacter(GameState.Character deadCharacter)
    {
        foreach (Transform child in characters.transform)
        {
            if (child.gameObject.name == deadCharacter.ID)
            {
                child.gameObject.GetComponent<CharacterMouseMover>().Disable();
            }
        }
    }

    private void OnUpdateDeadCharacters()
    {
        foreach (var character in GameState.Instance.Characters)
        {
            if (!character.IsAlive)
            {
                foreach (Transform child in characters.transform)
                {
                    if (child.gameObject.name == character.ID)
                    {
                        var deadCharacter = child.gameObject;
                        float darkness = 0.4f;
                        deadCharacter.GetComponent<Image>().color = new Color(darkness, darkness, darkness);
                        deadCharacter.transform.Find("Skull").GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }
}
