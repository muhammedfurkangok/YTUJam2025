using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class DoctorController : MonoBehaviour
{
    [Header("DOTWEENPATH")]
    [SerializeField] DOTweenPath doTweenPath;

    [Header("ANIMATOR")]
    [SerializeField] Animator animator;


    [Header("EFFECTS")]
    [SerializeField] float stabEffectDuration = 0.5f;
    [SerializeField] float enjectEffectDuration = 1f;
    [SerializeField] float endEffectDuration = 1f;


    public static event Action<float> OnDoctorStab;
    public static event Action<float> OnDoctorEnject;
    public static event Action<float> OnDoctorAttackEnd;

    Tween tweee;

    public void DocMove()
    {
        animator.SetTrigger("Walk");
        tweee = doTweenPath.tween.Play().OnComplete(() => animator.SetTrigger("Attack"));
    }

    public void OnDoctorStabAnimationEvent()
    {
        OnDoctorStab?.Invoke(stabEffectDuration);
    }

    public void OnDoctorEnjectAnimationEvent()
    {
        OnDoctorEnject?.Invoke(enjectEffectDuration);
    }
    public void OnDoctorAttackEndAnimationEvent()
    {
        OnDoctorAttackEnd?.Invoke(endEffectDuration);
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Attack");
        StartCoroutine(RewindRoutine());
    }
    IEnumerator RewindRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        tweee.Rewind();
    }
}
