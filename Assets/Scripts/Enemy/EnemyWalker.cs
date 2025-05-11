using UnityEngine;

namespace Enemy
{
    public class EnemyWalker : EnemyBase
    {
        private float attackCooldown = 2f;
        private float lastAttackTime = 0f;

        protected override void Update()
        {
            base.Update();

            if (isDead) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }


        protected override void Die()
        {
            base.Die();
        }

        public override void Attack()
        {
            spriteDirectionalController.animator.SetTrigger("Attack");

            // Fiziksel saldırı alanı
            Vector3 boxCenter = transform.position + transform.forward * 1.2f;
            Vector3 boxSize = new Vector3(1.5f, 2f, 1.5f); // x-y-z boyut

            Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    PlayerStatsManager.Instance.DecreaseHealth(damage);
                    break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 boxCenter = transform.position + transform.forward * 1.2f;
            Vector3 boxSize = new Vector3(1.5f, 2f, 1.5f);
            Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, boxSize);
        }

        public void WalkSound()
        {
            if (enemyAudioSource.isPlaying) return;
        }
    }
}