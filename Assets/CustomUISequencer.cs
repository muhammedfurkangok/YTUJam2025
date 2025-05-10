using System.Collections.Generic;
using UnityEngine;

public class CustomUISequencer : MonoBehaviour
{
    [SerializeField] List<CustomUISequence> sequences;

    public void ExecuteSequence()
    {

    }
}

public class CustomUISequence : MonoBehaviour
{
    [SerializeField] GameObject uiElement;
    [SerializeField] UISequenceType type;



}
public enum UISequenceType
{
    FadeIn,
    FadeOut,
}