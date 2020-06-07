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
    bool isSpecialCast;
    public bool IsSpecialCast { get => isSpecialCast; }
    bool isMagic;
    public bool IsMagic { get => isMagic; }
    int mpCost;
    public int MpCost { get => mpCost; }
    int nOTargets;
    public int NOTargets { get => nOTargets; }
    float modifier;
    public float Modifier { get => modifier; }

    public Skill(string nam, bool pos, bool range, bool special, bool magic, int cost, int targets, float mod)
    {
        this.name = nam;
        this.isPositive = pos;
        this.isRanged = range;
        this.isSpecialCast = special;
        this.isMagic = magic;
        this.mpCost = cost;
        this.nOTargets = targets;
        this.modifier = mod;
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
        return new Skill(this.name, this.isPositive, this.isRanged, this.isSpecialCast, this.isMagic, this.mpCost, this.nOTargets, this.modifier);
    }

    public bool Compare(Skill toCmp)
    {
        return (this.name == toCmp.Name) && (this.IsPositive == toCmp.IsPositive) && (this.isRanged == toCmp.IsRanged) &&
               (this.isMagic == toCmp.IsMagic) && (this.mpCost == toCmp.MpCost) && (this.nOTargets == toCmp.NOTargets) &&
               (this.modifier == toCmp.Modifier);
    }
}
