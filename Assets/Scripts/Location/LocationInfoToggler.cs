using System;
using System.Linq;
using UnityEngine;

public class LocationInfoToggler : MonoBehaviour
{
    public static event Action<Location> SelectedLeader;

    [SerializeField] private GameObject locationInfoPrefab;

    private Camera camera;
    private Canvas canvas;
    private LocationSelector locationSelector;
    private GameObject instantiatedInfo;
    private Location location;

    public void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        locationSelector = GetComponent<LocationSelector>();
        locationSelector.LocationSelected += OnShowInfo;
        locationSelector.ClickedOutside += HideInfo;
        location = GetComponent<Location>();
        canvas = GameObject.Find("OverlayUI").GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        locationSelector.LocationSelected -= OnShowInfo;
        locationSelector.ClickedOutside -= HideInfo;
    }

    private void HideInfo()
    {
        if (instantiatedInfo != null)
        {
            instantiatedInfo.GetComponent<FadeEffect>().FadeOutAndDestroy();
            instantiatedInfo = null;
        }
    }

    private void OnShowInfo()
    {
        if ((!locationSelector.IsConnected() && CharacterMouseMover.IsMovingObject) || locationInfoPrefab == null || instantiatedInfo != null)
            return;

        var locationInfo = GameInfo.Instance.Locations.FirstOrDefault(x => x.ID == location.LocationID);
        if(locationInfo == null)
        {
            Debug.Log("Location not found in GameInfo for ID: " + location.LocationID);
            return;
        }
        
        instantiatedInfo = Instantiate(locationInfoPrefab.gameObject, canvas.transform);
        
        Vector2 screenPoint = camera.ScreenToWorldPoint(transform.position);
        instantiatedInfo.GetComponent<RectTransform>().localPosition = screenPoint;

        instantiatedInfo.GetComponent<SetLocationInfo>().SetLocation(locationInfo);
        instantiatedInfo.GetComponent<FadeEffect>().FadeIn();
    }
}