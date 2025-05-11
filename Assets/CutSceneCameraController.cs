using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneCameraController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CinemachineCamera cam;
    public void Cut()
    {
        cam.Priority = 10;
        animator.SetBool("Cut", true);
    }
    
    private void ResetCutscene() //event 
    {
        cam.Priority = -10;
        animator.SetBool("Cut", false);
    }
}
