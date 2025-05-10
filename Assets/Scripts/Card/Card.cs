using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
    public RawImage allInOneMaterial;

    private void Start()
    {
        cardTransform.localEulerAngles = new Vector3(0, 180, 0);
        RevealCard();
    }

    public void RevealCard()
    {
        cardTransform.DOShakeRotation(
            duration: shakeDuration,
            strength: new Vector3(0, shakeStrength, 0),
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
                    DOVirtual.Float(0, 1, slowRevealDuration, (value) =>
                    {
                        allInOneMaterial.material.SetFloat("_ShineLocation", value);
                    });
                    Debug.Log("🃏 Kart Açıldı: Şimdi içeriği okuyabilirsin.");
                });
            });
        });
    }
}