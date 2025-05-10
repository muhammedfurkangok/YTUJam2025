using UnityEngine;
using Weapon;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public int reloadAmmo;
    public float fireRate;
    public float reloadTime;
    public bool isAuto;
    public SoundType fireSound;
    public SoundType reloadSound;
    public GameObject muzzleFlashPrefab;
    public Bullet bulletPrefab;
}