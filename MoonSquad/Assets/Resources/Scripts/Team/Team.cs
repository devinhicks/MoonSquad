using System.IO;
using UnityEngine;
using UnityEditor;

public class Team
{
    public string teamName;

    public SOCharacter captain;
    public SOCharacter pilot;
    public SOCharacter mechanic;
    public SOCharacter gunner;

    // team stats
    public int teamSpeed;
    public int teamIntelligence;
    public int teamLuck;
    public int teamDexterity;

    // instance
    public static Team instance;

    // sets the team stats based on the characters chosen
    private void SetTeamStats()
    {
        // pilot's speed boosts team speed
        teamSpeed = captain.speed + (pilot.speed * 2) + mechanic.speed
            + gunner.speed;
        // captain's intelligence boosts team intelligence
        teamIntelligence = (captain.intelligence * 2) + pilot.intelligence
            + mechanic.intelligence + gunner.intelligence;
        // gunner's luck boosts team luck
        teamLuck = captain.luck + pilot.luck + mechanic.luck
            + (gunner.luck * 2);
        // mechanic's dexterity boosts team's dexterity
        teamDexterity = captain.dexterity + pilot.dexterity
            + (mechanic.dexterity * 2) + gunner.dexterity;
    }

    // constructor sets team name and sets characters to their positions
    // it also calls setTeamStats() to set stats based on chosen characters
    public Team(string name, SOCharacter c, SOCharacter p, SOCharacter m, SOCharacter g)
    {
        teamName = name;

        captain = c;
        pilot = p;
        mechanic = m;
        gunner = g;

        SetTeamStats();

        instance = this;

        // can be called to create a file to show the team stats
        //SaveTeam();
        //ReadTeam();
    }

    // destructor is called when Team object is destroyed and sends message to Console
    ~Team()
    {
        Debug.Log("Team " + teamName + " has been destroyed.");
    }

    public static Team operator +(Team a, Team b)
    {
        // if for some reason you needed to add two teams together
        string name = a.teamName + " " + b.teamName;
        Team t = new Team(name, a.captain, b.pilot, a.mechanic, b.gunner);
        return t;
    }


    // courtesy of Daniel Robledo from Unity Support
    // https://support.unity.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-

    public void SaveTeam()
    {
        string path = "Assets/Resources/teamFile.txt";

        // saves current team to a data file
        if (File.Exists(path))
        {
            return;
        }

        StreamWriter file = File.CreateText(path);
        // write to teamFile.txt
        //StreamWriter writer = new StreamWriter(file, true);
        file.WriteLine("Team " + teamName + " -\nCaptain: " +
            captain + "\nPilot: " + pilot + "\nMechanic: " + mechanic +
            "\nGunner: " + gunner + "\n\nTeam Stats:\nIntelligence: " +
            teamIntelligence + "\nSpeed: " + teamSpeed + "\nDexterity: " +
            teamDexterity + "\nLuck: " + teamLuck);
        file.Close();

        // re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load(path) as TextAsset;

        // print to console
        Debug.Log(asset.text);
    }

    public void ReadTeam()
    {
        string path = "Assets/Resources/teamFile.txt";

        // read text from file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
