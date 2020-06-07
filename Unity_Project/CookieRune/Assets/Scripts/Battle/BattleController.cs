﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Skrypt napisany przez: Artur Marbach

public enum BattleState { START, PTURN, ETURN, WON, LOST, INACTION, RUN, AIM }

public class BattleController : MonoBehaviour
{  
    public Text dialogue;

    [Header("Player UI")]
    public Text playerNametext1;
    public Text playerHPtext1;
    public Slider pHPslider1;
    public Text playerMPtext1;
    public Slider pMPslider1;
    public Text playerNametext2;
    public Text playerHPtext2;
    public Slider pHPslider2;
    public Text playerMPtext2;
    public Slider pMPslider2;
    public Text playerNametext3;
    public Text playerHPtext3;
    public Slider pHPslider3;
    public Text playerMPtext3;
    public Slider pMPslider3;
    public Button atkBtn;
    public Button skillBtn;
    public Button runBtn;
    public Button itemsBtn;
    public Button[] pAimBtns;

    [Header("Skills UI")]
    public Button[] aSkill;
    public Button nextPageSkills;
    public Button prevPageSkills;
    public Button back;

    [Header("Enemy UI")]
    public Text[] enemyName;
    public Text[] enemyHPText;
    public Slider[] eHPslider;
    public Button[] eAimBtns;

    [Header("Battle State")]
    public BattleState state;

    [Header("Battle Stations")]
    public Transform[] pBattleStation = new Transform[3];
    public Transform[] eBattleStation = new Transform[3];

    List<Character> playerParty;
    List<InventoryInfo.ItemInfo> playerInventory;
    List<Character> enemyParty;

    List<GameObject> bodies = new List<GameObject>();

    // turn control
    int turnIterator = 0;
    List<Character> allCharas = new List<Character>();
    // skill execution and aim
    Skill toExec;
    List<int> targetList;
    // skill choosing control
    int skillPage = 0;

    InventoryInfo.ItemInfo toUse;
    bool isItems = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(setupBattle());
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug Validate")) debugFunc();
    }

    IEnumerator setupBattle()
    {
        //GameObject pInScene = Instantiate(player, pBattleStation);
        //GameObject eInScene = Instantiate(enemy, eBattleStation);

        InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();

        playerInventory = carrier.getInventory();
        playerParty = carrier.getPlayerParty();
        enemyParty = carrier.getEnemyParty();
        foreach (Character chara in playerParty)
        {
            allCharas.Add(chara);
        }
        foreach (Character chara in enemyParty)
        {
            allCharas.Add(chara);
        }
        allCharas.Capacity = playerParty.Capacity + enemyParty.Capacity;
        allCharas.Sort();
        turnIterator = 0;

        for (int i=0;i<playerParty.Count;i++)
        {
            bodies.Add(Instantiate(playerParty[i].getBody(), pBattleStation[i]));
        }

        for (int i = 0; i < 3; i++)
        {
            if (i < enemyParty.Capacity)
            {
                enemyName[i].text = enemyParty[i].unitName;
                eHPslider[i].maxValue = enemyParty[i].maxHP;
            }
            else
            {
                enemyName[i].gameObject.SetActive(false);
                enemyHPText[i].gameObject.SetActive(false);
                eHPslider[i].gameObject.SetActive(false);
            }
            eAimBtns[i].gameObject.SetActive(false);
        }

        playerNametext1.text = playerParty[0].unitName;
        playerNametext2.text = playerParty[1].unitName;
        playerNametext3.text = playerParty[2].unitName;

        pHPslider1.maxValue = playerParty[0].maxHP;
        pHPslider2.maxValue = playerParty[1].maxHP;
        pHPslider3.maxValue = playerParty[2].maxHP;

        pMPslider1.maxValue = playerParty[0].maxMP;
        pMPslider2.maxValue = playerParty[1].maxMP;
        pMPslider3.maxValue = playerParty[2].maxMP;

        updateHP();
        updateMP();

        hideAimPlayerBtns();
        hideAimEnemyBtns();
        hidePlayerBaseOptions();
        hideSkills();
        back.gameObject.SetActive(false);

        dialogue.text = "Battle starts!";

        yield return new WaitForSeconds(2f);

        setupTurn();
    }

    void updateHP()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < enemyParty.Capacity)
            {
                eHPslider[i].value = enemyParty[i].curHP;
                enemyHPText[i].text = enemyParty[i].curHP + "/" + enemyParty[i].maxHP;
            }
        }

        pHPslider1.value = playerParty[0].curHP;
        pHPslider2.value = playerParty[1].curHP;
        pHPslider3.value = playerParty[2].curHP;
        
        playerHPtext1.text = "HP: " + playerParty[0].curHP + "/" + playerParty[0].maxHP;
        playerHPtext2.text = "HP: " + playerParty[1].curHP + "/" + playerParty[1].maxHP;
        playerHPtext3.text = "HP: " + playerParty[2].curHP + "/" + playerParty[2].maxHP;
    }

    void updateMP()
    {
        pMPslider1.value = playerParty[0].curMP;
        pMPslider2.value = playerParty[1].curMP;
        pMPslider3.value = playerParty[2].curMP;

        playerMPtext1.text = "MP: " + playerParty[0].curMP + "/" + playerParty[0].maxMP;
        playerMPtext2.text = "MP: " + playerParty[1].curMP + "/" + playerParty[1].maxMP;
        playerMPtext3.text = "MP: " + playerParty[2].curMP + "/" + playerParty[2].maxMP;
    }

