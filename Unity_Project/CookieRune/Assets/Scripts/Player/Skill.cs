using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    string name;
    public string Name { get => name; }
    bool isPositive;
    public bool IsPositive { get => isPositive; }
    bool isRanged;
    public bool IsRanged { get => isRanged; }
    bool isMagic;
    public bool IsMagic { get => isMagic; }
    int mpCost;
    public int MpCost { get => mpCost; }
    int nOTargets;
    public int NOTargets { get => nOTargets; }
    float modifier;
    public float Modifier { get => modifier; }
    GameObject hitFX;

    public Skill(string nam, bool pos, bool range, bool magic, int cost, int targets, float mod, string fxName)
    {
        this.name = nam;
        this.isPositive = pos;
        this.isRanged = range;
        this.isMagic = magic;
        this.mpCost = cost;
        this.nOTargets = targets;
        this.modifier = mod;
        hitFX = Resources.Load("hitFX\\" + fxName) as GameObject;
    }

    public void useWithStatOn(int stat, Character target)
    {
        int offset = (int)(stat * this.modifier);
        if (this.isPositive)
        {
            target.heal(offset);
        }
        else
        {
            int dmg;
            if (this.isMagic) dmg = offset - target.mdef;
            else dmg = offset - target.def;
            if (dmg > 0) target.takeDmg(dmg);
        }
    }

    public Skill Copy()
    {
        return new Skill(this.name, this.isPositive, this.isRanged, this.isMagic, this.mpCost, this.nOTargets, this.modifier, hitFX.gameObject.name);
    }

    public bool Compare(Skill toCmp)
    {
        return (this.name.Equals(toCmp.Name)) && (this.IsPositive == toCmp.IsPositive) && (this.isRanged == toCmp.IsRanged) &&
               (this.isMagic == toCmp.IsMagic) && (this.mpCost == toCmp.MpCost) && (this.nOTargets == toCmp.NOTargets) &&
               (this.modifier == toCmp.Modifier);
    }

    public GameObject getHitFX()
    {
        return hitFX;
    }

    public string getShortDesc()
    {
        return "Costs: " + mpCost + " MP.";
    }
}
