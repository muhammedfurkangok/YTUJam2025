using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public float lifeTime = 3f;

        private Rigidbody rb;

        public void Initialize(Vector3 direction)
        {
            rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.linearVelocity = direction * speed;

            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Visual mermi yalnızca duvar gibi objelerde yok edilir
            if (!other.CompareTag("Player"))
            {
            }
        }
    }
}