using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
     public static GameUI instance;
     [SerializeField] public GameObject IngameUi,LeaderBorad;
     private Button NextButton;
     public Text NumberText;
     public Text currentLevelText, nextLevelText;
    public Image fill;
    public Sprite oragne, gray;
    void Start()
    {
        instance = this;
        StartCoroutine(StratGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.faild)
        {
            if(LeaderBorad.activeInHierarchy)
            {
                GameManager.instance.faild = false;
                Restart();
            }
        }
        

    }

    IEnumerator StratGame()
    {
        NumberText.text = 3.ToString();
        NumberText.color = Random.ColorHSV(0,1,.5f,1,1,1);
        yield return new WaitForSeconds(1);
        NumberText.text = 2.ToString();
        NumberText.color = Random.ColorHSV(0,1,.5f,1,1,1);
        yield return new WaitForSeconds(1);
        NumberText.text = 1.ToString();
        NumberText.color = Random.ColorHSV(0,1,.5f,1,1,1);
        yield return new WaitForSeconds(1);
        NumberText.text = "Go!";
        NumberText.color = Random.ColorHSV(0,1,.5f,1,1,1);
        GameManager.instance.start = true;
        yield return new WaitForSeconds(.5f);
        NumberText.gameObject.SetActive(false);


    }
    public void OpenLeaderBorad()
    {
        IngameUi.SetActive(false);
        LeaderBorad.SetActive(true);
        if(GameManager.instance.faild)
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level",1).ToString();
            nextLevelText.text = PlayerPrefs.GetInt("Level",1)+1+"";
            fill.sprite = gray;
        }
        else
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level",1) +"";
            nextLevelText.text = PlayerPrefs.GetInt("Level",1)+1+"";
            fill.sprite = oragne;
        }
    }

    private void Restart()
    {
        NextButton = GameObject.Find("/Canvas/LeaderBoradPanel/Next botton")
            .GetComponent<Button>();
        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Reload());
        NextButton.transform.GetChild(0).GetComponent<Text>().text = "Restart";
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        if(PlayerPrefs.GetInt("Level",2) < 7)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        else
            SceneManager.LoadScene(Random.Range(2,6));

    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

}
