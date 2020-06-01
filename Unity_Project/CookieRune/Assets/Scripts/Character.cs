using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : System.IComparable
{
    public string unitName;
    public int level;
    public int curHP;
    public int maxHP;

    public int atk;
    public int def;

    public int mag;
    public int mdef;

    public int spd;

    public int curMP;
    public int maxMP;

    List<Skill> skills;

    public Character()
    {
        unitName = "Ben";
        level = 1;
        maxHP = 60;
        curHP = maxHP;
        maxMP = 20;
        curMP = maxMP;
        atk = 5;
        def = 1;
        spd = 5;
        skills = new List<Skill>();
        skills.Add(new Skill("Attack", false, false, false, false, 0, 1, 1f));
    }

    public Character(string nam, int lvl, int mHP, int mMP, int atk, int def, int mag, int mdef, int spd)
    {
        unitName = nam;
        level = lvl;
        maxHP = mHP;
        curHP = maxHP;
        maxMP = mMP;
        curMP = maxMP;
        this.atk = atk;
        this.def = def;
        this.mag = mag;
        this.mdef = mdef;
        this.spd = spd;
        skills = new List<Skill>();
        skills.Add(new Skill("Attack", false, false, false, false, 0, 1, 1f));
    }

    public bool takeDmg(int dmg)
    {
        if (dmg > 0) curHP -= dmg;

        if (curHP <= 0) return true;
        return false;
    }

    public Skill getSkill(int i)
    {
        if (skills[i] != null) return skills[i];
        else return null;
    }

    public int CompareTo(object obj)
    {
        Character other = obj as Character;
        if (other != null)
        {
            return other.spd - this.spd;
        }
        else return -1;
    }

    public void addSkill(Skill template)
    {
        skills.Add(template.Copy());
    }

    public int getSkillCount()
    {
        return skills.Count - 1;
    }

}