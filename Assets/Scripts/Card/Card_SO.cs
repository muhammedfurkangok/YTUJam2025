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
    public Sprite CardImage;
    public Sprite CardFront;
    public Sprite CardBack;
    public string CardDescription;


    // Kartın çalıştıracağı fonksiyon
    public CardEffectType effectType;
}


public enum CardEffectType
{
    Default = 0,
    MoreBrain = 1,
    Berserk = 2,
    BoomHeadshot = 3,
    GrimReaper = 4,
    DoctorsSyringe = 5,
    DoctorFinal = 6,
    // istediğin kadar ekle
}