using System;
using DG.Tweening;
using Player;
using Runtime.System.InputSystem;
using Unity.Cinemachine;
using UnityEngine;
using Weapon;

public class WeaponManager : SingletonMonoBehaviour<WeaponManager>
{
    public WeaponBase[] weapons;
    private int currentWeaponIndex;
    private WeaponBase activeWeapon;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public bool isBersekMode = false;
    public bool isOnlyHsMode = false;

    private void Start()
    {
        PlayerCardEffectsController.Berserk += BerserkMode;
        PlayerCardEffectsController.RestartAllStats += RestartAllStats;
        PlayerCardEffectsController.BoomHeadshot += OnlyHeadShotMode;
        EquipWeapon(0);
    }


    private void Update()
    {
        if(InputManager.Instance.blockMovementInput) return;
        
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

        if (!isBersekMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
        }
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        if (activeWeapon != null) activeWeapon.gameObject.SetActive(false);

        activeWeapon = weapons[index];
        activeWeapon.gameObject.SetActive(true);
        currentWeaponIndex = index;

        UIManager.Instance.UpdateAmmoText(activeWeapon.data.reloadAmmo, activeWeapon.data.maxAmmo,
            activeWeapon.data.isNoNeedAmmo);
        AudioManager.Instance.PlayOneShotSound(SoundType.ItemChange);
        Debug.Log($"[WeaponManager] Equipped: {activeWeapon.data.weaponName}");
    }

    public void ShakeCamera()
    {
        cinemachineCamera.transform.DOShakePosition(0.5f, 0.2f, 10, 90f);
    }

    private void BerserkMode()
    {
        isBersekMode = true;
    }

    private void RestartAllStats()
    {
        isBersekMode = false;
        isOnlyHsMode = false;
    }

    private void OnlyHeadShotMode()
    {
        isOnlyHsMode = true;
    }

    private void OnDisable()
    {
        PlayerCardEffectsController.Berserk -= BerserkMode;
        PlayerCardEffectsController.RestartAllStats -= RestartAllStats;
        PlayerCardEffectsController.BoomHeadshot -= OnlyHeadShotMode;
    }
}