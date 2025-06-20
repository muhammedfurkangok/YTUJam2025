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
            
            if(WeaponManager.Instance.currentWeaponIndex == 0)
            {
                Fist fist = (Fist)WeaponManager.Instance.weapons[0];
                fist.KillScoutAnimation();
            }
        }

      public override void Attack()
        {
            spriteDirectionalController.animator.SetTrigger("Attack");
        
            // Calculate direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
            // Adjust the box center to face the player
            Vector3 boxCenter = transform.position + directionToPlayer * 1.2f;
            Vector3 boxSize = new Vector3(1.5f, 2f, 1.5f); // x-y-z dimensions
        
            Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);
        
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