using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridPlaceLocations : MonoBehaviour
{
    public GameObject LocationPrefab;
    
    #if UNITY_EDITOR
    [ContextMenu("Generate grid")]
    void Generate()
    {
        var locations = new List<Location>();
        foreach(Transform child in transform)
        {
            locations.Add(child.GetComponent<Location>());
        }
        
        foreach (var location in locations)
        {
            var connected = new List<Location>();
            var number = int.Parse(location.name.Split(' ')[1]);
            if(number % 7 != 0)
                connected.Add(locations[number - 1]);
            if(number % 7 != 6 && number != locations.Count - 1)
                connected.Add(locations[number + 1]);
            if(number + 7 < locations.Count + 1)
            {
                if(number >= 28)
                    connected.Add(locations[number + 7 - 1]);
                else
                    connected.Add(locations[number + 7]);
            }
            location.ConnectedLocations = connected;
            EditorUtility.SetDirty(location);
            EditorUtility.SetDirty(location.gameObject);
        }
    }
    #endif
}