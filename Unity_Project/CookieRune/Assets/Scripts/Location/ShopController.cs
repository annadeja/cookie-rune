using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text popup;
    public Image itemList;
    public Button buyButton;
    public Button sellButton;
    public Camera mainCam;
    [SerializeField] Vector3 cameraLocation;
    private Vector3 initialCameraPosition;
    public Text playerCredits;
    private InfoCarrier carrier;

    // Start is called before the first frame update
    void Start()
    {
        popup.gameObject.SetActive(false);
        itemList.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        initialCameraPosition = mainCam.transform.position;
        carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        popup.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Submit"))
        {
            CharacterControllerLocation charCon = other.GetComponent<CharacterControllerLocation>();
            charCon.canMove = false;
            initializeUI();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            CharacterControllerLocation charCon = other.GetComponent<CharacterControllerLocation>();
            charCon.canMove = true;
            closeUI();
        }

        playerCredits.text = "Credits: " + carrier.getPartyCredits();
    }

    private void OnTriggerExit(Collider other)
    {
        popup.gameObject.SetActive(false);
    }

    private void initializeUI()
    {
        if (mainCam != null)
            mainCam.transform.position = cameraLocation;

        popup.gameObject.SetActive(false);
        itemList.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
    }

    private void closeUI()
    {
        if (mainCam != null)
            mainCam.transform.position = initialCameraPosition;

        itemList.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
    }
}
