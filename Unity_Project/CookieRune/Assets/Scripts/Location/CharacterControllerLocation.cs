using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterControllerLocation : MonoBehaviour
{
    public Camera mainCam;
    CharacterController control;
    [SerializeField] Vector3 disp;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
        disp = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (disp.x > 0)
            {
                animator.SetBool("movingRight", true);
                animator.SetBool("movingLeft", false);
            }
            else if (disp.x < 0)
            {
                animator.SetBool("movingLeft", true);
                animator.SetBool("movingRight", false);
            }
            else
            {
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingRight", false);
            }
        }
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
            Vector3 camDiff = new Vector3(0, 0, -15);
            mainCam.transform.position = transform.position + camDiff;
        }
    }
}
