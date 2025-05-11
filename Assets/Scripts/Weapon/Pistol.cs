using UnityEngine;
using Utilities;
using Weapon;

public class Pistol : WeaponBase
{
    protected override void PlayFireEffects()
    {
        base.PlayFireEffects();
        // Rifle özel sarsıntısı
        WeaponManager.Instance.ShakeCamera();
    }

    public override void Fire()
    {
        base.Fire();
        AudioManager.Instance.PlayOneShotSound(SoundType.PistolShoot);
    }
    
    public override void Reload()
    {
        base.Reload();
        AudioManager.Instance.PlayOneShotSound(SoundType.PistolReload);
    }
}