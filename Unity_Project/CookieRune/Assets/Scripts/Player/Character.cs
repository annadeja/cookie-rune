using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : System.IComparable
{
    public string unitName;
    public int level;
    public int curHP;
    public int CurHP { get => curHP; }
    public int maxHP;

    public int atk;
    public int def;

    public int mag;
    public int mdef;

    public int spd;

    public int curMP;
    public int maxMP;

    int curXP;
    public int CurXP { get => curXP; }
    int nxtXP;
    int lp;
    public int Lp { get => lp; }

    List<Skill> skills;

    GameObject body;

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
        curXP = 0;
        nxtXP = 100;
        lp = 10;
        skills = new List<Skill>();
        skills.Add(new Skill("Attack", false, false, false, false, 0, 1, 1f));
        body = Resources.Load("CharacterBodies\\" + unitName + "Body") as GameObject;
    }

    public Character(string nam, int lvl, int mHP, int mMP, int atk, int def, int mag, int mdef, int spd, int curXP)
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
        this.curXP = curXP;
        nxtXP = 100;
        lp = 10;
        skills = new List<Skill>();
        skills.Add(new Skill("Attack", false, false, false, false, 0, 1, 1f));
    }

    public void takeDmg(int dmg)
    {
        if (dmg > 0) curHP -= dmg;

        if (curHP < 0) curHP = 0;
    }

    public void heal(int hp)
    {
        if (hp > 0) curHP += hp;

        if (curHP > maxHP) curHP = maxHP;
    }

    public void restoreMP(int mp)
    {
        if (mp > 0) curMP += mp;

        if (curMP > maxMP) curMP = maxMP;
    }

    public int calcNextXP()
    {
        return level * 100;
    }

    public bool checkLevelUp()
    {
        bool ret = false;
        if (curXP >= nxtXP)
        {
            level++;
            curXP -= nxtXP;
            lp += 10;
            nxtXP = calcNextXP();
            ret = true;
        }
        if (curXP >= nxtXP) checkLevelUp();
        return ret;
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

    public bool hasSkill(Skill template)
    {
        bool ret = false;
        foreach (Skill skill in skills)
        {
            ret = skill.Compare(template);
        }
        return ret;
    }

    public bool addXP(int value)
    {
        curXP += value;
        return checkLevelUp();
    }

    public void setNewStats(int atk, int def, int mag, int mdef, int spd, int mHP, int mMP, int lp)
    {
        this.atk = atk;
        this.def = def;
        this.mag = mag;
        this.mdef = mdef;
        this.spd = spd;
        this.maxHP = mHP;
        this.maxMP = mMP;
        this.lp = lp;
    }

    public GameObject getBody()
    {
        return body;
    }
}