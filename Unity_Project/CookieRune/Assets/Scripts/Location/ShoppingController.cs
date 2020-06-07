using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingController : MonoBehaviour
{
    public Text popup;

    [Header("Shop UI")]
    public Image itemList;
    public Button buyButton;
    public Button sellButton;
    public Text playerCredits;
    public Button nextPage;
    public Button prevPage;

    [Header("Product UI")]
    public Button[] productBtns = new Button[5];
    public Text[] prices = new Text[5];

    [Header("Camera settings")]
    public Camera mainCam;
    [SerializeField] Vector3 cameraLocation;
    private Vector3 initialCameraPosition;

    private InfoCarrier carrier;

    private List<InventoryInfo.ItemInfo> itemsForSale = new List<InventoryInfo.ItemInfo>();
    private List<InventoryInfo.ItemInfo> playersItems = new List<InventoryInfo.ItemInfo>();

    private int page = 0;
    private bool isBuying = true;

    // Start is called before the first frame update
    void Start()
    {
        popup.gameObject.SetActive(false);
        itemList.gameObject.SetActive(false);
        initialCameraPosition = mainCam.transform.position;
        carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
        playersItems = carrier.getInventory();

        itemsForSale.Add(new InventoryInfo.ConsumableInfo("japko", "meme", "You know. ;^)", "", 0, 0));
        itemsForSale.Add(new InventoryInfo.ArmorInfo("Niedzwiedz", 420, 420, "Inside joke", "No.", "", 420));
        itemsForSale.Add(new InventoryInfo.WeaponInfo("Tryptyk", 666, 666, "Inside joke", "No.", "", 666));
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
        showItems();
    }

    private void closeUI()
    {
        if (mainCam != null)
            mainCam.transform.position = initialCameraPosition;

        itemList.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
    }

    public void showItems()
    {
        List<InventoryInfo.ItemInfo> items;
        if (isBuying)
            items = itemsForSale;
        else
            items = playersItems;
        if (page <= 0)
            prevPage.gameObject.SetActive(false);
        else
            prevPage.gameObject.SetActive(true);
        if (page >= (items.Count - 1) / 5)
            nextPage.gameObject.SetActive(false);
        else
            nextPage.gameObject.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            if (page * 5 + i >= items.Count)
            {
                productBtns[i].gameObject.SetActive(false);
                prices[i].gameObject.SetActive(false);
            }
            else
            {
                productBtns[i].gameObject.SetActive(true);
                prices[i].gameObject.SetActive(true);
                if (isBuying && carrier.getPartyCredits() < items[page * 5 + i].Value)
                    productBtns[i].interactable = false;
                else
                    productBtns[i].interactable = true;
                productBtns[i].GetComponentInChildren<Text>().text = items[page * 5 + i].Name;

                if (isBuying)
                    prices[i].text = items[page * 5 + i].Value + " C";
                else
                    prices[i].text = items[page * 5 + i].Value / 2 + " C";
            }
        }
    }

    public void onNextPageBtn()
    {
        page++;
        showItems();
    }

    public void onPrevPageBtn()
    {
        page--;
        showItems();
    }

    public void onBuyBtn()
    {
        isBuying = true;
        showItems();
    }

    public void onSellBtn()
    {
        isBuying = false;
        showItems();
    }

    public void onProductBtn(int i)
    {
        if (isBuying)
        {
            carrier.setPartyCredits(carrier.getPartyCredits() - itemsForSale[page * 5 + i].Value);
            playersItems.Add(itemsForSale[page * 5 + i].copy());
        }
        else
        {
            carrier.setPartyCredits(carrier.getPartyCredits() + playersItems[page * 5 + i].Value / 2);
            playersItems.RemoveAt(page * 5 + i);
            showItems();
        }
    }
}
