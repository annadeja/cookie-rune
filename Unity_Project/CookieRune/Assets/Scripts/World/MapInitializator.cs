using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializator : MonoBehaviour
{
    [SerializeField] GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        InfoCarrier carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
        if (carrier != null)
        {
            Transform whereStart = GameObject.Find(carrier.getLastLocation()).transform;
            hero.transform.position = whereStart.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
