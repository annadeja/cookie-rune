using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraTestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dispZ = transform.forward * Input.GetAxis("Z");
        Vector3 dispX = transform.right * Input.GetAxis("X");
        Vector3 dispY = transform.up * Input.GetAxis("Y");
        Vector3 disp = dispZ + dispX + dispY;

        transform.Translate(disp);
        transform.Rotate(Vector3.up, 3 * Input.GetAxis("Mouse X"), Space.World);
        transform.Rotate(Vector3.right, -3 * Input.GetAxis("Mouse Y"), Space.World);
    }
}
