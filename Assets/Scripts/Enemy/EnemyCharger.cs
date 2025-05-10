using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyCharger : EnemyBase
    {
        private bool isCharging = false;
        private float chargeSpeed = 10f;
        private float chargeCooldown = 3f;
        private float lastChargeTime = 0f;
        private float chargeDuration = 2f;
        private Rigidbody rb;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }

        protected override void Update()
        {
            if (isDead) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (!isCharging && Time.time > lastChargeTime + chargeCooldown && distanceToPlayer < attackRange)
            {
                Charge();
                lastChargeTime = Time.time;
            }

            base.Update();
        }

        private void Charge()
        {
            isCharging = true;
            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * chargeSpeed + Vector3.up * (chargeSpeed * 0.2f), ForceMode.Impulse);

            Debug.Log($"{gameObject.name} oyuncuya doğru ZIPLAYARAK CHARGE attı!");
            Invoke(nameof(StopCharge), chargeDuration); 
        }

        private void StopCharge()
        {
            isCharging = false;
            agent.enabled = true;
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isCharging && collision.gameObject.CompareTag("Player"))
            {
                Attack();
                StopCharge();
            }
        }
        protected override void Die()
        {
            base.Die();
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.CrapDeath).GetRandomClip());
        }

        public override void Attack()
        {
            spriteDirectionalController.animator.SetTrigger("Attack");
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.CrapAttack).GetRandomClip());
            PlayerStatsManager.Instance.DecreaseHealth( damage);
        }
        
        public void WalkSound()
        {
            if(enemyAudioSource.isPlaying) return;
            enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.CrapWalk).GetRandomClip());
        }
    }
}