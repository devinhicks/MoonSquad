using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : Deck
{
    [SerializeField]
    private float chanceAttack = 0.7f;
    [SerializeField]
    private float chanceRepair = 0.25f;
    [SerializeField]
    private float chanceSkip = 0.05f;

    // instance
    public static PlayerDeck instance;
    void Awake() { instance = this; }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            InitializeDeck();

        if (Input.GetKeyDown(KeyCode.H))
            UpdateHand();
    }

    public override void AddCard(int count)
    {
        GameObject card = cardPrefab;
        float num = Random.value;

        if (num < chanceAttack)
            card.GetComponent<Card>().SetName(count); // call attack card function
        else if (num > chanceAttack && num < (chanceAttack + chanceRepair))
            card.GetComponent<Card>().SetName(count); // call heal card function
        else
            card.GetComponent<Card>().SetName(count); // call skip card function

        playableDeck.Add(Instantiate(cardPrefab, deckSpawnPoint));
    }
}
