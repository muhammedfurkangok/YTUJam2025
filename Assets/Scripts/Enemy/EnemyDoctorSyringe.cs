using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyDoctorSyringe : EnemyBase
    {
        private float attackCooldown = 2f;
        private float lastAttackTime = 0f;

        [Header("Health Settings")] public float maxHealth = 1000f;
        private float currentHealth;

        [Header("UI Settings")] public GameObject bossHealthBarBackground;
        public Image bossHealthBarFill;

        [Header("Attack Settings")] public GameObject projectilePrefab; // Projectile prefab
        public Transform shootPoint; // Point from where the projectile is shot
        private float projectileSpeed = 20f;

        private void Start()
        {
            currentHealth = maxHealth;
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

        public override void Attack()
        {
            spriteDirectionalController.animator.SetTrigger("Attack");

            ShootProjectile();
        }

        private void ShootProjectile()
        {
            // Instantiate the projectile at the shoot point
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;

                // Set the projectile's velocity
                rb.linearVelocity = directionToPlayer * projectileSpeed;
            }
        }

        public override void TakeDamage(float damage)
        {
            if (isDead) return;

            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            bossHealthBarFill.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                spriteDirectionalController.animator.SetTrigger("Die");
                Die();
            }
        }

        public override void TakeDamage(float damageAmount, bool isHeadShot)
        {
            if (isDead) return;

            if (isHeadShot)
            {
                damageAmount *= 0.5f;
            }

            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            bossHealthBarFill.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                spriteDirectionalController.animator.SetTrigger("Die");
                Die();
            }
        }

        protected override void Die()
        {
            base.Die();
            HealhBarCloseAnimation();
            SuicideManager.Instance.Suicide();
        }

        private void HealthBarInitializeAnimation()
        {
            bossHealthBarBackground.gameObject.SetActive(true);

            bossHealthBarBackground.transform.DOScale(new Vector3(1, 1, 1), 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    bossHealthBarFill.fillAmount = 0f;
                    bossHealthBarFill.DOFillAmount(1f, 2f);
                });
        }

        private void HealhBarCloseAnimation()
        {
            bossHealthBarBackground.transform.DOScale(new Vector3(0, 1, 1), 0.5f)
                .SetEase(Ease.InBack);
            bossHealthBarFill.fillAmount = 0f;
        }
    }
}