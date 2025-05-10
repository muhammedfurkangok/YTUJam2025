using System;
using Random = UnityEngine.Random;

public class CardManager : SingletonMonoBehaviour<CardManager>
{
    public Card sceneCard;
    public Card_SO cardSO;


    private void Start()
    {
        RandomCard();
    }

    public void RandomCard()
    {
        var randomIndex = Random.Range(0, cardSO.cardData.Length);
        sceneCard.SetCardData(cardSO.cardData[randomIndex]);
        sceneCard.RevealCard();
    }
}