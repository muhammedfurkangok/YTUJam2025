using DG.Tweening;
using System.Collections;
using UnityEngine;

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
    [SerializeField] Ease blackoutEase = Ease.InSine;


    private Vector3 startPos, startRot;
    private void Start()
    {
        startRot = Vector3.zero;
        startPos = new(0, 0.59f, 0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) PlayerStatsManager.Instance.DecreaseHealth(100);
        if (Input.GetKey(KeyCode.T))
            StartCoroutine(ResetCamera());
    }
    //oyuncu üstündeki kamerayı kımıldatıyor.
    public void DieAnim()
    {
        Blackout();
        transform.DORotate(rot, duration).SetEase(rotateEase);
        transform.DOMove(pos, duration).SetEase(moveEase).OnComplete(() => StartCoroutine(ResetCamera()));   
    }
    public IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(1.5f);
        transform.eulerAngles = startRot;
        transform.localPosition = startPos;
        CutSceneController.Instance.CutSceneStart();
        
    }

    private void Blackout()
    {
        blackCanvas.GetComponent<CanvasGroup>().DOFade(1f, duration + 1f).SetEase(blackoutEase);
    }

}
