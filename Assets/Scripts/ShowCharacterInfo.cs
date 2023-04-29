using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo
{
    public Character[] Characters = new Character[4];

    public static GameInfo Instance = new()
    {
        Characters = new Character[]
        {
            new()
            {
                Name = "Seraphina Nightshade",
                Description = "Seraphina Nightshade is a rogue with a quick wit and even quicker reflexes. She has long, flowing black hair and wears black leather armor adorned with silver studs. Seraphina is always on the lookout for her next heist or adventure, but will stand up for what's right when it matters most."
            },
            new()
            {
                Name = "Luna Silvermist",
                Description = "Luna Silvermist is an elf with an affinity for nature magic. She has long, silver hair and wears a dress made of leaves and flowers. Luna uses her powers to protect the forest and its inhabitants from those who would do them harm."
            },
            new()
            {
                Name = "Freya Stormcaller",
                Description = "Freya Stormcaller is a warrior with a heart of gold and a sword of lightning. She is tall and muscular with long, blonde hair that falls in unruly waves around her face. Freya is a champion of justice and is always ready to protect the innocent from those who would do them harm."
            },
            new()
            {
                Name = "Aurora Moonstone",
                Description = "Aurora Moonstone is a fairy with a love of mischief and a deep connection to the moon. She has delicate wings that shimmer with iridescent colors, and wears a dress made of stars. Aurora uses her powers of magic to bring joy and wonder to those around her, but can also be a fierce defender of her friends and loved ones."
            }
        }
    };
}

public class Character
{
    public string Name;
    public string Description;
}

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
        CharacterInfoPanel.SetActive(true);
        Portrait.sprite = CharacterPortraits[characterID-1].sprite;
        var character = GameInfo.Instance.Characters[characterID-1];
        Name.text = character.Name;
        Description.text = character.Description;
    }
}