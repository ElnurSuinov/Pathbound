using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLeft : MonoBehaviour, CardDropArea
{
    public void OnCardDrop(CardView cardView)
    {
        cardView.gameObject.SetActive(false);
        Destroy(cardView.gameObject);
        Debug.Log("Card was dropped on the LEFT");
    }
}
