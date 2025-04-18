using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickUp : Singleton<CardPickUp>
{
    [SerializeField] private CardView cardPickUp;

    private CardView cardview;
    public void Show(Card card)
    {
        cardPickUp.gameObject.SetActive(true);
        cardPickUp.Setup(card);

    }

    public void Hide()
    {
        cardPickUp.gameObject.SetActive(false);
    }
}
