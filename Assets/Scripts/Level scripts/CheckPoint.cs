using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [HideInInspector] public GameObject[] checkpiont;
    [HideInInspector] public int cruuntcheckpoint = 1;

    void Awake()
    {
        checkpiont = GameObject.FindGameObjectsWithTag("CheckPoint");
        cruuntcheckpoint=1;
    }
    void Start()
    {
        foreach (GameObject item in checkpiont)
        {
            item.AddComponent<CruuntCheckPoint>();
            item.GetComponent<CruuntCheckPoint>().CruuntCheckPointNumber = cruuntcheckpoint;
            item.name = "CheckPoint" + cruuntcheckpoint;
            cruuntcheckpoint++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
