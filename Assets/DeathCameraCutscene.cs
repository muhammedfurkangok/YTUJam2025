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


    private Vector3 startPos, startRot;
    private void Start()
    {
        startRot = transform.eulerAngles;
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) DieAnim();
        if (Input.GetKey(KeyCode.T)) ResetCamera(); 
    }
    //oyuncu üstündeki kamerayı kımıldatıyor.
    public void DieAnim()
    {
        Blackout();
        transform.DORotate(rot, duration).SetEase(rotateEase);
        transform.DOMove(pos, duration).SetEase(moveEase);
    }
    public void ResetCamera()
    {
        transform.eulerAngles = startRot;
        transform.position = startPos;
    }

    private void Blackout()
    {
        blackCanvas.GetComponent<CanvasGroup>().DOFade(1f, duration + 1f).SetEase(blackoutEase);
    }

}
