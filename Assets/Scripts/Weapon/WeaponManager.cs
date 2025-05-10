using DG.Tweening;
using Runtime.System.InputSystem;
using UnityEngine;
using Weapon.Interfaces;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponBase[] weapons;
        private int currentWeaponIndex = 0;
        private WeaponBase activeWeapon;

      

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)&& weapons[0].canBeEquipped) EquipWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)&& weapons[1].canBeEquipped) EquipWeapon(1);

            if (InputManager.Instance.IsRunning())
            {
                activeWeapon?.weaponAnimator.SetFloat("Speed", 1f);
            }
            else
            {
                activeWeapon?.weaponAnimator.SetFloat("Speed", 0f);
            }
            if (activeWeapon == null) return;
            if (Input.GetMouseButtonDown(0) && !activeWeapon.isAuto) activeWeapon.Fire();
            if (Input.GetMouseButton(0) && activeWeapon.isAuto) activeWeapon.Fire();
            if (Input.GetMouseButtonUp(0)) activeWeapon.ResetFiringState();
            if (Input.GetKeyDown(KeyCode.R)) activeWeapon.Reload();
        }

        public void EquipWeapon(int index)
        {
            if (index < 0 || index >= weapons.Length) return;

            if (activeWeapon != null)
            {
                activeWeapon.gameObject.SetActive(false);
                RectTransform prevImageRect = activeWeapon.weaponImage;
                RectTransform prevIconRect = activeWeapon.weaponIcon;
                prevImageRect.DOSizeDelta(activeWeapon.WeaponImageNormalSize, 0.5f);
                prevIconRect.DOSizeDelta(activeWeapon.WeaponIconNormalSize, 0.5f);
            }

            activeWeapon = weapons[index];
            activeWeapon.gameObject.SetActive(true);
            UIManager.Instance.UpdateAmmoText(activeWeapon.currentAmmo, activeWeapon.maxAmmo);
            currentWeaponIndex = index;

            RectTransform imageRect = activeWeapon.weaponImage;
            RectTransform iconRect = activeWeapon.weaponIcon;
            Vector2 selectedImageSize = activeWeapon.WeaponImageNormalSize * 1.5f;
            Vector2 selectedIconSize = activeWeapon.WeaponIconNormalSize * 1.5f;
            imageRect.DOSizeDelta(selectedImageSize, 0.5f);
            iconRect.DOSizeDelta(selectedIconSize, 0.5f);

            Debug.Log($"Silah Değiştirildi: {activeWeapon.weaponName}");
        }
    }
}