using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGroup : MonoBehaviour
{
    public delegate void EnterLocationEventHandler(string characterID, Location currentLocation);
    public event EnterLocationEventHandler EnteredLocation;
    public ShowEnterLocationInfo showEnterLocationInfo;
    public Location currentLocation;
    [FormerlySerializedAs("CharacterMovers")] public CharacterMovement[] CharacterMovement;
    
    private Transform target;
    private GameObject droppedCharacter;

    public bool HasEnteredTargetLocation { get; private set; }

    private void Start()
    {
        // The group is on the starting location by default
        HasEnteredTargetLocation = true;
        transform.position = currentLocation.transform.position;
        foreach(var characterMove in CharacterMovement)
            characterMove.SetPosition(transform);
        LocationSelector.DroppedCharacterOnValidLocation += OnMoveTowardsLocation;
    }

    private void OnDestroy()
    {
        LocationSelector.DroppedCharacterOnValidLocation -= OnMoveTowardsLocation;
    }

    private void OnMoveTowardsLocation(GameObject droppedCharacter, Location selectedLocation)
    {
        currentLocation = selectedLocation;
        target = selectedLocation.transform;
        this.droppedCharacter = droppedCharacter;
        HasEnteredTargetLocation = false;
        
        foreach(var characterMove in CharacterMovement)
            characterMove.MoveTo(transform);
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.01)
            {
                EnteredLocation?.Invoke(droppedCharacter.name, currentLocation);
                HasEnteredTargetLocation = true;
                target = null;
            }
        }
    }
}