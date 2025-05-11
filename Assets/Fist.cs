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
        base.PlayFireEffects(); // Her zaman fire animasyonu çalışır
        weaponAnimator.SetTrigger("Fire");

        // KillScout koşulları
        if (!isKillScoutAnimationPlaying && Time.time >= lastKillScoutTime + killScoutCooldown)
        {
            StartCoroutine(PlayKillScoutAnimation());
        }
    }

    private IEnumerator PlayKillScoutAnimation()
    {
        isKillScoutAnimationPlaying = true;
        lastKillScoutTime = Time.time;

        weaponAnimator.SetTrigger("KillScout"); // Özel animasyonu tetikle
        WeaponManager.Instance.ShakeCamera(); // Kamera sarsıntısı
        AudioManager.Instance.PlayOneShotSound(SoundType.FistHit); // Ses efekti

        yield return new WaitForSeconds(1f); // Animasyon süresi kadar bekle
        isKillScoutAnimationPlaying = false;
    }
}