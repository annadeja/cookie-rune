﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryInfo;

public class InfoCarrier : MonoBehaviour
{
    [Header("Location Info")]
    [SerializeField] string lastLocation;
    [Header("Party")]
    List<Character> party;
    List<InventoryInfo.ItemInfo> inventory = new List<ItemInfo>();
    int credits = 0;
    List<Character> enemyParty;
    List<System.Tuple<string, bool>> pastEncounters = new List<System.Tuple<string, bool>>();

    float volume = 1.0f;
    public float Volume { get => volume; }

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        createNewParty();
        InventoryInfo.ItemInfo item = new InventoryInfo.ConsumableInfo("Dorito", "Healing", "A dorito. ONLY ONE DORITO. Tastes like a conspiracy.", "", 69, 69) as InventoryInfo.ItemInfo;
        addToInventory(item);
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug Validate")) debugParty();
    }

    public void addToInventory(ItemInfo item)
    {
        inventory.Add(item);
    }

    public void setLastLocation(string name)
    {
        lastLocation = name;
    }

    public string getLastLocation()
    {
        return lastLocation;
    }

    public void createNewParty()
    {
        party = new List<Character>();
        party.Add(new Character("P. Butter", 1, 15, 20, 5, 1, 1, 1, 4));
        party.Add(new Character("Meringue", 1, 30, 20, 3, 3, 3, 1, 2));
        party.Add(new Character("Saffron", 1, 20, 100, 4, 1, 4, 3, 3));
        party.Capacity = 3;
    }

    public Character getCharacter(int i)
    {
        return party[i];
    }

    public void setEnemyParty(List<Character> enemyParty)
    {
        this.enemyParty = enemyParty;
    }

    public Character getEnemy(int i)
    {
        return enemyParty[i];
    }

    public List<Character> getPlayerParty()
    {
        return party;
    }

    public int getPartyCredits()
    {
        return credits;
    }

    public void setPartyCredits(int credits)
    {
        this.credits = credits;
    }

    public List<Character> getEnemyParty()
    {
        return enemyParty;
    }

    public List<InventoryInfo.ItemInfo> getInventory()
    {
        return inventory;
    }

    public void addEncounter(string name)
    {
        pastEncounters.Add(System.Tuple.Create(name, false));
    }

    public void setLastEncounterVictory(bool outcome)
    {
        if (pastEncounters != null)
            pastEncounters[pastEncounters.Count - 1] = System.Tuple.Create(pastEncounters[pastEncounters.Count - 1].Item1, outcome);
    }

    public List<System.Tuple<string, bool>> getPastEncounters()
    {
        return pastEncounters;
    }

    public void clearEncounters()
    {
        pastEncounters = new List<System.Tuple<string, bool>>();
    }

    public void setVolume(System.Single value)
    {
        if (value > 1.0f) volume = 1.0f;
        else if (value < 0.0f) volume = 0.0f;
        else volume = value;

    }

    private void debugParty()
    {
        foreach (Character chara in party)
        {
            if (chara != null)
            {
                Debug.Log(chara.CurAtk);
                Debug.Log(chara.CurDef);
            }
        }
    }
}
