using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum CardType { Attack, Defend, Heal }
    public CardType cardType;
    public string Title => data.name;

    public string Description => data.Description;

    public Sprite Image => data.Image;

    public int Energy { get; private set; }

    private readonly CardData data;


    public Card(CardData cardData)
    {
        data = cardData;
        Energy = data.Energy;
    }



}
