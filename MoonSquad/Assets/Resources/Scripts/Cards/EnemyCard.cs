using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card
{
    public Transform target;
    private DropSlot enemySlot;

    private bool translating;
    public bool canClick = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("EnemyCardSlot").transform;
        enemySlot = GameObject.FindGameObjectWithTag("EnemyCardSlot").
            GetComponent<DropSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (translating)
        {
            transform.position = Vector3.Lerp(transform.position,
                target.transform.position, .1f);

            if (Vector2.Distance(new Vector2(transform.position.x,
                transform.position.y), target.transform.position) < .1f)
            {
                GameManager.instance.OnEnemyCardSelected();
                translating = false;
                canClick = false;
            }
        }
    }

    public void OnMouseDown()
    {
        if (canClick)
        {
            // flip card over

            transform.SetParent(target);
            enemySlot.SetOccupied(gameObject);

            translating = true; // slide it into place

            EnemyDeck.instance.currentHand.Remove(gameObject);
        }
    }

    public void RemoveCardsFromBoard()
    {
        target = GameManager.instance.enemyDiscardPile;
        transform.SetParent(target);
        translating = true;
    }

    public void CreateAttackCard(int num)
    {
        SetName(num);
        //Debug.Log("attack card created");
    }

    public void CreateHealCard(int num)
    {
        SetName(num);
        //Debug.Log("heal card created");
    }

    public void CreateSkipCard(int num)
    {
        SetName(num);
        //Debug.Log("skip card created");
    }
}
