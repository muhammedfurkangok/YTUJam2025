using UnityEngine;
using UnityEngine.UI;

public class CutSceneCameraController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void Cut()
    {
        animator.SetBool("Cut", true);
    }
    
    private void ResetCutscene() //event 
    {
        animator.SetBool("Cut", false);
    }
}
