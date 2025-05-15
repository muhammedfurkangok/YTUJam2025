using UnityEngine;
using Utilities;
using Weapon;

public class Pistol : WeaponBase
{
  
    
    public override void PlayFireSound()
    {
        AudioManager.Instance.PlayOneShotSound(SoundType.PistolShoot);
        WeaponManager.Instance.ShakeCamera();
    }

    public override void Reload()
    {
        base.Reload();
        AudioManager.Instance.PlayOneShotSound(SoundType.PistolReload);
    }

    public override void PlayReloadSound()
    {
        AudioManager.Instance.PlayOneShotSound(SoundType.PistolReload);
    }
}