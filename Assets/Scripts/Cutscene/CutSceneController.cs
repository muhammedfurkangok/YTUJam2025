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
        CardManager.Instance.RandomCard();
    }

  
    public void CutSceneStart() //all are auto-reseted when done.
    {
        cameraCut.Cut();
        canvasCut.Cut();

        doc.DocMove();
    }
}
