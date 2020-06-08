using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] string battleName;
    [Header("Enemy params")]
    [SerializeField] string[] enemyName;
    [SerializeField] int[] enemyLvl;
    [SerializeField] int[] enemyHP;
    [SerializeField] int[] enemyAtk;
    [SerializeField] int[] enemyDef;
    [SerializeField] int[] enemyMag;
    [SerializeField] int[] enemyMdef;
    [SerializeField] int[] enemySpd;

    List<Character> enemyParty;

    // Start is called before the first frame update
    void Start()
    {
        enemyParty = new List<Character>();
        for (int i = 0; i < enemyName.Length; i++)
        {
            enemyParty.Add(new Character(enemyName[i], enemyLvl[i], enemyHP[i], 0, enemyAtk[i], enemyDef[i], enemyMag[i], enemyMdef[i], enemySpd[i]));
        }
        enemyParty.Capacity = enemyName.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
            carrier.setEnemyParty(enemyParty);
            carrier.addEncounter(gameObject.name);
            SceneManager.LoadScene(battleName);
        }
    }
}