//----------------PLAYER ACTIONS--------------------------

    IEnumerator playerUseSkill()
    {
        allCharas[turnIterator].curMP -= toExec.MpCost;
        updateMP();
        for (int i = 0; i < targetList.Count; i++)
        {
            //init phase
            dialogue.text = "init skill: " + toExec.Name;
            yield return new WaitForSeconds(1f);

            //mid phase
            dialogue.text = "mid skill: " + toExec.Name;
            yield return new WaitForSeconds(1f);

            //end phase
            dialogue.text = "end skill: " + toExec.Name;
            int stat = allCharas[turnIterator].atk;
            if (toExec.IsMagic) stat = allCharas[turnIterator].mag;
            if (toExec.IsPositive) toExec.useWithStatOn(stat, playerParty[targetList[i]]);
            else toExec.useWithStatOn(stat, enemyParty[targetList[i]]);
            updateHP();
            yield return new WaitForSeconds(1f);
        }

        dialogue.text = "post: " + toExec.Name;
        toExec = null;
        yield return new WaitForSeconds(1f);

        nextChara();
        setupTurn();
    }

    IEnumerator playerUseItem()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            //init phase
            dialogue.text = "init item: " + toUse.Name;
            yield return new WaitForSeconds(1f);

            //mid phase
            dialogue.text = "mid item: " + toUse.Name;
            yield return new WaitForSeconds(1f);

            //end phase
            dialogue.text = "end item: " + toUse.Name;
            //Debug.Log(playerParty[targetList[i]].CurHP);
            if (toUse.Type == "Offensive")
                toUse.takeEffect(enemyParty[targetList[i]]);
            else
                toUse.takeEffect(playerParty[targetList[i]]);
            updateHP();
            updateMP();
            //Debug.Log(playerParty[targetList[i]].CurHP);
            yield return new WaitForSeconds(1f);
        }

        dialogue.text = "post: " + toUse.Name;
        playerInventory.Remove(toUse);
        toUse = null;
        yield return new WaitForSeconds(1f);

        nextChara();
        setupTurn();
    }

    IEnumerator playerFlee()
    {
        state = BattleState.INACTION;

        dialogue.text = "You try to flee!";

        yield return new WaitForSeconds(1f);

        float chance = Random.Range(0f, 100f);

        if (chance > 50f)
        {
            dialogue.text = "You managed to flee!";
            yield return new WaitForSeconds(2f);
            state = BattleState.RUN;
            StartCoroutine(EndBattle());
        }
        else
        {
            dialogue.text = "You failed!";
            yield return new WaitForSeconds(1f);
            nextChara();
            setupTurn();
        }
    }
