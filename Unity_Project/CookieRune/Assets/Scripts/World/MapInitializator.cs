using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapInitializator : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] AudioSource mapMusic;
    // Start is called before the first frame update
    void Start()
    {
        InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
        if (carrier != null)
        {
            Transform whereStart = GameObject.Find(carrier.getLastLocation()).transform;
            hero.transform.position = whereStart.position;
        }
        mapMusic.volume = carrier.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("escape"))
        //{
        //    SceneManager.LoadScene("MainMenu_Scene");
        //}
    }
}
