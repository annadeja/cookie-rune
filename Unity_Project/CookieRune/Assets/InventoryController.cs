using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryController : MonoBehaviour
{
    public GameObject panel;
    InfoCarrier carrier;
    [Header("HP")]
    public Text[] hpText = new Text[3];
    public Slider[] hpSlider = new Slider[3];

    [Header("MP")]
    public Text[] mpText = new Text[3];
    public Slider[] mpSlider = new Slider[3];

    [Header("XP")]
    public Text[] xpText = new Text[3];
    public Slider[] xpSlider = new Slider[3];

    [Header("Aim Buttons")]
    public Button[] aimBtn = new Button[3];

    [Header("Item Buttons")]
    public Button[] itemBtn = new Button[3];
    public Button nextPageBtn;
    public Button prevPageBtn;

    [Header("Game Control UI")]
    public Button exit;

    int page = 0;
    bool isAiming = false;

    InventoryInfo.ItemInfo toUse = null;

    // Start is called before the first frame update
    void Start()
    {
        carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleInventory()
    {
        if (panel.activeSelf) panel.SetActive(false);
        else openPanel();
    }

    void openPanel()
    {
        panel.SetActive(true);
        updateBars();
        showItems();
        deactivateAim();
    }

    void updateBars()
    {
        for (int i =0; i< 3; i++)
        {
            Character curChara = carrier.getCharacter(i);
            hpSlider[i].maxValue = curChara.maxHP;
            hpSlider[i].value = curChara.CurHP;
            hpText[i].text = "HP: " + curChara.CurHP + "/" + curChara.maxHP;
            mpSlider[i].maxValue = curChara.maxMP;
            mpSlider[i].value = curChara.curMP;
            mpText[i].text = "MP: " + curChara.curMP + "/" + curChara.maxMP;
            xpSlider[i].maxValue = curChara.MaxXP;
            xpSlider[i].value = curChara.CurXP;
            xpText[i].text = "XP: " + curChara.CurXP + "/" + curChara.MaxXP;
        }
    }

    void showItems()
    {
        var playerInventory = carrier.getInventory();
        if (page > 0)
            prevPageBtn.gameObject.SetActive(true);
        else prevPageBtn.gameObject.SetActive(false);
        if (page < (int)(System.Math.Ceiling(playerInventory.Count / 3.0) - 1))
            nextPageBtn.gameObject.SetActive(true);
        else nextPageBtn.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            if (page * 3 + i < playerInventory.Count)
            {
                InventoryInfo.ItemInfo toDisplay = playerInventory[page * 3 + i];
                itemBtn[i].gameObject.SetActive(true);
                if (playerInventory[page * 3 + i].Type == "Offensive")
                    itemBtn[i].interactable = false;
                else
                    itemBtn[i].interactable = true;
                itemBtn[i].GetComponentInChildren<Text>().text = toDisplay.Name;
            }
            else itemBtn[i].gameObject.SetActive(false);
        }
    }

    void activateAim(int btn)
    {
        nextPageBtn.interactable = false;
        prevPageBtn.interactable = false;
        exit.interactable = false;
        for (int i = 0; i < 3; i++)
        {
            if (i == btn) itemBtn[i].interactable = false;
            else itemBtn[i].interactable = true;
            aimBtn[i].interactable = true;
        }
        isAiming = true;
    }

    void deactivateAim()
    {
        nextPageBtn.interactable = true;
        prevPageBtn.interactable = true;
        exit.interactable = true;
        for (int i = 0; i < 3; i++)
        {
            itemBtn[i].interactable = true;
            aimBtn[i].interactable = false;
        }
        isAiming = false;
    }

    public void OnItemBtn(int i)
    {
        if (isAiming) deactivateAim();
        else
        {
            activateAim(i);
            toUse = carrier.getInventory()[page * 3 + i];
        }
    }

    public void OnAimBtn(int i)
    {
        toUse.takeEffect(carrier.getCharacter(i));
        carrier.getInventory().Remove(toUse);
        toUse = null;
        deactivateAim();
        showItems();
        updateBars();
    }

    public void OnNextPageBtn()
    {
        page++;
        showItems();
    }

    public void OnPrevPageBtn()
    {
        page--;
        showItems();
    }

    public void OnExitBtn()
    {
        Destroy(carrier.gameObject);
        carrier = null;
        SceneManager.LoadScene("MainMenu_Scene");
    }
}
