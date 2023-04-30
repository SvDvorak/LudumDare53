using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class GameInfoLoader : MonoBehaviour
{
    public TextAsset GameInfoJson;
    public LocationImages LocationImages;
    private static string filePath => Path.Combine(Application.dataPath, "GameInfo.json");

    public void Start()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log("Loading game info...");
        if(File.Exists(filePath))
        {
            ReadGameInfo();
        }
        else
        {
            using (var writer = new StreamWriter(filePath))
            {
                var json = JsonConvert.SerializeObject(GameInfo.Instance, Formatting.Indented);
                writer.Write(json);
            }
            Debug.LogWarning("GameInfo.json not found at " + filePath);
        }
#else
        GameInfo.Instance = JsonConvert.DeserializeObject<GameInfo>(GameInfoJson.text);
#endif

        ValidateGameInfo();
    }

    public static void ReadGameInfo()
    {
        using(var reader = new StreamReader(filePath))
        {
            var json = reader.ReadToEnd();
            GameInfo.Instance = JsonConvert.DeserializeObject<GameInfo>(json);
        }
    }

    private void ValidateGameInfo()
    {
        var info = GameInfo.Instance;

        var duplicateIDs = from x in info.Locations
            group x by x
            into g
            let count = g.Count()
            where count > 1
            select new { Value = g.Key, Count = count };
        foreach(var duplicateID in duplicateIDs)
            Debug.LogError($"Duplicate ID for location {duplicateID.Value.ID}, found {duplicateID.Count} times.");

        foreach(var character in info.Characters)
        {
            foreach(var locationID in character.FailureLocationIDs)
            {
                if(!info.Locations.Any(x => x.ID == locationID))
                    Debug.LogError($"Character {character.ID} has failure location {locationID} which does not exist.");
            }
        }

        foreach(var locationImage in LocationImages.imageList)
        {
            if(!string.IsNullOrEmpty(locationImage.ID) && !info.Locations.Any(x => x.ID == locationImage.ID))
                Debug.LogError($"Location image {locationImage.ID} does not exist in GameInfo.");
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
            Start();
    }
}