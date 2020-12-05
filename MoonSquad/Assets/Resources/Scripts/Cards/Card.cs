using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public void SetName(int val)
    {
        transform.name = "Card_" + val;
    }
}
