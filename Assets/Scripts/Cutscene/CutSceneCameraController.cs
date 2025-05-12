using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneCameraController : SingletonMonoBehaviour<CutSceneCameraController>
{
    [SerializeField] Animator animator;
    [SerializeField] CinemachineCamera cam;
    
    public GameObject[] allObjectsToDisable;

    public void Cut()
    {
        cam.gameObject.SetActive(true);
        DisableObjects();
        cam.Priority = 10;
        animator.SetBool("Cut", true);
    }

    public void ResetCutscene() //event 
    {
        cam.gameObject.SetActive(false);
        cam.Priority = -10;
       animator.SetBool("Cut", false);
    }

    public void DisableObjects()
    {
        foreach (var obj in allObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
    
    public void ActivateObjects()
    {
        foreach (var obj in allObjectsToDisable)
        {
            obj.SetActive(true);
        }
    }
}