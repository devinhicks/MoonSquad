using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : Card
{
    public Transform target;
    private DropSlot playerSlot;

    private bool translating;

    private bool hasBeenTouched = false;
    private Transform originalPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerCardSlot").transform;
        playerSlot = GameObject.FindGameObjectWithTag("PlayerCardSlot").
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
                GameManager.instance.OnPlayerCardSelected();
                translating = false;
            }
        }
    }

    public void OnMouseDown()
    {
        if (!hasBeenTouched)
        {
            originalPos = transform.parent;
            hasBeenTouched = true;
        }

        CheckIfOccupied(playerSlot);
        translating = true; // slide it into place
    }

    void CheckIfOccupied(DropSlot slot)
    {
        if (slot.transform.childCount > 0)
        {
            slot.ResetCardOccupier().ResetCardPosition();
        }
        slot.SetOccupied(gameObject);
        transform.SetParent(slot.transform);
    }

    void ResetCardPosition()
    {
        target = originalPos;
        transform.SetParent(originalPos);
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
