using System;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyDoctorSyringe : EnemyBase
    {
        private float attackCooldown = 2f;
        private float lastAttackTime = 0f;
        
        [Header("UI Settings")]
        public GameObject bossHealthBarBackground;
        public Image bossHealthBarFill;

        private void Start()
        {
            HealthBarInitializeAnimation();
        }

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

            MovementController.Instance.KillYourselfAnim();
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
        
        private void HealthBarInitializeAnimation()
        {
            bossHealthBarBackground.gameObject.SetActive(true);
            
            bossHealthBarBackground.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    bossHealthBarFill.DOFillAmount(1, 2f).OnComplete(() =>
                    {
                    });
                });
        }

        private void HealhBarCloseAnimation()
        {
            bossHealthBarBackground.transform.DOScale(new Vector3(0, 1, 1), 0.5f).SetEase(Ease.InBack);
        }
    }
}