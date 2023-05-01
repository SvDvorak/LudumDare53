using System;
using System.Collections.Generic;
using System.Linq;

public class GameState
{
    public List<Character> Characters = new();
    public List<string> CarriedItems = new();
    public List<string> DeliveredItems = new();

    public class Character
    {
        public string ID;
        public bool IsAlive = true;
    }

    public GameState()
    {
        foreach(var currentCharacter in GameInfo.Instance.Characters)
            Characters.Add(new Character() { ID = currentCharacter.ID });
    }

    public List<GameInfo.Item> GetActiveItems()
    {
        var gameInfo = GameInfo.Instance;
        return CarriedItems.Select(x => gameInfo.Items.FirstOrDefault(y => y.ID == x)).ToList();
    }

    public GameInfo.ItemEvent GetLocationEvent(string locationID)
    {
        var location = GameInfo.Instance.Locations.FirstOrDefault(x => x.ID == locationID);
        if(location.Carried != null)
        {
            foreach(var carried in location.Carried)
            {
                if(Instance.CarriedItems.Contains(carried.ItemID))
                    return carried;
            }
        }
        if(location.Available != null)
        {
            foreach(var available in location.Available)
            {
                if(!Instance.DeliveredItems.Contains(available.ItemID) && !Instance.CarriedItems.Contains(available.ItemID))
                    return available;
            }
        }

        return null;
    }
    
    public bool IsGameOver() => Characters.All(x => !x.IsAlive);

    public static GameState Instance = new();
}

public class GameInfo
{
    public Character[] Characters = new Character[4];
    public Location[] Locations = Array.Empty<Location>();
    public Item[] Items = Array.Empty<Item>();
    public string PartyDeadGameOverText = "";

    public class Character
    {
        public string ID;
        public string Name;
        public string Description;
        public string[] FailureLocationIDs;
    }

    public class Location
    {
        public string ID;
        public string Name;
        public string Description;
        public string Failure;
        public List<ItemEvent> Available;
        public List<ItemEvent> Carried;
    }

    public class ItemEvent
    {
        public string ItemID;
        public string EventText;
        public string[] Changes;
        public string[] ChoiceYesChanges;
        public string[] ChoiceNoChanges;
    }

    public class Item
    {
        public string ID;
        public string Name;
        public string Description;
    }

    public static GameInfo Instance = new()
    {
        Characters = new Character[]
        {
            new()
            {
                ID = "SERAPHINA",
                Name = "Seraphina Nightshade",
                Description = "Seraphina Nightshade is a rogue with a quick wit and even quicker reflexes. She has long, flowing black hair and wears black leather armor adorned with silver studs. Seraphina is always on the lookout for her next heist or adventure, but will stand up for what's right when it matters most.",
                FailureLocationIDs = new[] {"FOREST", "CAVE", "CASTLE"}
            },
            new()
            {
                ID = "LUNA",
                Name = "Luna Silvermist",
                Description = "Luna Silvermist is an elf with an affinity for nature magic. She has long, silver hair and wears a dress made of leaves and flowers. Luna uses her powers to protect the forest and its inhabitants from those who would do them harm.",
                FailureLocationIDs = new[] {"FOREST", "CAVE", "CASTLE"}
            },
            new()
            {
                ID = "FREYA",
                Name = "Freya Stormcaller",
                Description = "Freya Stormcaller is a warrior with a heart of gold and a sword of lightning. She is tall and muscular with long, blonde hair that falls in unruly waves around her face. Freya is a champion of justice and is always ready to protect the innocent from those who would do them harm.",
                FailureLocationIDs = new[] {"FOREST", "CAVE", "CASTLE"}
            },
            new()
            {
                ID = "AURORA",
                Name = "Aurora Moonstone",
                Description = "Aurora Moonstone is a fairy with a love of mischief and a deep connection to the moon. She has delicate wings that shimmer with iridescent colors, and wears a dress made of stars. Aurora uses her powers of magic to bring joy and wonder to those around her, but can also be a fierce defender of her friends and loved ones.",
                FailureLocationIDs = new[] {"FOREST", "CAVE", "CASTLE"}
            }
        },
        Locations = new Location[]
        {
            new()
            {
                ID = "FOREST",
                Name = "Forest",
                Description = "The forest is a place of mystery and magic. It is home to many creatures, both good and evil. The trees are tall and thick, with branches that reach up into the sky. The air is filled with the sounds of birds chirping and insects buzzing. There are also dangers lurking in the shadows, waiting to pounce on unsuspecting travelers.",
                Failure = "You have failed to enter the forest.",
                Available = new List<ItemEvent>()
                {
                    new()
                    {
                        ItemID = "BOOKOFMORMON",
                        EventText = "You find a book on the ground. It's called 'The Book of Mormon'.",
                        Changes = new[] {"+BOOKOFMORMON"}
                    }
                },
                Carried = new List<ItemEvent>()
                {
                    new()
                    {
                        ItemID = "CROWN",
                        EventText = "A group of bandits approach and demand you give them the crown",
                        ChoiceYesChanges = new []{"-CROWN"},
                        ChoiceNoChanges = new []{"-CROWN"}
                    }
                }
            },
            new()
            {
                ID = "CAVE",
                Name = "Cave",
                Description = "The cave is a dark and mysterious place. It is home to many creatures, both good and evil. The walls are made of stone, with stalactites hanging from the ceiling. There are also dangers lurking in the shadows, waiting to pounce on unsuspecting travelers.",
                Failure = "You have failed to enter the cave.",
                Carried = new List<ItemEvent>()
                {
                    new()
                    {
                        ItemID = "BOOKOFMORMON",
                        EventText = "The troll takes the book and gives you a crown in return.",
                        Changes = new[] {"+CROWN", "-BOOKOFMORMON"}
                    }
                }
            },
            new()
            {
                ID = "CASTLE",
                Name = "Castle",
                Description = "It's only a model.",
                Failure = "You have failed to enter the castle.",
                Available = new List<ItemEvent>()
                {
                    new()
                    {
                        ItemID = "CROWN",
                        EventText = "I want my crown back! Go get it for me.",
                        ChoiceNoChanges = new[] {"DEFEAT You dare defy the King? You are thrown in the dungeon and left to rot."}
                    }
                },
                Carried = new List<ItemEvent>()
                {
                    new()
                    {
                        ItemID = "CROWN",
                        EventText = "You give the King his crown. He is very happy and gives you infinite riches.",
                        Changes = new[] {"VICTORY You got a whole bunch of gold, you win!"}
                    },
                    new()
                    {
                        ItemID = "BOOKOFMORMON",
                        EventText = "You filthy heathens! You will hang for this!",
                        Changes = new []{"DEFEAT You all got hung."}
                    }
                }
            }
        },
        Items = new Item[]
        {
            new()
            {
                ID = "BOOKOFMORMON",
                Name = "Book of Mormon",
                Description = "The Book of Mormon is a sacred text of the Latter Day Saint movement.",
            },
            new()
            {
                ID = "CROWN",
                Name = "King's Crown",
                Description = "The King's Crown is a symbol of power and authority. The King needs it",
            },
            new()
            {
                ID = "FANCYROBE",
                Name = "Fancy robe"
            }
        }
    };
}