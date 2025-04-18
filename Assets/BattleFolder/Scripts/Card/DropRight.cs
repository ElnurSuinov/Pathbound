    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DropRight : MonoBehaviour, CardDropArea
    {
        public void OnCardDrop(CardView cardView)
        {
            cardView.gameObject.SetActive(false);
            Destroy(cardView.gameObject);
            Debug.Log("Card was dropped on the RIGHT");
        }
    }
