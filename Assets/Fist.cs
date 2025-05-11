using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Fist : WeaponBase
{
    private bool isKillScoutAnimationPlaying = false;

    protected override void PlayFireEffects()
    {
        base.PlayFireEffects();
        // Rifle özel sarsıntısı
        WeaponManager.Instance.ShakeCamera();
        AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);

        // Start the KillScout animation if not already playing
        if (!isKillScoutAnimationPlaying)
        {
            StartCoroutine(PlayKillScoutAnimation());
        }
    }

    private IEnumerator PlayKillScoutAnimation()
    {
        isKillScoutAnimationPlaying = true;

        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Trigger the KillScout animation
        weaponAnimator.SetTrigger("KillScout");

        isKillScoutAnimationPlaying = false;
    }
}