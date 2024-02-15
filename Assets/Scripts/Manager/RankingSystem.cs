using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    public int cruuntcheckpoint =1,lapcount;
    public float distance,counter;
    public Vector3 checkpoint;
    public int Rank;
    
    void Update()
    {
        calculateRanking();

    }

    private void calculateRanking()
    {
        distance = Vector3.Distance(transform.position,checkpoint);
        counter = lapcount *1000 + cruuntcheckpoint*100 + distance;
    }

    void OnTriggerEnter(Collider terget)
    {
        if(terget.tag == "CheckPoint")
        {
            cruuntcheckpoint= terget.GetComponent<CruuntCheckPoint>().CruuntCheckPointNumber;
            checkpoint = GameObject.Find("CheckPoint" +cruuntcheckpoint).transform.position;
        }

        if(terget.tag == "Finish")
        {
            lapcount+=1;
            GameManager.instance.pass +=1;
        }
    }



}
