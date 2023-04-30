using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LocationSelector))]
public class Location : MonoBehaviour
{
    public string LocationID;
    public List<Location> ConnectedLocations;

    public void Update()
    {
#if UNITY_EDITOR
        foreach(var loc in ConnectedLocations)
        {
            if(!loc.ConnectedLocations.Contains(this))
            {
                loc.ConnectedLocations.Add(this);
                Debug.Log("Connected " + name + " in " + loc.name);
                EditorUtility.SetDirty(loc);
            }
        }
#endif
    }
}