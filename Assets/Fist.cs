using UnityEngine;
using Utilities;

public class Fist : WeaponBase
{
    protected override void PlayFireEffects()
    {
        base.PlayFireEffects();
        // Rifle özel sarsıntısı
        Camera.main?.DoShakeCamera(0.15f, 0.2f, 8, 90f);
    }
}
