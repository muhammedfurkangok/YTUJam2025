using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Enemy Stats")] public float health = 100f;
        public float moveSpeed = 3.5f;
        public float attackRange = 2f;
        public int damage = 10;
        public SpriteDirectionalController spriteDirectionalController;
        public Transform player;
        public NavMeshAgent agent;
        public bool isDead = false;
        public bool dontDestroy = false;
        public Transform[] patrolPoints;
        public AudioSource enemyAudioSource;
        public List<GameSound> AudioClips;

        [Header("Patrol Settings")] private int currentPatrolIndex;
        private float waitTime = 2f;
        private bool waiting = false;


        public abstract void Attack();

        public virtual void TakeDamage(float damageAmount)
        {
            if (isDead) return;

            health -= damageAmount;

            if (health <= 0)
            {
                spriteDirectionalController.animator.SetTrigger("Die");
               
                Die();
            }
        }

        public virtual void TakeDamage(float damageAmount, bool isHeadShot)
        {
            if (isDead) return;
            
            if (isHeadShot)
            {
                spriteDirectionalController.animator.SetTrigger("HeadShot");
                Die();
            }
            
        }
        

        protected virtual void Die()
        {
            isDead = true;
            agent.enabled = false;
            foreach (var component in GetComponentsInChildren<Collider>())
            { component.enabled = false; }

            foreach (var component in GetComponentsInChildren<Rigidbody>())
            { component.useGravity = false; }
            if (!dontDestroy)
                Destroy(transform.gameObject,2f);
        }

        protected virtual void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent.speed = moveSpeed;

            if (patrolPoints.Length > 0)
            {
                currentPatrolIndex = Random.Range(0, patrolPoints.Length);
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

        protected virtual void Update()
        {
            if (isDead) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            if (waiting || !agent.isActiveAndEnabled) return;

            if (agent.remainingDistance < 0.1f)
            {
                SetAnimationSpeed(0);
                StartCoroutine(WaitBeforeNextPatrol());
            }
        }

        private IEnumerator WaitBeforeNextPatrol()
        {
            waiting = true;
            yield return new WaitForSeconds(waitTime);
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            if (agent.isActiveAndEnabled) agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            waiting = false;
            SetAnimationSpeed(1);
        }

        public void SetAnimationSpeed(float speed)
        {
            spriteDirectionalController.animator.SetFloat("speed", speed);
        }
        
      
    }
}