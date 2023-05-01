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

    private bool canMove = true;
    public static bool IsMovingObject => followingObject != null;

    public void Start()
    {
        camera = FindObjectOfType<Camera>();
        canvas = FindObjectOfType<Canvas>().gameObject;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (followingObject != null)
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            followingObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);
            followingObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canMove)
            return;

        //ReleaseFollowingObject();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canMove)
            return;

        if (eventData.dragging)
        {
            if (followingObject != null)
                return;

            followingObject = new GameObject();
            var image = followingObject.AddComponent<Image>();
            image.sprite = GetComponent<Image>().sprite;
            image.color = new Color(1, 1, 1, 0.8f);
            image.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            followingObject.transform.SetParent(canvas.transform);
        }
    }

    private void ReleaseFollowingObject()
    {
        Destroy(followingObject);
        followingObject = null;
        DroppedCharacter?.Invoke(gameObject);
    }

    public void Enable()
    {
        canMove = true;
    }

    public void Disable()
    {
        if (followingObject != null)
            ReleaseFollowingObject();

        followingObject = null;
        canMove = false;
    }
}
