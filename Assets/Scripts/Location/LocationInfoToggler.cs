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
    private GameObject instantiatedInfo;
    private Location location;

    public static bool IsShowingInfo { get; private set; }

    public void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        locationSelector = GetComponent<LocationSelector>();
        locationSelector.ValidLocationSelected += OnShowInfo;
        locationSelector.ClickedOutside += HideInfo;
        location = GetComponent<Location>();
        canvas = GameObject.Find("OverlayUI").GetComponent<Canvas>();
        ShowEnterLocationInfo.ShowedInfo += HideInfo;
    }

    private void OnDestroy()
    {
        locationSelector.ValidLocationSelected -= OnShowInfo;
        locationSelector.ClickedOutside -= HideInfo;
    }

    private void HideInfo()
    {
        if (instantiatedInfo != null)
        {
            instantiatedInfo.GetComponent<FadeEffect>().FadeOutAndDestroy();
            instantiatedInfo = null;
            outline.gameObject.SetActive(false);
            IsShowingInfo = false;
        }
    }

    private void OnShowInfo()
    {
        if ((!locationSelector.IsConnected() && CharacterMouseMover.IsMovingObject) || locationInfoPrefab == null || instantiatedInfo != null)
            return;

        var locationInfo = GameInfo.Instance.Locations.FirstOrDefault(x => x.ID == location.LocationID);
        if (locationInfo == null)
        {
            Debug.Log("Location not found in GameInfo for ID: " + location.LocationID);
            return;
        }

        instantiatedInfo = Instantiate(locationInfoPrefab.gameObject, canvas.transform);
        instantiatedInfo.GetComponent<SetLocationInfo>().SetLocation(locationInfo);
        instantiatedInfo.GetComponent<FadeEffect>().FadeIn();
        outline.gameObject.SetActive(true);
        ShowedInfo?.Invoke(transform);
        IsShowingInfo = true;
    }
}