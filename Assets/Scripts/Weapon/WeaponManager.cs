using UnityEngine;
using Weapon;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase[] weapons;
    private int currentWeaponIndex;
    private WeaponBase activeWeapon;

    private void Update()
    {
        HandleWeaponSwitch();
        if (activeWeapon == null) return;

        if (Input.GetMouseButtonDown(0) && !activeWeapon.IsAuto()) activeWeapon.Fire();
        if (Input.GetMouseButton(0) && activeWeapon.IsAuto()) activeWeapon.Fire();
        if (Input.GetMouseButtonUp(0)) activeWeapon.ResetFiringState();
        if (Input.GetKeyDown(KeyCode.R)) activeWeapon.Reload();
    }

    private void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        if (activeWeapon != null) activeWeapon.gameObject.SetActive(false);

        activeWeapon = weapons[index];
        activeWeapon.gameObject.SetActive(true);
        currentWeaponIndex = index;

        UIManager.Instance.UpdateAmmoText(activeWeapon.data.reloadAmmo, activeWeapon.data.maxAmmo, activeWeapon.data.isNoNeedAmmo);
        Debug.Log($"[WeaponManager] Equipped: {activeWeapon.data.weaponName}");
    }
}