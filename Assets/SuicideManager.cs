using UnityEngine;

public class SuicideManager : SingletonMonoBehaviour<SuicideManager>
{
    [SerializeField] SpriteRenderer visual;
    [SerializeField] GameObject[] objectsToClose;
    [SerializeField] Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Suicide();
    }

    public void Suicide()
    {
        visual.enabled = true;
        foreach(var obj in objectsToClose)
        {
            obj.SetActive(false);
        }
        animator.SetTrigger("KYS");
    }

}
