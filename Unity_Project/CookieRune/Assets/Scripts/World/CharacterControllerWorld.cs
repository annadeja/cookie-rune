using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControllerWorld : MonoBehaviour
{
    [SerializeField] LocationInfo inLocation = null;
    [SerializeField] PathFollower moveControl;
    public Text locationText;
    public Image locationPanel;
    private Animator animator;

    public InventoryController inv;
    bool isInInventory = false;

    [SerializeField] Canvas dirArrows;
    [SerializeField] Image upArrow;
    [SerializeField] Image rightArrow;
    [SerializeField] Image downArrow;
    [SerializeField] Image leftArrow;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.SetBool("movingRight", true);
        upArrow.gameObject.SetActive(true);
        rightArrow.gameObject.SetActive(true);
        downArrow.gameObject.SetActive(false);
        leftArrow.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInInventory)
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
                else if (Input.GetButtonDown("Submit"))
                {
                    string name = inLocation.getLocation();
                    if (name != null)
                    {
                        SceneManager.LoadScene(name);
                    }
                }
                if (moveControl.IsInMotion())
                {
                    upArrow.gameObject.SetActive(false);
                    rightArrow.gameObject.SetActive(false);
                    downArrow.gameObject.SetActive(false);
                    leftArrow.gameObject.SetActive(false);
                }
                else
                {
                    if (inLocation.hasNorth()) upArrow.gameObject.SetActive(true);
                    if (inLocation.hasEast()) rightArrow.gameObject.SetActive(true);
                    if (inLocation.hasSouth()) downArrow.gameObject.SetActive(true);
                    if (inLocation.hasWest()) leftArrow.gameObject.SetActive(true);
                }
            }
        }
        if (Input.GetButtonDown("Inventory")) inv.toggleInventory();
        dirArrows.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Location")
        {
            inLocation = other.gameObject.GetComponent<LocationInfo>();
            InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
            if (carrier != null)
            {
                carrier.setLastLocation(other.gameObject.name);
            }
            locationPanel.gameObject.SetActive(true);
            locationText.text = "Currently in location: " + inLocation.getLocationName() + "\n(press \'E\' to enter)";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Location")
        {
            inLocation = null;
            locationPanel.gameObject.SetActive(false);
        }
    }
}
