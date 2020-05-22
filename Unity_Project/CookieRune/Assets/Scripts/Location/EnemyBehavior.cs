using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] Vector3 home;
    [SerializeField] float maxDisplacement;
    [SerializeField] private Vector3 displacement;
    [SerializeField] private float frameDisplacement;
    [SerializeField] private CharacterController control;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        home = transform.position;
        frameDisplacement = 1f;
        displacement = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        displacement.y -= 10 * Time.deltaTime;
        if (control.isGrounded)
            displacement.y = 0;
        if ((transform.position.x - home.x > maxDisplacement) || (transform.position.x - home.x <  -maxDisplacement))
        {
            frameDisplacement = -frameDisplacement;
        }
        displacement.x = frameDisplacement;
        //transform.position += displacement * Time.deltaTime; 
        control.Move(displacement * Time.deltaTime);
    }
}
