using System;
using UnityEngine;
using TMPro;

// handles creating the team and checking for errors

public class BuildTeam : MonoBehaviour
{
    public static Team team;

    public TMP_InputField teamNameText;
    public SOCharacter captain;
    public SOCharacter pilot;
    public SOCharacter mechanic;
    public SOCharacter gunner;

    public DropSlot capSlot;
    public DropSlot pilSlot;
    public DropSlot mechSlot;
    public DropSlot gunSlot;

    public Canvas confirmTeamCanvas;

    private string errorMessage = "Can't build team";

    // instance
    public static BuildTeam instance;
    public void Awake() { instance = this; }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            CreateTeam();
    }

    public void CreateTeam()
    {
        if (teamNameText.text == "" || captain == null || pilot == null ||
            mechanic == null || gunner == null)
        {
            throw new Exception();
        }
        else
        {
            team = new Team(teamNameText.text, captain, pilot, mechanic, gunner);

            confirmTeamCanvas.enabled = true;
        }
    }

    public void SetCrewmember(SOCharacter character, string slotName)
    {
        switch(slotName)
        {
            case "CaptainSlot":
                captain = character;
                break;
            case "MechanicSlot":
                mechanic = character;
                break;
            case "PilotSlot":
                pilot = character;
                break;
            case "GunnerSlot":
                gunner = character;
                break;
        }
    }

    public void OnConfirmTeamButton()
    {
        capSlot.GetDroppedItem();
        pilSlot.GetDroppedItem();
        mechSlot.GetDroppedItem();
        gunSlot.GetDroppedItem();

        try
        {
            CreateTeam();
        }
        catch (Exception ex)
        {
            Debug.Log(errorMessage + ": " + ex.ToString());
            throw;
        }
    }

    public void OnReSelectTeamButton()
    {
        confirmTeamCanvas.enabled = false;
    }
}