using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ship : MonoBehaviour, IDamageable
{
    public Hull hull;
    public Engine engine;
    public Weapons weapons;

    public List<ShipPart> parts;

    public Slider hullSlider;
    public Slider engineSlider;
    public Slider weaponsSlider;

    public TextMeshProUGUI hullText;
    public TextMeshProUGUI engineText;
    public TextMeshProUGUI weaponsText;

    void Start()
    {
        InitializeShip();
    }

    public void InitializeShip()
    {
        hull = new Hull();
        engine = new Engine();
        weapons = new Weapons();

        // set health of all parts to max health
        hull.currentHealth = hull.maxHealth;
        engine.currentHealth = engine.maxHealth;
        weapons.currentHealth = weapons.maxHealth;

        hull.healthText = hullText;
        engine.healthText = engineText;
        weapons.healthText = weaponsText;

        hullSlider.maxValue = hull.maxHealth;
        engineSlider.maxValue = engine.maxHealth;
        weaponsSlider.maxValue = weapons.maxHealth;

        hullSlider.value = hull.currentHealth;
        engineSlider.value = engine.currentHealth;
        weaponsSlider.value = weapons.currentHealth;

        hullText.text = "100/100";
        engineText.text = "100/100";
        weaponsText.text = "100/100";

        // create list of ship parts and add hull, engine, and weapons
        // will make it easier to adjust health of all parts in a loop
        parts = new List<ShipPart>();
        parts.Add(hull);
        parts.Add(engine);
        parts.Add(weapons);
    }

    public void Damage(int amount)
    {
        amount /= 3;

        hull.currentHealth -= amount;
        engine.currentHealth -= amount;
        weapons.currentHealth -= amount;

        // update UI
        hullText.text = hull.currentHealth + "/100";
        engineText.text = engine.currentHealth + "/100";
        weaponsText.text = weapons.currentHealth + "/100";
    }

    public void Repair(int amount)
    {
        amount /= 3;

        // update UI
        hull.currentHealth += amount;
        engine.currentHealth += amount;
        weapons.currentHealth += amount;

        hullText.text = hull.currentHealth + "/100";
        engineText.text = engine.currentHealth + "/100";
        weaponsText.text = weapons.currentHealth + "/100";
    }
}

public abstract class ShipPart : IDamageable
{
    public int maxHealth = 500;
    public int currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public void Damage(int amount)
    {
        currentHealth -= amount;
        healthText.text = currentHealth + "/100";
    }

    public void Repair(int amount)
    {
        currentHealth += amount;
        healthText.text = currentHealth + "/100";
    }
}

public class Hull : ShipPart { }
public class Engine : ShipPart { }
public class Weapons : ShipPart { }