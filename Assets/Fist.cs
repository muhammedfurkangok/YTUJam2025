using System.Collections;
using UnityEngine;
using Utilities;

public class Fist : WeaponBase
{
    private bool isKillScoutAnimationPlaying = false;
    private float lastKillScoutTime = Mathf.NegativeInfinity;
    private const float killScoutCooldown = 6f;

    protected override void PlayFireEffects()
    {
        base.PlayFireEffects(); 
       
        AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);
        WeaponManager.Instance.ShakeCamera(); 

    }

    public void KillScoutAnimation()
    {
        if (!isKillScoutAnimationPlaying && Time.time >= lastKillScoutTime + killScoutCooldown)
        {
            StartCoroutine(PlayKillScoutAnimation());
        }
         
        
        AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);
        WeaponManager.Instance.ShakeCamera(); 
    }

    private IEnumerator PlayKillScoutAnimation()
    {
        isKillScoutAnimationPlaying = true;
        lastKillScoutTime = Time.time;

        weaponAnimator.SetTrigger("KillScout"); // Özel animasyonu tetikle

        yield return new WaitForSeconds(1f); // Animasyon süresi kadar bekle
        isKillScoutAnimationPlaying = false;
    }
}