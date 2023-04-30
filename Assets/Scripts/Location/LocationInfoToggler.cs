using System;
using System.Linq;
using UnityEngine;

public class LocationInfoToggler : MonoBehaviour
{
    public static event Action<Location> SelectedLeader;

    [SerializeField] private GameObject locationInfoPrefab;

    private LocationSelector locationSelector;
    private GameObject instantiatedInfo;
    private Location location;

    public void Start()
    {
        locationSelector = GetComponent<LocationSelector>();
        locationSelector.LocationSelected += OnShowInfo;
        locationSelector.ClickedOutside += HideInfo;
        location = GetComponent<Location>();
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
        if (locationInfoPrefab == null || instantiatedInfo != null)
            return;

        var locationInfo = GameInfo.Instance.Locations.FirstOrDefault(x => x.ID == location.LocationID);
        if(locationInfo == null)
        {
            Debug.Log("Location not found in GameInfo for ID: " + location.LocationID);
            return;
        }
        
        instantiatedInfo = Instantiate(locationInfoPrefab.gameObject, transform, true);
        instantiatedInfo.GetComponent<RectTransform>().localPosition = new Vector2(0, 0.2f);
        instantiatedInfo.GetComponent<SetLocationInfo>().SetLocation(locationInfo);
        instantiatedInfo.GetComponent<FadeEffect>().FadeIn();
    }
}