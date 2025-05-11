using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : SingletonMonoBehaviour<CardManager>
{
    public Card sceneCard;
    public Card_SO cardSO;


    public void RandomCard()
    {
        int randomIndex = Random.Range(0, cardSO.cardData.Length);
        var data = cardSO.cardData[randomIndex];
        sceneCard.SetCardData(data);
        sceneCard.RevealCard(() => Player.PlayerCardEffectsController.Instance.ExecuteCardEffect(data));
    }
}