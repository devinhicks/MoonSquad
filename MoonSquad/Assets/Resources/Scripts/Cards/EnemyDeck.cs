using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : Deck
{
    [SerializeField]
    private float chanceAttack = 0.75f;
    [SerializeField]
    private float chanceHeal = 0.2f;
    [SerializeField]
    private float chanceSkip = 0.05f;

    // instance
    public static EnemyDeck instance;
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
            card.GetComponent<EnemyCard>().CreateAttackCard(count); // call attack card function

        else if (num > chanceAttack && num < (chanceAttack + chanceHeal))
            card.GetComponent<EnemyCard>().CreateHealCard(count); // call heal card function

        else
            card.GetComponent<EnemyCard>().CreateSkipCard(count); // call skip card function

        playableDeck.Add(Instantiate(cardPrefab, deckSpawnPoint));
    }
}
