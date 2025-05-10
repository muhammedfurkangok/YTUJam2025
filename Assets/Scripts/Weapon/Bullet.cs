using Enemy;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public float lifeTime = 3f;
        public float damage = 25f;
        private Rigidbody rb;

        public void Initialize(Vector3 direction)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            rb.linearVelocity = direction * speed;
            Destroy(gameObject, lifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Head"))
            {
                EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
                if (enemy.isDead) return;

                enemy.TakeDamage(100, true);
                Destroy(gameObject);

                // var MMFPlayer = FindObjectOfType<MMF_Player>();
                //
                // MMF_FloatingText floatingText = MMFPlayer.GetFeedbackOfType<MMF_FloatingText>();
                //
                // floatingText.Value = "HEADSHOT!!!";
                //
                // MMFPlayer.PlayFeedbacks(enemy.agent.transform.position + Vector3.up * 1.1f);
            }
            else if (other.CompareTag("Enemy"))
            {
                EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
                if (enemy != null)
                {
                    if (enemy.isDead) return;
                    enemy.TakeDamage(damage);
                    // var MMFPlayer = FindObjectOfType<MMF_Player>();
                    // if (MMFPlayer != null)
                    // {
                    //     damage = Random.Range(20, 45);
                    //     MMF_FloatingText floatingText = MMFPlayer.GetFeedbackOfType<MMF_FloatingText>();
                    //     floatingText.Value = damage.ToString();
                    //     MMFPlayer.PlayFeedbacks(enemy.agent.transform.position + Vector3.up * 1.1f, damage);
                    // }
                }


                Destroy(gameObject);
            }
           
        }
    }
}