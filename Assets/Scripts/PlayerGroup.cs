using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerGroup : MonoBehaviour
{
    public event Action EnteredLocation;

    public Location currentLocation;
    private Transform target;

    public bool HasEnteredTargetLocation { get; private set; }

    private void Start()
    {
        // The group is on the starting location by default
        HasEnteredTargetLocation = true;
        LocationSelector.ValidLocationSelected += OnMoveTowardsLocation;
    }

    private void OnDestroy()
    {
        LocationSelector.ValidLocationSelected -= OnMoveTowardsLocation;
    }

    private void OnMoveTowardsLocation(Location location)
    {
        currentLocation = location;
        target = location.transform;
        HasEnteredTargetLocation = false;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.01)
            {
                EnteredLocation?.Invoke();
                HasEnteredTargetLocation = true;
                target = null;
            }
        }
    }
}
