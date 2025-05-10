using UnityEngine;
using Utilities;
using Weapon;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [Header("Setup")] public WeaponData data;
    public Transform firePoint;
    public Transform refBullet;
    public Animator weaponAnimator;

    protected int currentAmmo;
    protected float lastFireTime;
    protected bool isReloading = false;
    protected bool isFiring = false;

    private GameObject lightFlashInstance;

    protected virtual void Start()
    {
        currentAmmo = data.reloadAmmo;
        if (data.muzzleFlashPrefab != null)
        {
            lightFlashInstance = Instantiate(data.muzzleFlashPrefab, firePoint);
            lightFlashInstance.transform.SetParent(firePoint);
            lightFlashInstance.SetActive(false);
        }
        
        
    }

    public virtual void Fire()
    {
        if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + data.fireRate)
            return;
        if (!data.isNoNeedAmmo)
            currentAmmo--;
        lastFireTime = Time.time;

        ShowMuzzleFlash();
        PlayFireEffects();

        Bullet bullet = Instantiate(data.bulletPrefab, firePoint.position, refBullet.rotation);
        bullet.Initialize(firePoint.forward);

        UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo, data.isNoNeedAmmo);

        if (currentAmmo == 0)
            Reload();
    }

    public virtual void Reload()
    {
        if (isReloading || data.maxAmmo <= 0 || currentAmmo == data.reloadAmmo)
            return;

        isReloading = true;
        weaponAnimator.SetTrigger("Reload");
        AudioManager.Instance.PlayOneShotSound(data.reloadSound);
        Invoke(nameof(FinishReload), data.reloadTime);
    }

    public virtual void ResetFiringState()
    {
        isFiring = false;
        weaponAnimator.ResetTrigger("Fire");
    }

    protected virtual void ShowMuzzleFlash()
    {
        if (lightFlashInstance == null) return;
        lightFlashInstance.SetActive(true);
        Invoke(nameof(HideMuzzleFlash), 0.05f);
    }

    private void HideMuzzleFlash()
    {
        if (lightFlashInstance) lightFlashInstance.SetActive(false);
    }

    protected virtual void PlayFireEffects()
    {
        weaponAnimator.SetTrigger("Fire");
        AudioManager.Instance.PlayOneShotSound(data.fireSound);
        Camera.main?.DoShakeCamera(0.1f, 0.1f, 5, 90f);
    }

    private void FinishReload()
    {
        int ammoToReload = Mathf.Min(data.reloadAmmo, data.maxAmmo);
        data.maxAmmo -= ammoToReload - currentAmmo;
        currentAmmo = ammoToReload;
        UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo);
        isReloading = false;
    }

    public bool IsAuto() => data.isAuto;
}