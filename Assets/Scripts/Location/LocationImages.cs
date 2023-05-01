using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LocationImages", menuName = "Custom/Location Images")]
public class LocationImages : ScriptableObject
{
    [System.Serializable]
    public class ImageData
    {
        [FormerlySerializedAs("name")] public string ID;
        public Sprite image;
        public AudioClip music;
        [Range(0, 1f)]
        public float volume = 1;
    }

    public List<ImageData> imageList = new();

    public Sprite GetImage(string locationID)
    {
        return imageList.First(x => x.ID == locationID).image;
    }

    public AudioClip GetMusic(string locationID)
    {
        return imageList.First(x => x.ID == locationID).music;
    }

    public float GetVolume(string locationID)
    {
        return imageList.First(x => x.ID == locationID).volume;
    }
}
