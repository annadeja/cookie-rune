using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo : MonoBehaviour
{
    [SerializeField] MovementPath north;
    [SerializeField] MovementPath east;
    [SerializeField] MovementPath south;
    [SerializeField] MovementPath west;
    [SerializeField] bool isStartingNorth;
    [SerializeField] bool isStartingEast;
    [SerializeField] bool isStartingSouth;
    [SerializeField] bool isStartingWest;
    [SerializeField] string locationName;
    [SerializeField] string accLocationName;

    void Start()
    {
        
    }

    // Update is called once per frame
    public MovementPath getNorth()
    {
        if (north != null)
        {
            if (isStartingNorth) { north.resetPath(); }
            else { north.resetPathNeg(); }
        }
        return north;
    }

    public bool hasNorth()
    {
        if (north != null) return true;
        return false;
    }

    public MovementPath getEast()
    {
        if (east != null)
        {
            if (isStartingEast) { east.resetPath(); }
            else { east.resetPathNeg(); }
        }
        return east;
    }

    public bool hasEast()
    {
        if (east != null) return true;
        return false;
    }

    public MovementPath getSouth()
    {
        if (south != null)
        {
            if (isStartingSouth) { south.resetPath(); }
            else { south.resetPathNeg(); }
        }
        return south;
    }

    public bool hasSouth()
    {
        if (south != null) return true;
        return false;
    }

    public MovementPath getWest()
    {
        if (west != null)
        {
            if (isStartingWest) { west.resetPath(); }
            else { west.resetPathNeg(); }
        }
        return west;
    }

    public bool hasWest()
    {
        if (west != null) return true;
        return false;
    }

    public string getLocationName()
    {
        return accLocationName;
    }

    public string getLocation()
    {
        if (locationName != null || locationName != "") return locationName;
        else return null;
    }
}
