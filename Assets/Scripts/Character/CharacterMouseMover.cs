using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMouseMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Camera camera;
    [SerializeField] private Canvas canvas;

    private static GameObject followingObject;

    public static bool IsMovingObject => followingObject != null;

    public void Start()
    {

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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
