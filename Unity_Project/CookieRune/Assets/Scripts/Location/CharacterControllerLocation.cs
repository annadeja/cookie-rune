using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterControllerLocation : MonoBehaviour
{
    public Camera mainCam;
    CharacterController control;
    [SerializeField] Vector3 disp;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        disp = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        disp.x = Input.GetAxis("X") * 7;
        disp.y -= 10 * Time.deltaTime;
        if (control.isGrounded)
        {
            disp.y = 0;
            if (Input.GetButton("Jump"))
            {
                disp.y = 10;
            }
        }

        control.Move(disp * Time.deltaTime);

        if(mainCam != null)
        {
            Vector3 camDiff = new Vector3(0, 10, -20);
            mainCam.transform.position = transform.position + camDiff;
        }
    }
}
