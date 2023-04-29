using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Location))]
public class LocationSelector : MonoBehaviour
{
    public static event Action<Location> ValidLocationSelected;
    private PlayerGroup playerGroup;
    private Location location;

    /// <summary>
    /// Keeps track if your mouse is over this hitbox
    /// </summary>
    private bool isMouseOver = false;

    void Start()
    {
        playerGroup = FindObjectOfType<PlayerGroup>();
        location = GetComponent<Location>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0) && playerGroup.HasEnteredTargetLocation && IsInList())
            ValidLocationSelected?.Invoke(location);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    /// <summary>
    /// Detects if this location that you clicked on is connected to the playerGroup's current location
    /// </summary>
    /// <returns></returns>
    private bool IsInList()
    {
        return playerGroup.currentLocation.ConnectedLocations.Find(x => x.gameObject.name.Equals(gameObject.name));
    }
}
