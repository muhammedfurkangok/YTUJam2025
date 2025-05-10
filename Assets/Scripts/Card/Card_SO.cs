using System;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Card")]
public class Card_SO : ScriptableObject
{
    public CardData[] cardData;
}


[Serializable]
public struct CardData
{
    public string CardName;
    public RawImage CardImage;
    public string CardDescription;
}
