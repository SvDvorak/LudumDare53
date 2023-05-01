using System;
using System.Linq;
using UnityEngine;

public class LocationInfoToggler : MonoBehaviour
{
    public static event Action<Location> SelectedLeader;
    public static event Action<Transform> ShowedInfo;
    public SpriteRenderer outline;

    [SerializeField] private GameObject locationInfoPrefab;

    private Camera camera;
    private Canvas canvas;
    private LocationSelector locationSelector;
    private static GameObject instantiatedInfo;
    private Location location;

    private bool isMouseOver = false;

    public void Start()
    {
        camera = FindObjectOfType<Camera>();
        locationSelector = GetComponent<LocationSelector>();
        LocationSelector.DroppedCharacterOnValidLocation += DroppedCharacterOnValidLocation;
        LocationSelector.ValidLocationSelected += OnShowInfo;
        LocationInfoHider.HidInfo += HideInfo;
        location = GetComponent<Location>();
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnDestroy()
    {
        LocationSelector.DroppedCharacterOnValidLocation -= DroppedCharacterOnValidLocation;
        LocationSelector.ValidLocationSelected -= OnShowInfo;
        LocationInfoHider.HidInfo -= HideInfo;
    }

    private void DroppedCharacterOnValidLocation(GameObject droppedCharacter, Location selectedLocation)
    {
        OnShowInfo(selectedLocation.gameObject);
    }


    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void Update()
    {
        if (!isMouseOver && Input.GetMouseButtonUp(0))
            outline.gameObject.SetActive(false);
    }

    private void HideInfo()
    {
        instantiatedInfo = null;
    }

    private void OnShowInfo(GameObject selectedLocation)
    {
        if (!gameObject.Equals(selectedLocation) || !locationSelector.IsConnected() || CharacterMouseMover.IsMovingObject || locationInfoPrefab == null)
            return;

        var locationInfo = GameInfo.Instance.Locations.FirstOrDefault(x => x.ID == location.LocationID);
        if (locationInfo == null)
        {
            Debug.Log("Location not found in GameInfo for ID: " + location.LocationID);
            return;
        }

        if (instantiatedInfo == null)
        {
            instantiatedInfo = Instantiate(locationInfoPrefab.gameObject, canvas.transform);
            instantiatedInfo.GetComponent<SetLocationInfo>().SetLocation(locationInfo);
            instantiatedInfo.GetComponent<FadeEffect>().FadeIn();
        }
        else
        {
            instantiatedInfo.GetComponent<SetLocationInfo>().SetLocation(locationInfo);
            instantiatedInfo.GetComponent<LocationInfoPositionAdjuster>().UpdatePosition();
        }

        outline.gameObject.SetActive(true);
        ShowedInfo?.Invoke(transform);
    }
}