using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackBtn()
    {
        GameObject carrier = GameObject.Find("ObjectCarrier");
        if (carrier != null) Destroy(carrier);
        SceneManager.LoadScene("MainMenu_Scene");
    }
}
