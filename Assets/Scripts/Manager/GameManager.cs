using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject[] runner;
    private List<RankingSystem> rankings = new List<RankingSystem>();
    public int pass;
    public string fristplace,secondplace,thirdplace;
    public bool finish,faild,start,faild2;
    private InGameUI inGameUI;

    void Awake()
    {
        instance = this;
        runner = GameObject.FindGameObjectsWithTag("Player");
        inGameUI = FindObjectOfType<InGameUI>();
    }
    void Start()
    {
        for (int i = 0; i < runner.Length; i++)
        {
            rankings.Add(runner[i].GetComponent<RankingSystem>());
        }
    }

    void Update()
    {
        CanculateRanking();
    }

    private void CanculateRanking()
    {
        rankings = rankings.OrderBy(t => t.counter).ToList();

        switch(rankings.Count)
        {
            case 3:
            rankings[0].Rank = 3;
            rankings[1].Rank = 2;
            rankings[2].Rank = 1;

            inGameUI.a = rankings[2].gameObject.name;
            inGameUI.b = rankings[1].gameObject.name;
            inGameUI.c = rankings[0].gameObject.name;
            break;

            case 2:
            rankings[0].Rank = 2;
            rankings[1].Rank = 1;

            inGameUI.a = rankings[1].gameObject.name;
            inGameUI.b = rankings[0].gameObject.name;
            inGameUI.thirdplace.color = Color.red;
            break;

            case 1:
            inGameUI.a = rankings[0].gameObject.name;
            rankings[0].Rank = 1;
            if(fristplace == "")
            {
                if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("Level")&& !faild2)
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
                fristplace = rankings[0].gameObject.name;
                if(GameUI.instance.LeaderBorad.activeInHierarchy == false)
                    GameUI.instance.OpenLeaderBorad();
            }
            break;
        }

       if(pass >= (float)runner.Length/2)
       {
            pass = 0;
            rankings = rankings.OrderBy(t => t.counter).ToList();
            foreach (RankingSystem rs in rankings)
            {
                if(rs.Rank == rankings.Count)
                {
                    if(rs.gameObject.name == PlayerPrefs.GetString("PlayerName"))
                    {
                        Handheld.Vibrate();
                        faild2 = true;
                        faild = true;
                        GameUI.instance.OpenLeaderBorad();
                        // ui
                    }

                    if(thirdplace == "")
                        thirdplace = rs.gameObject.name;
                    else if(secondplace == "")
                        secondplace = rs.gameObject.name;
                    rs.gameObject.SetActive(false);
                }
            }
            runner = GameObject.FindGameObjectsWithTag("Player");
            rankings.Clear();
            for (int i = 0; i < runner.Length; i++)
            {
                rankings.Add(runner[i].GetComponent<RankingSystem>());
            }
            if(rankings.Count <2)
            {
                finish = true;
                Handheld.Vibrate();
            }
       }

    }
}
