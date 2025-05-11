using System.Collections;
using Enemy;
using UnityEngine;
        using Utilities;
        
        public class Fist : WeaponBase
        {
            private bool isKillScoutAnimationPlaying = false;
            private float lastKillScoutTime = Mathf.NegativeInfinity;
            private const float killScoutCooldown = 6f;
            private const float detectionRadius = 3f; // Radius to detect nearby enemies
        
            protected override void PlayFireEffects()
            {
                base.PlayFireEffects(); // Always play fire animation
                weaponAnimator.SetTrigger("Fire");
        
                // Check KillScout conditions
                if (!isKillScoutAnimationPlaying && Time.time >= lastKillScoutTime + killScoutCooldown && IsEnemyNearby())
                {
                    StartCoroutine(PlayKillScoutAnimation());
                }
            }
        
            private bool IsEnemyNearby()
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
                foreach (var hit in hits)
                {
                    if (hit.GetComponent<EnemyBase>() != null) // Check if it's an EnemyBase
                    {
                        return true;
                    }
                }
                return false;
            }
        
            private IEnumerator PlayKillScoutAnimation()
            {
                isKillScoutAnimationPlaying = true;
                lastKillScoutTime = Time.time;
        
                weaponAnimator.SetTrigger("KillScout"); // Trigger special animation
                WeaponManager.Instance.ShakeCamera(); // Camera shake
                AudioManager.Instance.PlayOneShotSound(SoundType.FistHit); // Play sound effect
        
                yield return new WaitForSeconds(1f); // Wait for animation duration
                isKillScoutAnimationPlaying = false;
            }
        }