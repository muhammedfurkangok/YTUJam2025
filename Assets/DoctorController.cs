using DG.Tweening;
using System;
using UnityEngine;

public class DoctorController : MonoBehaviour
{
    [SerializeField] DOTweenPath doTweenPath;

    [SerializeField] Animator animator;

    public static event Action OnDoctorStab;
    public static event Action OnDoctorEnject;
    public static event Action OnDoctorAttackEnd;


    private void Start()
    {
        Invoke(nameof(MoveDoc), 1f);
    }

    public void MoveDoc()
    {
        animator.SetTrigger("Walk");
        doTweenPath.tween.Play().OnComplete(() => animator.SetTrigger("Attack"));
    }

    public void OnDoctorStabAnimationEvent()
    {
        OnDoctorStab?.Invoke();
    }

    public void OnDoctorEnjectAnimationEvent()
    {
        OnDoctorEnject?.Invoke();
    }
    public void OnDoctorAttackEndAnimationEvent()
    {
        OnDoctorAttackEnd?.Invoke();
    }
}
