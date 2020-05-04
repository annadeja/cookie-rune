using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] string sceneName;
    InfoCarrier carrier;

    // Start is called before the first frame update
    void Start()
    {
         carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        carrier.setLastLocation("LocationP_Town");
        SceneManager.LoadScene("WorldMap_Scene");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Debug.Log("quitting");
        Application.Quit(1);
    }
}
