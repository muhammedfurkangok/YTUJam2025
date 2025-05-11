using DG.Tweening;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    [SerializeField] CanvasGroup pressToConti;
    private void OpenPressTo() //EVENT FUNC
    {
        pressToConti.DOFade(1f, 1f);
    }
}
