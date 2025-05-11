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

    public bool ifCutScene;

    // Kartın çalıştıracağı fonksiyon
    public CardEffectType effectType;
}


public enum CardEffectType
{
    None,
    SpawnUnit,
    HealPlayer,
    DamageEnemy,
    ChangeWeather,
    FocusOnBuilding,
    // istediğin kadar ekle
}