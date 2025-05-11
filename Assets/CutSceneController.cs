using System;
using Player;
using UnityEngine;

public class CutSceneController : SingletonMonoBehaviour<CutSceneController>
{
    [SerializeField] CutSceneCanvasController canvasCut;
    [SerializeField] CutSceneCameraController cameraCut;
    [SerializeField] DoctorController doc;

    private void Start()
    {
        CutSceneStart();
    }

  
    public void CutSceneStart() //all are auto-reseted when done.
    {
        PlayerCardEffectsController.Instance.RestartAll();
        cameraCut.Cut();
        canvasCut.Cut();
        doc.DocMove();
    }
}
