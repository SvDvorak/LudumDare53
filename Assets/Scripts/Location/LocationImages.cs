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
        }

        public List<ImageData> imageList = new();

        public Sprite GetImage(string locationID)
        {
            return imageList.First(x => x.ID == locationID).image;
        }
    }
