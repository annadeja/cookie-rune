using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCarrier : MonoBehaviour
{
    [SerializeField] string lastLocation;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLastLocation(string name)
    {
        lastLocation = name;
    }

    public string getLastLocation()
    {
        return lastLocation;
    }
}
