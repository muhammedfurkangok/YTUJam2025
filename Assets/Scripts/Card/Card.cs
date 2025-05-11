using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Runtime.System.InputSystem;

public class Card : MonoBehaviour
{
    public Transform cardTransform;

    [Header("Shake Settings")]
    public float shakeDuration = 2.5f;
    public float shakeStrength = 20f;
    public int shakeVibrato = 20;

    [Header("Reveal Settings")]
    public float preRevealPause = 0.2f;
    public float slowRevealDuration = 1.5f;
    public Ease revealEase = Ease.InOutElastic;
    public Image allInOneMaterial;

    [Header("Card Settings")]
    public Image cardFront;
    public Image cardBack;
    public TextMeshProUGUI cardHeader;
    public Image cardImage;
    public TextMeshProUGUI cardDescription;
    public CanvasGroup canvasGroup;

    private CardData currentData;

    private void Start()
    {
    }

    public void SetCardData(CardData cardData)
    {
        currentData = cardData;
        cardHeader.text = cardData.CardName;
        cardImage.sprite = cardData.CardImage;
        cardDescription.text = cardData.CardDescription;
        cardFront.sprite = cardData.CardFront;
        cardBack.sprite = cardData.CardBack;
    }

    public void RevealCard(Action onRevealComplete = null)
    {
        cardTransform.localEulerAngles = new Vector3(0, 180, 0);
        canvasGroup.alpha = 1;
        cardTransform.DOShakePosition(
            duration: shakeDuration,
            strength: new Vector3(shakeStrength, shakeStrength, 0),
            vibrato: shakeVibrato,
            randomness: 100,
            fadeOut: true
        ).OnComplete(() =>
        {
            DOVirtual.DelayedCall(preRevealPause, () =>
            {
                Sequence revealSequence = DOTween.Sequence();

                revealSequence.Append(cardTransform.DORotate(new Vector3(0, 45, 0), slowRevealDuration * 0.5f)
                    .SetEase(Ease.InBack));

                revealSequence.Append(cardTransform.DORotate(new Vector3(0, 0, 0), slowRevealDuration * 0.5f)
                    .SetEase(revealEase));

                revealSequence.OnComplete(() =>
                {
                    DOVirtual.Float(0, 1, slowRevealDuration, (value) => { allInOneMaterial.material.SetFloat("_ShineLocation", value); }).OnComplete(() =>
                        {
                            DOVirtual.DelayedCall(0.5f, () =>
                            {
                                onRevealComplete?.Invoke();
                                HideCard();
                               
                            });
                        });
                });
            });
        });
    }

    public void HideCard()
    {
        cardTransform.DORotate(new Vector3(0, 180, 0), slowRevealDuration * 0.5f)
            .SetEase(Ease.InBack).OnComplete(() =>
            {
                DOVirtual.Float(1, 0, slowRevealDuration,
                    (value) => { allInOneMaterial.material.SetFloat("_ShineLocation", value); })
                    .OnComplete(() =>
                    {
                        canvasGroup.DOFade(0, slowRevealDuration).OnComplete(() =>
                        {
                            gameObject.SetActive(false);
                            CutSceneCameraController.Instance.ActivateObjects();
                            PostProcessVFXController.Instance.ResetPostProcessToNormal();
                            InputManager.Instance.blockLookInput = false;
                            InputManager.Instance.blockMovementInput = false;
                        });
                    });
            });
    }
}
