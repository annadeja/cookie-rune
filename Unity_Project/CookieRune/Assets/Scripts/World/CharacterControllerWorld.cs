using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerWorld : MonoBehaviour
{
    [SerializeField] LocationInfo inLocation = null;
    [SerializeField] PathFollower moveControl;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.SetBool("movingRight", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (inLocation != null)
        {
            if (Input.GetAxis("X") > 0)
            {
                if (animator != null)
                {
                    animator.SetBool("movingLeft", false);
                    animator.SetBool("movingRight", true);
                }
                moveControl.setPath(inLocation.getEast());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("X") < 0)
            {
                if (animator != null)
                {
                    animator.SetBool("movingLeft", true);
                    animator.SetBool("movingRight", false);
                }
                moveControl.setPath(inLocation.getWest());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("Z") > 0)
            {
                if (animator != null)
                {
                    animator.SetBool("movingLeft", false);
                    animator.SetBool("movingRight", true);
                }
                moveControl.setPath(inLocation.getNorth());
                moveControl.startMotion();
                moveControl.initMovement();
            }
            else if (Input.GetAxis("Z") < 0)
            {
                if (animator != null)
                {
                    animator.SetBool("movingLeft", false);
                    animator.SetBool("movingRight", true);
                }
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
            InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
            if (carrier != null)
            {
                carrier.setLastLocation(other.gameObject.name);
            }
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
