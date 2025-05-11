using System.Collections;
    using UnityEngine;
    using Utilities;
    
    public class Fist : WeaponBase
    {
        private bool isKillScoutAnimationPlaying = false;
        private float lastKillScoutTime = Mathf.NegativeInfinity; // Tracks the last animation time
        private const float killScoutCooldown = 5f; // Cooldown duration in seconds
    
        protected override void PlayFireEffects()
        {
            if (isKillScoutAnimationPlaying || Time.time < lastKillScoutTime + killScoutCooldown)
                return; // Prevent triggering during cooldown or while animation is playing
    
            base.PlayFireEffects();
            WeaponManager.Instance.ShakeCamera();
            AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);
    
            StartCoroutine(PlayKillScoutAnimation());
        }
    
        private IEnumerator PlayKillScoutAnimation()
        {
            isKillScoutAnimationPlaying = true;
            lastKillScoutTime = Time.time; // Update the last animation time
    
            // Trigger the KillScout animation
            weaponAnimator.SetTrigger("KillScout");
    
            // Wait for the animation to complete (1 second)
            yield return new WaitForSeconds(1f);
    
            isKillScoutAnimationPlaying = false;
        }
    }