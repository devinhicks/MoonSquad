using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Character", menuName = "Create Character")]
public class SOCharacter : ScriptableObject
{
    public string characterName;
    public string tagLine;
    public string animal;
    // potentially pull bio description from file

    public int speed;
    public int intelligence;
    public int dexterity;
    public int luck;

    public Sprite characterSprite;

}
