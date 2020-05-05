using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int level;
    public int curHP;
    public int maxHP;

    public int atk;
    public int def;

    public bool takeDmg(int dmg)
    {
        int acDMG = dmg - def;
        if (acDMG > 0) curHP -= acDMG;

        if (curHP <= 0) return true;
        return false;
    }
}
