using DG.Tweening;
using UnityEngine;

public class DeathCameraCutscene : MonoBehaviour
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


    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) DieAnim();
    }
    //oyuncu üstündeki kamerayı kımıldatıyor.
    public void DieAnim()
    {
        Blackout();
        transform.DORotate(rot, duration).SetEase(rotateEase);
        transform.DOMove(pos, duration).SetEase(moveEase);
    }

    private void Blackout()
    {
        blackCanvas.GetComponent<CanvasGroup>().DOFade(1f, duration + 1f).SetEase(blackoutEase);
    }

}