//-----------------------END PLAYER ACTIONS--------------------------

    void setupTurn()
    {
        dialogue.text = "It's " + allCharas[turnIterator].unitName + "'s turn.";
        if (isEnemyPartyDead())
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else if (isPlayerPartyDead())
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else if (enemyParty.Contains(allCharas[turnIterator]))
        {
            state = BattleState.ETURN;
            StartCoroutine(EnemyTurn());
        }
        else if (playerParty.Contains(allCharas[turnIterator]))
        {
            playerTurn();
        }
    }

    void playerTurn()
    {
        if (allCharas[turnIterator].curHP <= 0)
        {
            nextChara();
            setupTurn();
        }
        else
        {
            state = BattleState.PTURN;
            showPlayerBaseOptions();
        }
    }

    public void onAimButton(int i)
    {
        if (state != BattleState.AIM) return;

        targetList.Add(i);
        if (isItems)
        {
            state = BattleState.INACTION;
            hideAimPlayerBtns();
            hideAimEnemyBtns();
            StartCoroutine(playerUseItem());
        }
        else
        {
            if (targetList.Count >= toExec.NOTargets)
            {
                state = BattleState.INACTION;
                hideAimPlayerBtns();
                hideAimEnemyBtns();
                StartCoroutine(playerUseSkill());
            }
        }
    }

    public void onSkillBtn(int i)
    {
        if (state != BattleState.PTURN) return;

        toExec = allCharas[turnIterator].getSkill(i);
        targetList = new List<int>(0);
        if (toExec.IsPositive)
            showAimPlayerBtns();
        else
            showAimEnemyBtns();
        hidePlayerBaseOptions();
        hideSkills();
        state = BattleState.AIM;

        dialogue.text = "Aim now.";
    }

    public void onItemBtn(int i)
    {
        if (state != BattleState.PTURN) return;

        toUse = playerInventory[i];
        targetList = new List<int>(0);
        if (toUse.Type == "Offensive")
            showAimEnemyBtns();
        else
            showAimPlayerBtns();
        hidePlayerBaseOptions();
        hideSkills();
        state = BattleState.AIM;

        dialogue.text = "Aim now.";
    }

    public void OnAttackBtn()
    {
        isItems = false;
        onSkillBtn(0);
    }

    public void OnSkillBtnPaged(int i)
    {
        if (isItems)
        {
            onItemBtn(i - 1);
        }
        else
        {
            onSkillBtn(skillPage * 3 + i);
        }
        back.gameObject.SetActive(false);
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PTURN)
            return;

        StartCoroutine(playerFlee());
    }

    public void OnSkillsButton()
    {
        if (state != BattleState.PTURN)
            return;

        showSkills();
    }

    public void OnItemsButton()
    {
        if (state != BattleState.PTURN) return;

        showItems();
    }

    IEnumerator EnemyTurn()
    {
        if (allCharas[turnIterator].curHP > 0)
        {
            yield return new WaitForSeconds(1f);

            dialogue.text = "Enemy attacks!";

            yield return new WaitForSeconds(.5f);

            playerParty[Random.Range(0, 2)].takeDmg(allCharas[turnIterator].atk);
            updateHP();

            yield return new WaitForSeconds(.5f);
        }

        nextChara();
        setupTurn();
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogue.text = "You WON!";
            yield return new WaitForSeconds(1f);
            int gotXP = 0;
            for (int i =0;i<enemyParty.Count;i++)
            {
                gotXP += enemyParty[i].level * 10;
            }
            dialogue.text = "Your characters got " + gotXP + " XP!";
            yield return new WaitForSeconds(2f);
            for (int i=0;i<3;i++)
            {
                if (playerParty[i].addXP(gotXP))
                {
                    dialogue.text = playerParty[i].unitName + " leveled up!";
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        else if(state == BattleState.LOST)
        {
            dialogue.text = "You Lost!";
        }
        else if (state == BattleState.RUN)
        {
            dialogue.text = "You fled...";
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SugarMine_Scene");
    }

    void showAimEnemyBtns()
    {
        for (int i = 0; i < enemyParty.Capacity; i++)
        {
            if (enemyParty[i].curHP > 0) eAimBtns[i].gameObject.SetActive(true);
        }
    }

    void hideAimEnemyBtns()
    {
        for (int i = 0; i < enemyParty.Count; i++)
        {
            eAimBtns[i].gameObject.SetActive(false);
        }
    }

    void showAimPlayerBtns()
    {
        for (int i = 0; i < playerParty.Count; i++)
        {
            pAimBtns[i].gameObject.SetActive(true);
        }
    }

    void hideAimPlayerBtns()
    {
        for (int i = 0; i < playerParty.Count; i++)
        {
            pAimBtns[i].gameObject.SetActive(false);
        }
    }

    void showPlayerBaseOptions()
    {
        atkBtn.gameObject.SetActive(true);
        skillBtn.gameObject.SetActive(true);
        runBtn.gameObject.SetActive(true);
        itemsBtn.gameObject.SetActive(true);
    }

    void hidePlayerBaseOptions()
    {
        atkBtn.gameObject.SetActive(false);
        skillBtn.gameObject.SetActive(false);
        runBtn.gameObject.SetActive(false);
        itemsBtn.gameObject.SetActive(false);
    }

    void showSkills()
    {
        isItems = false;
        back.gameObject.SetActive(true);
        hidePlayerBaseOptions();
        if (skillPage > 0)
            prevPageSkills.gameObject.SetActive(true);
        else prevPageSkills.gameObject.SetActive(false);
        if (skillPage < (int)(System.Math.Ceiling(allCharas[turnIterator].getSkillCount() / 3.0) - 1))
            nextPageSkills.gameObject.SetActive(true);
        else nextPageSkills.gameObject.SetActive(false);
        for (int i = 1; i < 4; i++)
        {
            if (skillPage * 3 + i <= allCharas[turnIterator].getSkillCount())
            {
                Skill toDisplay = allCharas[turnIterator].getSkill(skillPage * 3 + i);
                aSkill[i - 1].gameObject.SetActive(true);
                if (toDisplay.MpCost > allCharas[turnIterator].curMP) aSkill[i - 1].interactable = false;
                else aSkill[i - 1].interactable = true;
                aSkill[i - 1].GetComponentInChildren<Text>().text = toDisplay.Name;
            }
            else aSkill[i - 1].gameObject.SetActive(false);
        }
    }

    void showItems()
    {
        isItems = true;
        back.gameObject.SetActive(true);
        hidePlayerBaseOptions();
        if (skillPage > 0)
            prevPageSkills.gameObject.SetActive(true);
        else prevPageSkills.gameObject.SetActive(false);
        if (skillPage < (int)(System.Math.Ceiling(playerInventory.Count / 3.0) -1))
            nextPageSkills.gameObject.SetActive(true);
        else nextPageSkills.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            if (skillPage * 3 + i < playerInventory.Count)
            {
                InventoryInfo.ItemInfo toDisplay = playerInventory[skillPage * 3 + i];
                aSkill[i].gameObject.SetActive(true);
                aSkill[i].GetComponentInChildren<Text>().text = toDisplay.Name;
            }
            else aSkill[i].gameObject.SetActive(false);
        }
    }


    void hideSkills()
    {
        prevPageSkills.gameObject.SetActive(false);
        nextPageSkills.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            aSkill[i].gameObject.SetActive(false);
        }
        skillPage = 0;
    }

    public void OnNextSkillPageBtn()
    {
        skillPage++;
        showSkills();
    }

    public void OnPrevSkillPageBtn()
    {
        skillPage--;
        showSkills();
    }

    public void OnBackBtn()
    {
        hideSkills();
        showPlayerBaseOptions();
        back.gameObject.SetActive(false);
    }

    void nextChara()
    {
        turnIterator = (++turnIterator) % allCharas.Capacity;
    }

    bool isPlayerPartyDead()
    {
        return (playerParty[0].curHP <= 0) && (playerParty[1].curHP <= 0) && (playerParty[2].curHP <= 0);
    }

    bool isEnemyPartyDead()
    {
        bool ret = true;
        for (int i = 0; i < enemyParty.Capacity; i++)
        {
            ret = ret && (enemyParty[i].curHP <= 0);
        }
        return ret;
    }

    private void debugFunc()
    {
        for (int i = 0; i < allCharas.Capacity; i++) Debug.Log(allCharas[i].unitName + ": " +  allCharas[i].curHP + "/" + allCharas[i].maxHP);
    }
}
