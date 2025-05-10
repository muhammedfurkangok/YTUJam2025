using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    public Animator animator;
    public Transform characterTransform;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 movementDirection = (transform.position - lastPosition).normalized;

        Vector3 characterForward = characterTransform.forward;

        float angle = Vector3.SignedAngle(characterForward, movementDirection, Vector3.up);
        
        if (angle > -45 && angle <= 45)
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 1);
        }
        else if (angle > 45 && angle <= 135)
        {
            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);
        }
        else if (angle > -135 && angle <= -45)
        {
            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", -1);
        }
    }
}