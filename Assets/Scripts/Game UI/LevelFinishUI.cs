using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishUI : MonoBehaviour
{
    [SerializeField] private Text[] rankings;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rankings[0].text = GameManager.instance.fristplace;
        rankings[1].text = GameManager.instance.secondplace;
        rankings[2].text = GameManager.instance.thirdplace;
    }
}
