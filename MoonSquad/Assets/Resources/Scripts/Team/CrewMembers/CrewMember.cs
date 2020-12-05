using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrewMember : MonoBehaviour, ICrewmember
{
    public SOCharacter character;

    public int speedStat;
    public int intelligenceStat;
    public int dexterityStat;
    public int luckStat;

    public Sprite profilePic;

    public void setStats(SOCharacter c)
    {
        character = c;
        speedStat = c.speed;
        intelligenceStat = c.intelligence;
        dexterityStat = c.dexterity;
        luckStat = c.luck;
        profilePic = c.characterSprite;
    }

    public void SetCharacter(SOCharacter c)
    {
        setStats(c);

        transform.Find("ProfilePic").GetComponent<SpriteRenderer>().sprite = profilePic;
    }

    public void Boost()
    {

    }
}

public interface ICrewmember
{
    // captain, pilot, mechanic, and gunner derive from this interface
    // 
    void Boost();
}