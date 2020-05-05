using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PTURN, ETURN, WON, LOST, INACTION, RUN }

public class BattleController : MonoBehaviour
{
    public Text enemyName;
    public Text playerHPtext;
    public Text dialogue;

    public Slider pHPslider;
    public Slider eHPslider;

    public BattleState state;
    public GameObject player;
    public GameObject enemy;

    public Transform pBattleStation;
    public Transform eBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(setupBattle());
    }

    IEnumerator setupBattle()
    {
        GameObject pInScene = Instantiate(player, pBattleStation);
        GameObject eInScene = Instantiate(enemy, eBattleStation);

        playerUnit = pInScene.GetComponent<Unit>();
        enemyUnit = eInScene.GetComponent<Unit>();

        enemyName.text = enemyUnit.unitName;

        pHPslider.maxValue = playerUnit.maxHP;
        eHPslider.maxValue = enemyUnit.maxHP;

        updateHP();

        dialogue.text = "Battle starts!";

        yield return new WaitForSeconds(2f);

        state = BattleState.PTURN;
        playerTurn();
    }

    void updateHP()
    {
        pHPslider.value = playerUnit.curHP;
        eHPslider.value = enemyUnit.curHP;
        playerHPtext.text = "HP: " + playerUnit.curHP + "/" + playerUnit.maxHP;
    }

    IEnumerator playerAttack()
    {
        state = BattleState.INACTION;
        //damage enemy
        bool isDead = enemyUnit.takeDmg(playerUnit.atk);

        updateHP();

        yield return new WaitForSeconds(2f);
        //say what happened
        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ETURN;
            StartCoroutine(EnemyTurn());
        }
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
            state = BattleState.ETURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void playerTurn()
    {
        dialogue.text = "It's your turn.";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PTURN)
            return;

        StartCoroutine(playerAttack());
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PTURN)
            return;

        StartCoroutine(playerFlee());
    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = "Enemy attacks!";

        yield return new WaitForSeconds(.5f);

        bool isDead = playerUnit.takeDmg(enemyUnit.atk);
        updateHP();

        yield return new WaitForSeconds(.5f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PTURN;
            playerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogue.text = "You WON!";
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
}
