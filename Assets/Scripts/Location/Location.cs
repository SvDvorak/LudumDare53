using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocationSelector))]
public class Location : MonoBehaviour
{
    public string LocationID;
    public List<Location> ConnectedLocations;
}
