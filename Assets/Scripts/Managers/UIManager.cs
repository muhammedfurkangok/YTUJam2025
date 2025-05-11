using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public Image HealthBar;
    public Image StaminaBar;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI AmmoText;

    public void UpdateHealthBar(float value)
    {
        HealthBar.fillAmount = value;
        HealthText.text = $"{value * 100}";
    }

    public void UpdateStaminaBar(float value)
    {
        StaminaBar.fillAmount = value;
        StaminaText.text = $"{(int)(value * 100)}";
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo, bool infiniteAmmo = false)
    {
        if (!infiniteAmmo)
        {
            AmmoText.text = $"{currentAmmo}/{maxAmmo}";
        }
        else
        {
            AmmoText.text = $"";
        }
    }
}