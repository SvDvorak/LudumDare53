using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMouseMover : MonoBehaviour, IPointerUpHandler, IDragHandler
{
    public delegate void DroppedCharacterEventHandler(GameObject droppedCharacter);
    public static event DroppedCharacterEventHandler DroppedCharacter;

    private Camera camera;
    private GameObject canvas;

    private static GameObject followingObject;

    public static bool IsMovingObject => followingObject != null;

    public void Start()
    {
        camera = GetComponent<Camera>();
        canvas = GameObject.Find("UI");
    }

    private void Update()
    {
        if (followingObject != null)
            followingObject.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(followingObject);
        followingObject = null;
        DroppedCharacter?.Invoke(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            if (followingObject != null)
                return;

            followingObject = new GameObject();
            var image = followingObject.AddComponent<Image>();
            image.sprite = GetComponent<Image>().sprite;
            image.color = new Color(1, 1, 1, 0.8f);
            followingObject.transform.SetParent(canvas.transform);
            followingObject.transform.position = Input.mousePosition;
        }
    }
}
