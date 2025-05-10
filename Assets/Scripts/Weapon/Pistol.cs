using DG.Tweening;
using UnityEngine;
using Utilities;
using Weapon.Interfaces;

namespace Weapon
{
    public class Pistol : WeaponBase, IWeapon
    {
        public override void Fire()
        {
            if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + fireRate)
                return;

            currentAmmo--;
            Camera baseCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            baseCamera.DoShakeCamera(0.2f, 0.3f, 10, 90f);
            EnableLightFlash();
            weaponAnimator.SetTrigger("Fire");
            AudioManager.Instance.PlayOneShotSound(SoundType.GlockShoot);
            UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
            lastFireTime = Time.time;

            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, refBullet.rotation);
            bullet.Initialize(firePoint.forward);

            if (currentAmmo == 0 && maxAmmo > 0) Reload();
        }

        public override void Reload()
        {
            if (isReloading || maxAmmo <= 0 || currentAmmo == reloadAmmo) return;
            isReloading = true;
            AudioManager.Instance.PlayOneShotSound(SoundType.GlockReload);
            weaponAnimator.SetTrigger("Reload");
            Invoke(nameof(FinishReload), reloadTime);
        }

        public override void ResetFiringState()
        {
            // weaponUIAnimator.SetBool("Shoot", false);
        }

        private void FinishReload()
        {
            int ammoToReload = Mathf.Min(reloadAmmo, maxAmmo);
            maxAmmo -= ammoToReload - currentAmmo;
            currentAmmo = ammoToReload;
            UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
            isReloading = false;
        }
    }
}