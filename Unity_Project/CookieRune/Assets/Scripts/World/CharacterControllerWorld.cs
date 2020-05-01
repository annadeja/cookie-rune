﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerWorld : MonoBehaviour
{
    [SerializeField] LocationInfo inLocation = null;
    [SerializeField] PathFollower moveControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inLocation != null)
        {
            if (Input.GetAxis("X") > 0)
            {
                moveControl.setPath(inLocation.getEast());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("X") < 0)
            {
                moveControl.setPath(inLocation.getWest());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("Z") > 0)
            {
                moveControl.setPath(inLocation.getNorth());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("Z") < 0)
            {
                moveControl.setPath(inLocation.getSouth());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                string name = inLocation.getLocation();
                if (name != null)
                {
                    SceneManager.LoadScene(name);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Location")
        {
            Debug.Log("Now in location: " + other.gameObject.name);
            inLocation = other.gameObject.GetComponent<LocationInfo>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Location")
        {
            Debug.Log("Leaving location: " + other.gameObject.name);
            inLocation = null;
        }
    }
}
