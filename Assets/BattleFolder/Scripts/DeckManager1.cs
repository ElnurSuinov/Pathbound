using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckManager : MonoBehaviour
{
    [Header("Deck Settings")]
    [Tooltip("Number of cards of each type in the deck. Total deck size will be three times this amount.")]
    public int initialCardCountPerType = 3;

    [Tooltip("Number of cards to draw each turn.")]
    public int drawAmount = 5;

    [Header("Card Data References")]
    [Tooltip("Reference to the CardData for Attack cards.")]
    public CardData attackCardData;

    [Tooltip("Reference to the CardData for Defend cards.")]
    public CardData defendCardData;

    [Tooltip("Reference to the CardData for Heal cards.")]
    public CardData healCardData;

    [Header("Runtime Deck and Discard Pile")]
    [Tooltip("Main deck list.")]
    public List<Card> deck = new List<Card>();

    [Tooltip("Discard pile. Cards that are played are added here.")]
    public List<Card> discardPile = new List<Card>();

    [Header("References")]
    [Tooltip("Reference to the HandView component, which manages card positions in hand.")]
    public HandView handView;

    [Tooltip("UI text that displays the current deck count.")]
    public TMP_Text deckCountText;

    [Tooltip("UI text that displays the current discard pile count.")]
    public TMP_Text discardCountText;

    private void Start()
    {
        InitializeDeck();
        Shuffle(deck);
        UpdateUICounts();
    }

    /// <summary>
    /// Initializes the deck by creating an equal number of Attack, Defend, and Heal cards.
    /// The amount per type is defined by initialCardCountPerType.
    /// </summary>
    private void InitializeDeck()
    {
        deck.Clear();

        // Add equal number of cards for each type.
        for (int i = 0; i < initialCardCountPerType; i++)
        {
            deck.Add(new Card(attackCardData));
            deck.Add(new Card(defendCardData));
            deck.Add(new Card(healCardData));
        }

        Debug.Log("Deck initialized with " + deck.Count + " cards.");
    }

    /// <summary>
    /// Starts the draw cards routine.
    /// </summary>
    public void DrawCards()
    {
        StartCoroutine(DrawCardsRoutine());
    }

    /// <summary>
    /// Draws 'drawAmount' cards from the deck, reshuffling discard into the deck if needed.
    /// Also, creates and adds the card views to the hand using HandView.
    /// </summary>
    private IEnumerator DrawCardsRoutine()
    {
        for (int i = 0; i < drawAmount; i++)
        {
            // If the deck runs out, immediately reshuffle the discard pile
            if (deck.Count == 0)
            {
                ReshuffleDiscardIntoDeck();
            }

            if (deck.Count > 0)
            {
                // Draw the top card.
                Card drawnCard = deck[0];
                deck.RemoveAt(0);

                // Create a CardView using your provided CardViewCreator.
                CardView cardView = CardViewCreator.Instance.CreateCardView(drawnCard, handView.transform.position, Quaternion.identity);

                // Add the card to the hand view with an animation.
                yield return handView.AddCard(cardView);

                UpdateUICounts();
            }
            else
            {
                Debug.LogWarning("No cards available even after reshuffle!");
                break;
            }
        }
    }

    /// <summary>
    /// Adds a card to the discard pile.
    /// Call this method when a card is played.
    /// </summary>
    /// <param name="card">The card that should be discarded.</param>
    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
        UpdateUICounts();
    }

    /// <summary>
    /// If the deck is empty, moves all cards from the discard pile to the deck and shuffles it.
    /// </summary>
    private void ReshuffleDiscardIntoDeck()
    {
        if (discardPile.Count > 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
            Shuffle(deck);
            Debug.Log("Discard pile reshuffled into deck.");
        }
    }

    /// <summary>
    /// Simple in-place list shuffle.
    /// </summary>
    public void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    /// <summary>
    /// Updates the UI text elements with the current counts for deck and discard pile.
    /// </summary>
    private void UpdateUICounts()
    {
        if (deckCountText != null)
            deckCountText.text = deck.Count.ToString();
        if (discardCountText != null)
            discardCountText.text = discardPile.Count.ToString();
    }
}
