using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfoToggler : MonoBehaviour
{
    public static event Action<Location> SelectedLeader;

    [SerializeField] private GameObject locationInfoPrefab;

    private LocationSelector locationSelector;
    private GameObject instantiatedInfo;

    public void Start()
    {
        locationSelector = GetComponent<LocationSelector>();
        locationSelector.LocationSelected += OnShowInfo;
        locationSelector.ClickedOutside += HideInfo;
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
            Destroy(instantiatedInfo);
            instantiatedInfo = null;
        }
    }

    private void OnShowInfo()
    {
        if (locationInfoPrefab == null)
            return;

        instantiatedInfo = Instantiate(locationInfoPrefab.gameObject);
        instantiatedInfo.transform.SetParent(transform);
        instantiatedInfo.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(0, 0.2f);
    }
}
