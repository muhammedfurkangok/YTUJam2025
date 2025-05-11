using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utilities;
using static Unity.Collections.AllocatorManager;

public class PostProcessVFXController : SingletonMonoBehaviour<PostProcessVFXController>
{
    private Volume volume;

    // Cached default values
    private float defaultBloomIntensity;
    private float defaultBloomScatter;
    private float defaultBloomThreshold;
    private float defaultChromaticAberration;

    private void Start()
    {
        volume = GetComponent<Volume>();

        // Cache default Bloom values
        if (volume.sharedProfile.TryGet(out Bloom bloom))
        {
            defaultBloomIntensity = bloom.intensity.value;
            defaultBloomScatter = bloom.scatter.value;
            defaultBloomThreshold = bloom.threshold.value;
        }

        // Cache default Chromatic Aberration
        if (volume.sharedProfile.TryGet(out ChromaticAberration ca))
        {
            defaultChromaticAberration = ca.intensity.value;
        }

        // Subscribe to events
        DoctorController.OnDoctorStab += DoctorController_OnDoctorStab;
        DoctorController.OnDoctorEnject += DoctorController_OnDoctorEnject;
        DoctorController.OnDoctorAttackEnd += DoctorController_OnDoctorAttackEnd;
    }

    private void DoctorController_OnDoctorStab(float duration)
    {
        volume.DoAberrate(duration); 
    }

    private void DoctorController_OnDoctorEnject(float duration)
    {
        if (!volume.sharedProfile.TryGet(out ChromaticAberration ca)) return;

        DOTween.To(() => ca.intensity.value, x => ca.intensity.value = x, 1f, duration);

    }

    private void DoctorController_OnDoctorAttackEnd(float duration)
    {
        if (!volume.sharedProfile.TryGet(out Bloom bloom)) return;

        // Animate to strong bloom effect
        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 100f, duration*4f);
        DOTween.To(() => bloom.scatter.value, x => bloom.scatter.value = x, 1f, duration);
        DOTween.To(() => bloom.threshold.value, x => bloom.threshold.value = x, 0f, duration).OnComplete( () =>
        {
           CardManager.Instance.RandomCard();
           
        });
        
        
    }

    /// <summary>
    /// Bu fonksiyonu oyuncu kart seçtikten sonra çağırarak postprocessi düzelt.
    /// </summary>
    public void ResetPostProcessToNormal()
    {
        if (!volume.sharedProfile.TryGet(out Bloom bloom)) return;

        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, defaultBloomIntensity, 1f);
        DOTween.To(() => bloom.scatter.value, x => bloom.scatter.value = x, defaultBloomScatter, 1f);
        DOTween.To(() => bloom.threshold.value, x => bloom.threshold.value = x, defaultBloomThreshold, 1f);

        if (!volume.sharedProfile.TryGet(out ChromaticAberration ca)) return;

        ca.intensity.value = defaultChromaticAberration;

    }
    private void ResetPostProcess()
    {
        if (!volume.sharedProfile.TryGet(out Bloom bloom)) return;

        bloom.intensity.value = defaultBloomIntensity;
        bloom.scatter.value = defaultBloomScatter;
        bloom.threshold.value = defaultBloomThreshold;

        if (!volume.sharedProfile.TryGet(out ChromaticAberration ca)) return;

        ca.intensity.value = defaultChromaticAberration;
    }

    private void OnDisable()
    {
        ResetPostProcess();
    }
}
