using System;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform defaultCameraTarget;

    public void FocusOnTarget(Action onComplete)
    {
        Debug.Log("Camera focusing on cutscene target...");
        DOVirtual.DelayedCall(2f, () =>
        {
            onComplete?.Invoke();
        });
    }

    public void ReturnToDefault()
    {
        Debug.Log("Camera returning to default...");
    }
}