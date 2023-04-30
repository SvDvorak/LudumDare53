public class GameInfo
{
    public Character[] Characters = new Character[4];
    public Location[] Locations = new Location[4];

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
        public string Success;
        public string Failure;
    }

    public class EnterLocationEvent
    {
        public string LocationID;
        public string EventText;
    }

    public class ItemOrAttribute
    {
        public string ItemID;
        public string ItemName;
        public string ItemDescription;
        //public EnterLocationEvent[] EnterLocationEvents;
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
                Success = "You have successfully entered the forest.",
                Failure = "You have failed to enter the forest."
            },
            new()
            {
                ID = "CAVE",
                Name = "Cave",
                Description = "The cave is a dark and mysterious place. It is home to many creatures, both good and evil. The walls are made of stone, with stalactites hanging from the ceiling. There are also dangers lurking in the shadows, waiting to pounce on unsuspecting travelers.",
                Success = "You have successfully entered the cave.",
                Failure = "You have failed to enter the cave."
            },
            new()
            {
                ID = "CASTLE",
                Name = "Castle",
                Description = "It's only a model.",
                Success = "You have successfully entered the castle.",
                Failure = "You have failed to enter the castle."
            }
        }
    };
}