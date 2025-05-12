using UnityEngine;
using Utilities;
using Weapon;
using Enemy;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [Header("Weapon Setup")]
    public WeaponData data;
    public Transform firePoint;
    public Transform refBullet;
    public Animator weaponAnimator;

    protected int currentAmmo;
    protected float lastFireTime;
    protected bool isReloading = false;
    protected bool isFiring = false;

    private GameObject muzzleFlashInstance;

    protected virtual void Start()
    {
        currentAmmo = data.reloadAmmo;

        if (data.muzzleFlashPrefab != null)
        {
            muzzleFlashInstance = Instantiate(data.muzzleFlashPrefab, firePoint);
            muzzleFlashInstance.SetActive(false);
        }
    }

    public virtual void Fire()
    {
        // Fire conditions check
        if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + data.fireRate)
            return;

        if (!data.isNoNeedAmmo)
            currentAmmo--;

        lastFireTime = Time.time;

        ShowMuzzleFlash();
        PlayFireEffects();

        // RAYCAST: Crosshair hizasından ateşle
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            HandleHit(hit);

            // Görsel mermi, hedefe doğru instantiate edilir
            Vector3 direction = (hit.point - firePoint.position).normalized;
            SpawnVisualBullet(direction);
        }
        else
        {
            // Hedef yoksa ileri yönde visual mermi gönder
            SpawnVisualBullet(firePoint.forward);
        }

        // UI güncelle
        UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo, data.isNoNeedAmmo);

        if (currentAmmo == 0)
            Reload();
    }

    private void HandleHit(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Head"))
        {
            var enemy = hit.collider.GetComponentInParent<EnemyBase>();
            if (enemy != null && !enemy.isDead)
            {
                enemy.TakeDamage(100f, true);

                var feedback = FindObjectOfType<MoreMountains.Feedbacks.MMF_Player>();
                var text = feedback.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_FloatingText>();
                text.Value = "HEADSHOT!!!";
                feedback.PlayFeedbacks(enemy.agent.transform.position + Vector3.up * 1.1f);

                AudioManager.Instance.PlayOneShotSound(SoundType.HeadExplosion);
            }
        }
        else if (hit.collider.CompareTag("Enemy"))
        {
            if (WeaponManager.Instance.isOnlyHsMode) return;

            var enemy = hit.collider.GetComponentInParent<EnemyBase>();
            if (enemy != null && !enemy.isDead)
            {
                float randomDamage = Random.Range(20f, 45f);
                enemy.TakeDamage(randomDamage);

                var feedback = FindObjectOfType<MoreMountains.Feedbacks.MMF_Player>();
                var text = feedback.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_FloatingText>();
                text.Value = randomDamage.ToString("F0");
                feedback.PlayFeedbacks(enemy.agent.transform.position + Vector3.up * 1.1f, randomDamage);
            }
        }
    }

    private void SpawnVisualBullet(Vector3 direction)
    {
        var bullet = Instantiate(data.bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        bullet.Initialize(direction);
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

    private void FinishReload()
    {
        int ammoToReload = Mathf.Min(data.reloadAmmo, data.maxAmmo);
        data.maxAmmo -= ammoToReload - currentAmmo;
        currentAmmo = ammoToReload;

        UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo);
        isReloading = false;
    }

    protected virtual void ShowMuzzleFlash()
    {
        if (muzzleFlashInstance == null) return;
        muzzleFlashInstance.SetActive(true);
        Invoke(nameof(HideMuzzleFlash), 0.05f);
    }

    private void HideMuzzleFlash()
    {
        if (muzzleFlashInstance) muzzleFlashInstance.SetActive(false);
    }

    protected virtual void PlayFireEffects()
    {
        weaponAnimator.SetTrigger("Fire");
        AudioManager.Instance.PlayOneShotSound(data.fireSound);
        Camera.main?.DoShakeCamera(0.1f, 0.1f, 5, 90f);
    }

       public virtual void ResetFiringState()
    {
        isFiring = false;
        weaponAnimator.ResetTrigger("Fire");
    }
    public bool IsAuto() => data.isAuto;
}
