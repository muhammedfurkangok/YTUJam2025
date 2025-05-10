using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public Image HealthBar;
    public TextMeshProUGUI HealthText;
    public Image StaminaBar;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI AmmoText;
    public GameObject[] bulletDecals;
    public GameObject ammoPanel;

    public void UpdateHealthBar(float value)
    {
        HealthBar.fillAmount = value;
        HealthText.text = $"{value * 100}%";
    }

    public void UpdateStaminaBar(float value)
    {
        StaminaBar.fillAmount = value;
        StaminaText.text = $"{(int)(value * 100)}%";
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        AmmoText.text = $"{currentAmmo}/{maxAmmo}";
    }

    public GameObject GetRandomBulletDecalPrefab()
    {
        return bulletDecals[Random.Range(0, bulletDecals.Length)];
    }
}