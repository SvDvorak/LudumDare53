using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public ShowEnterLocationInfo showEnterLocationInfo;

    [SerializeField] private GameObject characters;
    [SerializeField] private GameObject groupCharacters;

    void Start()
    {
        showEnterLocationInfo.CharacterDied += OnDeadCharacter;
        ShowEnterLocationInfo.ShowedInfo += OnDisableMouseMover;
        ShowEnterLocationInfo.HidInfo += OnEnableMouseMover;
    }

    private void OnDestroy()
    {
        showEnterLocationInfo.CharacterDied -= OnDeadCharacter;
        ShowEnterLocationInfo.ShowedInfo -= OnDisableMouseMover;
        ShowEnterLocationInfo.HidInfo -= OnEnableMouseMover;
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
                
                var deadCharacterObject = child.gameObject;
                float darkness = 0.4f;
                deadCharacterObject.GetComponent<Image>().color = new Color(darkness, darkness, darkness);
                deadCharacterObject.transform.Find("Skull").GetComponent<Image>().enabled = true;
            }
        }

        foreach (Transform child in groupCharacters.transform)
        {
            if (child.gameObject.name == deadCharacter.ID)
                child.gameObject.SetActive(false);
        }
    }
}
