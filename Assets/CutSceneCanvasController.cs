using UnityEngine;
using UnityEngine.UI;

public class CutSceneCanvasController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CanvasGroup group;
    public void Cut()
    {
        animator.SetBool("Cut", true);
    }
    private void AlphaChange()
    {
        group.alpha = 0;
    }
    private void ResetCutscene() //event 
    {
        Invoke(nameof(AlphaChange), 0f);
        animator.SetBool("Cut", false);
    }
}
