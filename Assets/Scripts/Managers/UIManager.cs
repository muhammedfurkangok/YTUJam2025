using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Import DOTween
using Utilities;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public Image HealthBar;
    public Image StaminaBar;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI AmmoText;


    private void Start()
    {
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth; // Calculate the fill amount
        HealthBar.DOFillAmount(fillAmount, 0.5f).SetEase(Ease.OutQuad); // Update the health bar fill
        HealthText.text = $"{currentHealth}"; // Update the health text
        HealthText.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo); // Add scaling animation
    }

    public void UpdateStaminaBar(float currentStamina, float maxStamina)
    {
        float fillAmount = currentStamina / maxStamina; 
        StaminaBar.DOFillAmount(fillAmount, 0.5f).SetEase(Ease.OutQuad);
        StaminaText.text = $"{(int)currentStamina}"; 
        StaminaText.DOColor(fillAmount < 0.3f ? Color.red : Color.white, 0.5f); 
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo, bool infiniteAmmo = false)
    {
        if (!infiniteAmmo)
        {
            AmmoText.text = $"{currentAmmo}/{maxAmmo}";

            AmmoText.DOFade(0, 0.1f).OnComplete(() => { AmmoText.DOFade(1, 0.1f); });
        }
        else
        {
            AmmoText.text = $"";
        }
    }

    public void ShowDoctorSyringeUI()
    {
    }
}