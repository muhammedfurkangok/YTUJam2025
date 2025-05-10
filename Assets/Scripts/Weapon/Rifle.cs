using DG.Tweening;
using UnityEngine;
using Utilities;
using Weapon.Interfaces;

namespace Weapon
{
    public class Rifle : WeaponBase, IWeapon
    {
        public override void Fire()
        {
            if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + fireRate)
                return;

            currentAmmo--;
            Camera baseCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            baseCamera.DoShakeCamera(0.1f, 0.1f, 3, 90f);
            EnableLightFlash();
            // weaponUIAnimator.SetBool("Shoot", true);
            if (!isFiring)
            {
                isFiring = true;
                weaponAnimator.SetTrigger("Fire");
            }
            else if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("RifleIdle"))
            {
                weaponAnimator.SetTrigger("Fire");
            }

            AudioManager.Instance.PlayOneShotSound(SoundType.M4A1Shoot);

            UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
            lastFireTime = Time.time;

            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, refBullet.rotation);
            bullet.Initialize(firePoint.forward);

            if (currentAmmo == 0 && maxAmmo > 0)
                Reload();
        }

        public override void ResetFiringState()
        {
            isFiring = false;
            weaponAnimator.ResetTrigger("Fire");
            // weaponUIAnimator.SetBool("Shoot", false);
        }

        public override void Reload()
        {
            if (isReloading || maxAmmo <= 0 || currentAmmo == reloadAmmo) return;
            isReloading = true;

            weaponAnimator.SetTrigger("Reload");
            AudioManager.Instance.PlayOneShotSound(SoundType.M4A1Reload);
            Invoke(nameof(FinishReload), reloadTime);
        }

        private void FinishReload()
        {
            int ammoToReload = Mathf.Min(reloadAmmo, maxAmmo);
            maxAmmo -= ammoToReload - currentAmmo;
            currentAmmo = ammoToReload;
            UIManager.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
            isReloading = false;
            Debug.Log($"{weaponName} Dolduruldu!");
        }
    }
}