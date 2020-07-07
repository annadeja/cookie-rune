using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] string sceneName;
    InfoCarrier carrier;
    [SerializeField] AudioSource menuMusic;

    public GameObject mainCanv;
    public GameObject optionCanv;
    public GameObject creditsCanv;

    public Toggle fScreen;

    public Dropdown resols;

    bool fscr = true;
    bool inOptions = false;
    bool inCredits = false;

    // Start is called before the first frame update
    void Start()
    {
        carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();

        fScreen.isOn = fscr;
        List<string> resolutions = new List<string>();
        string curRes = Screen.width + " x " + Screen.height;
        foreach (Resolution res in Screen.resolutions)
        {
            resolutions.Add(res.ToString().Split('@')[0]);
        }
        resols.AddOptions(resolutions);
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i].Equals(curRes))
            {
                resols.value = i;
                break;
            }
        }
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
        Application.Quit(1);
    }

    public void OnChangeRes()
    {
        string res = resols.options[resols.value].text;
        string[] sizes = res.Split('x');
        Screen.SetResolution(int.Parse(sizes[0]), int.Parse(sizes[1]), fscr);
    }

    public void OnToggleFull()
    {
        fscr = !fscr;
        OnChangeRes();
    }

    public void OnChangeVolume(System.Single volume)
    {
        menuMusic.volume = volume;
    }

    public void OnToggleOptions()
    {
        inOptions = !inOptions;
        if (inOptions)
        {
            optionCanv.SetActive(true);
            mainCanv.SetActive(false);
        }
        else
        {
            optionCanv.SetActive(false);
            mainCanv.SetActive(true);
        }
    }

    public void OnToggleCredits()
    {
        inCredits = !inCredits;
        if (inCredits)
        {
            creditsCanv.SetActive(true);
            mainCanv.SetActive(false);
        }
        else
        {
            creditsCanv.SetActive(false);
            mainCanv.SetActive(true);
        }
    }

}
