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
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.RobotDeath).GetRandomClip());
        }
        public override void Attack()
        {
            spriteDirectionalController.animator.SetTrigger("Attack");
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.RobotAttack).GetRandomClip());
            PlayerStatsManager.Instance.DecreaseHealth( damage);
        }
        
        public void WalkSound()
        {
            if(enemyAudioSource.isPlaying) return;
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.RobotWalk).GetRandomClip());
        }
    }
}