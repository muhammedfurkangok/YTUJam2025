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
      
    }
}