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
    public GameObject ammoPanel;

    public void UpdateHealthBar(float value)
    {
        HealthText.text = $"{value * 100}%";
    }

    public void UpdateStaminaBar(float value)
    {
        StaminaText.text = $"{(int)(value * 100)}%";
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        AmmoText.text = $"{currentAmmo}/{maxAmmo}";
    }

  
}