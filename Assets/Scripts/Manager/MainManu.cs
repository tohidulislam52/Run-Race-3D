using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public InputField PlayerName;
    public Text Currenttext, NextText;
    
    // Start is called before the first frame update
    void Start()
    {
        Currenttext.text = PlayerPrefs.GetInt("Level", 1).ToString();
        NextText.text = PlayerPrefs.GetInt("Level", 1) + 1 + "";
        if(PlayerPrefs.GetInt("FristName",0) == 0 )
        {
            PlayerPrefs.SetInt("FristName",1);
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        else
        {
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void StartGame()
    {
        if (PlayerName.text == "")
            PlayerPrefs.SetString("PlayerName", "Player");
        else
            PlayerPrefs.SetString("PlayerName", PlayerName.text);

        if(PlayerPrefs.GetInt("Level",2) < 7)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 2));
        else
            SceneManager.LoadScene(Random.Range(2,6));
    }

    public void Colors()
    {
        SceneManager.LoadScene(1);
    }
    
}
