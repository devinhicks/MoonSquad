using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSlot : MonoBehaviour
{
    private string objectName;
    public bool occupied;
    public GameObject occupier;

    public void Update()
    {
        if (occupied && transform.childCount == 0)
        {
            occupied = false;
            occupier = null;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetDroppedItem();
        }
    }

    public void SetOccupied(GameObject go)
    {
        occupied = true;
        occupier = go;
    }

    public DragAndDrop ResetOccupier()
    {
        return transform.GetChild(0).GetComponent<DragAndDrop>();
    }

    public PlayerCard ResetCardOccupier()
    {
        return transform.GetChild(0).GetComponent<PlayerCard>();
    }

    public void GetDroppedItem()
    {
        if (occupier != null)
        {
            objectName = occupier.name;
            SOCharacter c = null;

            switch (objectName)
            {
                case "Ali":
                    c = Resources.Load("Scripts/ScriptableObjects/Ali") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Ezra":
                    c = Resources.Load("Scripts/ScriptableObjects/Ezra") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Francisco":
                    c = Resources.Load("Scripts/ScriptableObjects/Francisco") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Josie":
                    c = Resources.Load("Scripts/ScriptableObjects/Josie") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Kwame":
                    c = Resources.Load("Scripts/ScriptableObjects/Kwame") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Selma":
                    c = Resources.Load("Scripts/ScriptableObjects/Selma") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Soleil":
                    c = Resources.Load("Scripts/ScriptableObjects/Soleil") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
                case "Vanna":
                    c = Resources.Load("Scripts/ScriptableObjects/Vanna") as SOCharacter;
                    BuildTeam.instance.SetCrewmember(c, transform.name);
                    break;
            }
        }
        else
        {
            Debug.Log(transform.name + " is empty");
        }
    }
}
