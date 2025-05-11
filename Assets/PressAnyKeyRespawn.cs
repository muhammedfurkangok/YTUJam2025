using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PressAnyKeyRespawn : MonoBehaviour
{
    public CanvasGroup revealCanvasGroup;
    public TextMeshProUGUI pressAnyKeyText;


    private void Start()
    {
        pressAnyKeyText.transform.DOScale( 1.2f, 2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DOVirtual.Float(revealCanvasGroup.alpha, 1f, 0.5f, alpha => { revealCanvasGroup.alpha = alpha; })
                .OnComplete(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay"); });
        }
    }
}