using UnityEngine;
using Utilities;

public class Fist : WeaponBase
{
    protected override void PlayFireEffects()
    {
        base.PlayFireEffects();
        // Rifle özel sarsıntısı
        WeaponManager.Instance.ShakeCamera();
        AudioManager.Instance.PlayOneShotSound(SoundType.FistHit);
    }
}
