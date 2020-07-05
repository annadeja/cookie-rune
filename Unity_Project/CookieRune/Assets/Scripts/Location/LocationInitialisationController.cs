using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInitialisationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<System.Tuple<string, bool>> encounters = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>().getPastEncounters();
        if (encounters != null && encounters.Count > 0)
        {
            if (encounters[encounters.Count - 1].Item2)
            {
                Transform lastPos = GameObject.Find(encounters[encounters.Count - 1].Item1).transform;
                GameObject.Find("Player").transform.position = lastPos.position + new Vector3(0, 3, 0);
            }
            else
            {
                Transform lastPos = GameObject.Find(encounters[encounters.Count - 1].Item1).transform;
                GameObject.Find("Player").transform.position = lastPos.position + new Vector3(5, 3, 0);
            }

            foreach (System.Tuple<string, bool> encounter in encounters)
            {
                GameObject EncounterInLocation = GameObject.Find(encounter.Item1);
                if (EncounterInLocation != null)
                {
                    EncounterInLocation.SetActive(!encounter.Item2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
