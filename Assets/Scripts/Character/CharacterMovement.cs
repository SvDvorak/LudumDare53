using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float smoothTime;
    private Transform target;
    private Vector3 velocity;
    private Vector3 offset;
    private Vector3 linearMove;
    private float jump;
    private float jumpSpeed;
    private float jumpHeight;
    private SpriteRenderer sprite;

    private Vector3 CharacterTarget => target.position + offset;

    public void OnEnable()
    {
        offset = transform.localPosition;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetPosition(Transform newTarget)
    {
        target = newTarget;
        transform.position = linearMove = CharacterTarget;
    }

    public void MoveTo(Transform newTarget)
    {
        StartCoroutine(MoveToRoutine(newTarget));
    }

    private IEnumerator MoveToRoutine(Transform newTarget)
    {
        jumpSpeed = Random.Range(9f, 11f);
        jumpHeight = Random.Range(0.09f, 0.11f);
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        target = newTarget;
    }

    public void Update()
    {
        if(target == null)
            return;
        
        sprite.flipX = velocity.x > 0;
        linearMove = Vector3.SmoothDamp(linearMove, CharacterTarget, ref velocity, smoothTime);
        jump += Time.deltaTime * jumpSpeed;
        var jumpOffset = Mathf.PingPong(jump, 1) * velocity.magnitude;
        transform.position = linearMove + new Vector3(0, jumpOffset * jumpHeight, 0);

        if(Vector3.Distance(transform.position, CharacterTarget) < 0.01)
        {
            target = null;
        }
    }
}
