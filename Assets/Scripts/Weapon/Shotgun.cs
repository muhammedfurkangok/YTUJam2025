using Enemy;
using UnityEngine;
using Utilities;
using Weapon;

public class Shotgun : WeaponBase
{
    [Header("Shotgun Settings")] 
    public int pelletCount = 8;          // Number of pellets (raycasts) per shot
    public float spreadAngle = 10f;      // Spread angle in degrees

    public override void Fire()
    {
        if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + data.fireRate)
            return;

        if (!data.isNoNeedAmmo)
            currentAmmo--;

        lastFireTime = Time.time;

        ShowMuzzleFlash();
        PlayFireEffects();

        Camera cam = Camera.main;
        Vector3 origin = cam.transform.position;

        for (int i = 0; i < pelletCount; i++)
        {
            // Random spread direction within the cone
            float randomYaw = Random.Range(-spreadAngle, spreadAngle);
            float randomPitch = Random.Range(-spreadAngle, spreadAngle);
            Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);
            Vector3 spreadDir = spreadRotation * cam.transform.forward;

            // Raycast from the camera
            if (Physics.Raycast(origin, spreadDir, out RaycastHit hit, 100f))
            {

                if (hit.collider.CompareTag("Head"))
                {
                    var enemy = hit.collider.GetComponentInParent<EnemyBase>();
                    if (enemy != null && !enemy.isDead)
                    {
                        enemy.TakeDamage(100f, true);
                        ShowFloatingText("HEADSHOT!!!", hit.point);
                        AudioManager.Instance.PlayOneShotSound(SoundType.HeadExplosion);
                    }
                }
                else if (hit.collider.CompareTag("Enemy"))
                {
                    if (WeaponManager.Instance.isOnlyHsMode) continue;

                    var enemy = hit.collider.GetComponentInParent<EnemyBase>();
                    if (enemy != null && !enemy.isDead)
                    {
                        float pelletDamage = Random.Range(20f, 35f);
                        enemy.TakeDamage(pelletDamage);
                        ShowFloatingText(pelletDamage.ToString("F0"), hit.point);
                    }
                }

                // Spawn bullet visual from firePoint towards hit point
                Vector3 directionToHit = (hit.point - firePoint.position).normalized;
                Bullet bulletVisual = Instantiate(data.bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToHit));
                bulletVisual.Initialize(directionToHit);
            }
        }

        AudioManager.Instance.PlayOneShotSound(SoundType.ShotgunShoot);
        UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo, data.isNoNeedAmmo);

        if (currentAmmo == 0)
            Reload();
    }

    protected override void PlayFireEffects()
    {
        base.PlayFireEffects();
        WeaponManager.Instance.ShakeCamera();
    }

    public override void Reload()
    {
        base.Reload();
        AudioManager.Instance.PlayOneShotSound(SoundType.ShotgunReload);
    }

    private void ShowFloatingText(string value, Vector3 position)
    {
        var mmfPlayer = FindObjectOfType<MoreMountains.Feedbacks.MMF_Player>();
        if (mmfPlayer != null)
        {
            var floatingText = mmfPlayer.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_FloatingText>();
            floatingText.Value = value;
            mmfPlayer.PlayFeedbacks(position + Vector3.up * 1.1f);
        }
    }
}
