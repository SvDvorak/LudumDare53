using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationInfoHider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action HidInfo;
    private bool isMouseOver = false;
    private bool shouldHide = true;

    public void Start()
    {
        CharacterMouseMover.DroppedCharacter += OnPreventHidingWait;
        ShowEnterLocationInfo.ShowedInfo += OnPreventHiding;
        ShowEnterLocationInfo.HidInfo += OnEnableHidingWait;
        LocationSelector.ValidLocationSelected += OnPreventHiding;
    }

    private void OnDestroy()
    {
        CharacterMouseMover.DroppedCharacter -= OnPreventHidingWait;
        ShowEnterLocationInfo.ShowedInfo -= OnPreventHiding;
        ShowEnterLocationInfo.HidInfo -= OnEnableHidingWait;
        LocationSelector.ValidLocationSelected -= OnPreventHiding;
    }

    private void OnEnableHidingWait()
    {
        StartCoroutine(WaitToHide());
    }

    private void OnPreventHiding()
    {
        shouldHide = false;
    }

    private void OnPreventHiding(GameObject selectedLocation)
    {
        OnPreventHiding();
    }

    private IEnumerator WaitToHide()
    {
        yield return new WaitForSeconds(0.1f);
        shouldHide = true;
    }

    private void OnPreventHidingWait(GameObject droppedCharacter)
    {
        shouldHide = false;
        StartCoroutine(WaitToHide());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && shouldHide && !isMouseOver && !CharacterMouseMover.IsMovingObject)
        {
            GetComponent<FadeEffect>().FadeOutAndDestroy();
            HidInfo?.Invoke();
        }
    }
}
