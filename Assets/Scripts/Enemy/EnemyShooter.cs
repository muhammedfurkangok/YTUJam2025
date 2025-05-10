using UnityEngine;
            
            namespace Enemy
            {
                public class EnemyShooter : EnemyBase
                {
                    public GameObject projectilePrefab;
                    public Transform shootPoint;
            
                    [Header("Attack Settings")] [SerializeField]
                    private float attackCooldown = 3f;
            
                    private float bulletspeed = 20f;
                    [SerializeField] private float lastAttackTime = Mathf.NegativeInfinity;
            
                    protected override void Update()
                    {
                        if (isDead) return;
            
                        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            
                        if (distanceToPlayer <= attackRange)
                        {
                            LookAtPlayer();
            
                            if (Time.time >= lastAttackTime + attackCooldown)
                            {
                                Attack();
                                lastAttackTime = Time.time;
                            }
            
                            SetAnimationSpeed(0); // Sniper waits after firing
                        }
                        else
                        {
                            SetAnimationSpeed(0); // No movement when out of range
                        }
                    }
            
                    private void LookAtPlayer()
                    {
                        Vector3 direction = (player.position - transform.position).normalized;
                        direction.y = 0f;
                        if (direction != Vector3.zero)
                            transform.forward = direction;
                    }
            
                    public override void Attack()
                    {
                        spriteDirectionalController.animator.SetTrigger("Attack");
            
                        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            Vector3 dir = (player.position - shootPoint.position).normalized;
                            rb.linearVelocity = dir * bulletspeed; // Adjust bullet speed as needed
                        }
            
                        // Play sniper shot sound if needed
                        // enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.RobotAttack).GetRandomClip());
                    }
            
                    protected override void Die()
                    {
                        base.Die();
                        // Play death sound if needed
                        // enemyAudioSource.PlayOneShot(AudioClips.Find(x => x.key == SoundType.RobotDeath).GetRandomClip());
                    }
                }
            }