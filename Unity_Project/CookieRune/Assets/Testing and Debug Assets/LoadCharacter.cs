using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] int thisNr;

    // Start is called before the first frame update
    void Start()
    {
        GameObject toFind = GameObject.Find("/testerJumper");
        if (toFind != null) { thisNr = toFind.GetComponent<SceneChange>().variable; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
