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

        public void UpdateHealthBar(float value)
        {
            HealthBar.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
            HealthText.text = $"{(int)(value * 100)}";
            HealthText.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
        }
    
        public void UpdateStaminaBar(float value)
        {
            StaminaBar.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
            StaminaText.text = $"{(int)(value * 100)}";
            StaminaText.DOColor(value < 0.3f ? Color.red : Color.white, 0.5f);
        }
        
        public void UpdateAmmoText(int currentAmmo, int maxAmmo, bool infiniteAmmo = false)
        {
            if (!infiniteAmmo)
            {
                AmmoText.text = $"{currentAmmo}/{maxAmmo}";
    
                AmmoText.DOFade(0, 0.1f).OnComplete(() =>
                {
                    AmmoText.DOFade(1, 0.1f);
                });
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