using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

namespace Weapon.Interfaces
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon Settings")]
        public string weaponName;
        public int maxAmmo;
        public int currentAmmo;
        public int reloadAmmo;
        public float fireRate;
        public float reloadTime;
        public bool isAuto;
        public bool isFiring;
        public bool canBeEquipped = false;

        [Header("Shooting Settings")]
        public Transform firePoint;
        public Transform refBullet;
        public Bullet bulletPrefab;
        public GameObject lightFlash;

        public Animator weaponAnimator;
        // public Animator weaponUIAnimator;
        // public RectTransform weaponImage; 
        // public RectTransform weaponIcon;
        // public Vector2 WeaponImageNormalSize;
        // public Vector2 WeaponIconNormalSize;
        protected bool isReloading = false;
        protected float lastFireTime = 0f;

        public abstract void Fire();
        public abstract void Reload();
        public abstract void ResetFiringState();

        protected void EnableLightFlash()
        {
            lightFlash.SetActive(true);
            Invoke(nameof(DisableLightFlash), 0.051f);
        }

        protected void DisableLightFlash()
        {
            lightFlash.SetActive(false);
        }
    }
}