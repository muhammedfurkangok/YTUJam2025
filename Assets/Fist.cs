using System.Collections;
    using UnityEngine;
    using Utilities;
    
    public class Fist : WeaponBase
    {
        private bool isKillScoutAnimationPlaying = false;
    
        protected override void PlayFireEffects()
        {
            if (isKillScoutAnimationPlaying)
                return; // Prevent other animations while KillScout is active
    
            base.PlayFireEffects();
            WeaponManager.Instance.ShakeCamera();
            AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);
    
            if (!isKillScoutAnimationPlaying)
            {
                StartCoroutine(PlayKillScoutAnimation());
            }
        }
    
        private IEnumerator PlayKillScoutAnimation()
        {
            isKillScoutAnimationPlaying = true;
    
            // Trigger the KillScout animation
            weaponAnimator.SetTrigger("KillScout");
    
            // Wait for the animation to complete (1 second)
            yield return new WaitForSeconds(1f);
    
            isKillScoutAnimationPlaying = false;
        }
    }