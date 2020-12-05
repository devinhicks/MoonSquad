using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // holds cards instantiated at start of the game
    public List<GameObject> playableDeck; // cards in deck that can be played
    public List<GameObject> currentHand; // cards currently on the board
    public List<GameObject> discardDeck; // cards that have been discarded

    public int deckSize;
    public int handSize; // 3 for enemy and 5 for player

    public Transform deckSpawnPoint;
    public Transform[] handSpawnPoints;

    public GameObject cardPrefab;

    public void InitializeDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            AddCard(i);
        }
    }

    public virtual void AddCard(int count)
    {
        // add card
    }

    public void UpdateHand()
    {
        for (int i = 0; i < handSize; i++)
        {
            currentHand.Add(playableDeck[i]);
        }

        for (int i = 0; i < handSize; i++)
        {
            playableDeck.RemoveAt(0);
        }

        playableDeck.TrimExcess();

        for (int i = 0; i < currentHand.Count; i++)
        {
            currentHand[i].transform.position = handSpawnPoints[i].transform.position;
            currentHand[i].transform.SetParent(handSpawnPoints[i].transform);
        }
    }
}
