using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField] private int direction = 1;
    public int curPoint = 0;
    [SerializeField] public Transform[] waypoints;

    public void nextWaypoint()
    {
        if (curPoint >= waypoints.Length - 1)
        {
            direction = -1;
        }
        if (curPoint <= 0)
        {
            direction = 1;
        }
        curPoint += direction;
    }

    public Transform getCurrentWaypoint() { return waypoints[curPoint]; }

    private void OnDrawGizmos()
    {
        if (waypoints != null || waypoints.Length > 2)
        {
            for(int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }

    public void changeDirection()
    {
        direction = -direction;
    }

    public Transform getEnd()
    {
        if (direction == -1) return waypoints[0];
        return waypoints[waypoints.Length - 1];
    }

    public void resetPath()
    {
        curPoint = 0;
        direction = 1;
    }
    public void resetPathNeg()
    {
        curPoint = waypoints.Length - 1;
        direction = -1;
    }
}
