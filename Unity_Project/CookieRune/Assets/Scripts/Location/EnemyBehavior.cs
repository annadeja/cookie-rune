using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float homeX;
    private float homeY;
    [SerializeField] float maxDisplacement;
    private Vector3 displacement;
    private float frameDisplacement;
    private CharacterController control;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        homeX = transform.position.x;
        homeY = transform.position.y;
        frameDisplacement = maxDisplacement / 3.0f;
        displacement = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        displacement.y -= 10 * Time.deltaTime;
        if (control.isGrounded)
            displacement.y = 0;
        if (transform.position.x < homeX + maxDisplacement) 
        {
            displacement.x = frameDisplacement;
        }
        else if (transform.position.x > homeX - maxDisplacement)
        {
            displacement.x = -frameDisplacement;
        }
        //transform.position += displacement * Time.deltaTime; 
        control.Move(displacement * Time.deltaTime);
    }
}
