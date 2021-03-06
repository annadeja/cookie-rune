﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] MovementPath path;
    private Transform moveToWaypoint;
    [SerializeField] private bool inMotion = false;
    Vector3 moveToVec;
    // Start is called before the first frame update
    void Start()
    {
        initMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null)
        {
            if (inMotion)
            {
                float debug = (transform.position - moveToWaypoint.position).magnitude;
                if (debug <= 0.51f)
                {
                    inMotion = !isAtEnd();
                    path.nextWaypoint();
                    moveToWaypoint = path.getCurrentWaypoint();
                    moveToVec = (moveToWaypoint.position - transform.position);
                }
                transform.Translate(moveToVec.normalized / 2);
            }
        }
    }

    bool isAtEnd()
    {
        return (path.getEnd().position - transform.position).magnitude <= 0.51f;
    }

    public void initMovement()
    {
        if (path != null)
        {
            if (path.waypoints != null)
            {
                moveToWaypoint = path.getCurrentWaypoint();
                transform.position = moveToWaypoint.position;
                path.nextWaypoint();
                moveToWaypoint = path.getCurrentWaypoint();
                moveToVec = (moveToWaypoint.position - transform.position);
            }
        }
    }

    public void setPath(MovementPath pth)
    {
        path = pth;
    }

    public void startMotion()
    {
        inMotion = true;
    }

    public bool IsInMotion()
    {
        return inMotion;
    }
}
