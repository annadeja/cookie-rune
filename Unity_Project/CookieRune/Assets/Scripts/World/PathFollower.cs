using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] MovementPath path;
    private Transform moveToWaypoint;
    Vector3 moveToVec;

    // Start is called before the first frame update
    void Start()
    {
        if (path.waypoints != null)
        {
            moveToWaypoint = path.getCurrentWaypoint();
            transform.position = moveToWaypoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - moveToWaypoint.position).magnitude < 0.2f)
        {
            Debug.Log("Tu jestem!");
            path.nextWaypoint();
            moveToWaypoint = path.getCurrentWaypoint();
            moveToVec = (moveToWaypoint.position - transform.position);
        }

        transform.Translate(moveToVec.normalized * moveToVec.magnitude * Time.deltaTime);
    }
}
