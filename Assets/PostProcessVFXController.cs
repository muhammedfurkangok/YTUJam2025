using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utilities;

public class PostProcessVFXController : MonoBehaviour
{
    Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
        DoctorController.OnDoctorStab += DoctorController_OnDoctorStab;
        DoctorController.OnDoctorEnject += DoctorController_OnDoctorEnject;
        DoctorController.OnDoctorAttackEnd += DoctorController_OnDoctorAttackEnd;
    }

    private void DoctorController_OnDoctorAttackEnd(float obj)
    {
        var b = volume.sharedProfile.TryGet<Bloom>(out var component);
        DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 1f, obj);
        component.scatter.Interp(0.4f, to: 1f, obj);
        component.threshold.Interp(0.75f, to: 0f, obj);

    }

    private void DoctorController_OnDoctorEnject(float obj)
    {
        var b = volume.sharedProfile.TryGet<ChromaticAberration>(out var component);
        if (!b) return;
        DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 1f, obj);
    }

    private void DoctorController_OnDoctorStab(float obj)
    {
        volume.DoAberrate(obj);
    }
}
