using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;

    private Camera camera;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        camera = GetComponent<Camera>();
        camera.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
