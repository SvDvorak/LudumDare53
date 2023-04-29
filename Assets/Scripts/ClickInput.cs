using UnityEditor;
using UnityEngine;

public class ClickInput : MonoBehaviour
{
    public Transform Player;
    private Camera gameCamera;
    private Transform target;

    public void Awake()
    {
        gameCamera = Camera.main;
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if(hit.collider != null && hit.collider.CompareTag("Location"))
            {
                target = hit.transform;
            }
        }

        if(target != null)
        {
            Player.position = Vector3.MoveTowards(Player.position, target.position, 3 * Time.deltaTime);
            if(Vector3.Distance(Player.position, target.position) < 0.01)
                target = null;
        }
    }
}