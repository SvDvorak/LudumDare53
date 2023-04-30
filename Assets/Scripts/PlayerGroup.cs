using System;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public event Action EnteredLocation;
    public ShowEnterLocationInfo showEnterLocationInfo;

    public Location currentLocation;
    private Transform target;
    private GameObject droppedCharacter;

    public bool HasEnteredTargetLocation { get; private set; }

    private void Start()
    {
        // The group is on the starting location by default
        HasEnteredTargetLocation = true;
        transform.position = currentLocation.transform.position;
        LocationSelector.ValidLocationSelected += OnMoveTowardsLocation;
    }

    private void OnDestroy()
    {
        LocationSelector.ValidLocationSelected -= OnMoveTowardsLocation;
    }

    private void OnMoveTowardsLocation(GameObject droppedCharacter, Location selectedLocation)
    {
        currentLocation = selectedLocation;
        target = selectedLocation.transform;
        this.droppedCharacter = droppedCharacter;
        HasEnteredTargetLocation = false;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.01)
            {
                showEnterLocationInfo.ShowEnterLocation(droppedCharacter.name, currentLocation);
                //EnteredLocation?.Invoke();
                HasEnteredTargetLocation = true;
                target = null;
            }
        }
    }
}