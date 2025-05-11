using System;
using UnityEngine;

public class CutSceneController : SingletonMonoBehaviour<CutSceneController>
{
    [SerializeField] CutSceneCanvasController canvasCut;
    [SerializeField] CutSceneCameraController cameraCut;
    [SerializeField] DoctorController doc;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F)) CutSceneStart();
    }

    public void CutSceneStart() //all are auto-reseted when done.
    {
        cameraCut.Cut();
        canvasCut.Cut();
        doc.DocMove();
    }
}
