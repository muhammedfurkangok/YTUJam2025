using System;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] CutSceneCanvasController canvasCut;
    [SerializeField] CutSceneCameraController cameraCut;
    [SerializeField] DoctorController doc;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F)) CutSceneStart();
    }

    public void CutSceneStart()
    {
        cameraCut.Cut();
        canvasCut.Cut();
        doc.DocMove();
    }
}
