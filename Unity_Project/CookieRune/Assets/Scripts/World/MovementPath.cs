using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public int direction = 1;
    public int curPoint = 0;
    [SerializeField] public Transform[] waypoints;

    public void nextWaypoint()
    {
        if (curPoint >= waypoints.Length - 1 || curPoint <= 0)
        {
            direction = -direction;
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
}
