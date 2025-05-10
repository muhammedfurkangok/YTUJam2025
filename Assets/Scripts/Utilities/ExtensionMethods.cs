using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace Utilities
{
    public static class ExtensionMethods 
    {
        public static void DoShakeCamera(this Camera camera, float duration, float strength, int vibrato, float randomness)
        {
            Vector3 originalPosition = camera.transform.localPosition;
            camera.DOShakePosition(duration, new Vector3(strength, 0, strength), vibrato, randomness)
                  .OnComplete(() => camera.transform.localPosition = originalPosition);
        }

        public static void DoAberrate( this Volume volume, float duration)
        {
            volume.sharedProfile.TryGet<ChromaticAberration>(out var component);
            if (component == null)
            {
                Debug.LogError("No ChromaticAberration component found");
            }
            DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 1f, duration / 2).OnComplete(() =>
            {
                DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 0f, duration / 2);
            });
        }
        /// <summary>
        /// Vertical Squish used to accent the start and the end of a jump.
        /// </summary>
        public static void DoSquish(this UnityEngine.Transform squishObj, float horizontalSquishAmount, float vSquishDuration)
        {
            squishObj.transform.DOScaleX(squishObj.transform.localScale.x + horizontalSquishAmount, vSquishDuration).OnComplete(() =>
            {
                squishObj.transform.DOScaleX(squishObj.transform.localScale.x - horizontalSquishAmount, vSquishDuration);
            }
            );
        }
        public static void DoSquash(this UnityEngine.Transform squashObj, float horizontalSquashAmount, float hSquashDuration)
        {
            squashObj.transform.DOScaleY(squashObj.transform.localScale.y + horizontalSquashAmount, hSquashDuration).OnComplete(() =>
            {
                squashObj.transform.DOScaleY(squashObj.transform.localScale.y - horizontalSquashAmount, hSquashDuration);
            }
            );
        }


    }
}