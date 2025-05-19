using System;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCameraCutscene : SingletonMonoBehaviour<DeathCameraCutscene>
{
    [Header("Controls")]
    [SerializeField] float duration;

    [SerializeField] Vector3 rot;
    [SerializeField] Vector3 pos;

    [SerializeField] Ease rotateEase;
    [SerializeField] Ease moveEase;

    [Header("Canvas")]
    [SerializeField] Canvas blackCanvas;
    [SerializeField] Ease blackoutEase = Ease.Linear;


    private Vector3 startRot;

    private void OnEnable()
    {
        PlayerStatsManager.OnDeath += DieAnim;
    }

    private void Start()
    {
        startRot = Vector3.zero;
        
    }

   
    //oyuncu üstündeki kamerayı kımıldatıyor.
    public void DieAnim()
    {
        Blackout();
        transform.DORotate(rot, duration).SetEase(rotateEase);
        transform.DOLocalMove(pos, duration).SetEase(moveEase);
    }

    private void Blackout()
    {
        blackCanvas.GetComponent<CanvasGroup>().DOFade(1f, duration + 1f).SetEase(blackoutEase).OnComplete(() => SceneManager.LoadScene(2));
    }


    private void OnDisable()
    {
        PlayerStatsManager.OnDeath -= DieAnim;
    }
}
