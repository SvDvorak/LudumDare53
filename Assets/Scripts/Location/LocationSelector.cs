using System;
using UnityEngine;

[RequireComponent(typeof(Location))]
public class LocationSelector : MonoBehaviour
{
    public delegate void LocationSelectedHandler(GameObject droppedCharacter, Location selectedLocation);
    public static event LocationSelectedHandler DroppedCharacterOnValidLocation;
    public static event Action<GameObject> ValidLocationSelected;
    public event Action ClickedOutside;

    private SpriteRenderer spriteRenderer;
    private PlayerGroup playerGroup;
    private Location location;

    /// <summary>
    /// Keeps track if your mouse is over this hitbox
    /// </summary>
    private bool isMouseOver = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerGroup = FindObjectOfType<PlayerGroup>();
        location = GetComponent<Location>();

        CharacterMouseMover.DroppedCharacter += OnDroppedCharacter;
    }

    private void OnDestroy()
    {
        CharacterMouseMover.DroppedCharacter -= OnDroppedCharacter;
    }

    private void OnDroppedCharacter(GameObject droppedCharacter)
    {
        if (isMouseOver)
        {
            if (playerGroup.HasEnteredTargetLocation && IsConnected())
            {
                isMouseOver = false;
                DroppedCharacterOnValidLocation?.Invoke(droppedCharacter, location);
            }
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseOver && playerGroup.HasEnteredTargetLocation && IsConnected())
            {
                ValidLocationSelected?.Invoke(gameObject);
            }
            else if (!isMouseOver)
            {
                ClickedOutside?.Invoke();
            }
        }
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        if (CharacterMouseMover.IsMovingObject)
        {
            spriteRenderer.color = Color.black;
            ValidLocationSelected?.Invoke(gameObject);
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// Detects if this location that you clicked on is connected to the playerGroup's current location
    /// </summary>
    /// <returns></returns>
    public bool IsConnected()
    {
        return playerGroup.currentLocation.ConnectedLocations.Find(x => x.gameObject.name.Equals(gameObject.name));
    }
}
