using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    private Camera _maincamara;
    private int _currentPlayer;
    public GameObject _CharParent;
    [SerializeField] private bool _OneTabe;
    
    private float _speed= .5f;
    private int _SelectionPos= 13;

    [SerializeField] private Button _PlayButton,_BuyButton;
    [SerializeField] private int _Points;
    [SerializeField] private int[] _ColorPrice;
    [SerializeField] private Text _CoinText;
    // Start is called before the first frame update
    void Start()
    {
        _maincamara = Camera.main;
        CamaraPos();
        CheckIfBuy();
    }
    void Update()
    {
        _CoinText.text = _Points.ToString();
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    public void Buy()
    {
        switch (_currentPlayer)
        {
            
            case 1:
            if(_Points >= _ColorPrice[1] && PlayerPrefs.GetInt("Color1",0) == 0)
            {
                PlayerPrefs.SetInt("Color1",1);
                _Points -= _ColorPrice[1];
            }
            break;

            case 2:
            if(_Points >= _ColorPrice[2] && PlayerPrefs.GetInt("Color2",0) == 0)
            {
                PlayerPrefs.SetInt("Color2",1);
                _Points -= _ColorPrice[2];
            }
            break;

            case 3:
            if(_Points >= _ColorPrice[3] && PlayerPrefs.GetInt("Color3",0) == 0)
            {
                PlayerPrefs.SetInt("Color3",1);
                _Points -= _ColorPrice[3];
            }
            break;

            case 4:
            if(_Points >= _ColorPrice[4] && PlayerPrefs.GetInt("Color4",0) == 0)
            {
                PlayerPrefs.SetInt("Color4",1);
                _Points -= _ColorPrice[4];
            }
            break;

            case 5:
            if(_Points >= _ColorPrice[5] && PlayerPrefs.GetInt("Color5",0) == 0)
            {
                PlayerPrefs.SetInt("Color5",1);
                _Points -= _ColorPrice[5];
            }
            break;

            case 6:
            if(_Points >= _ColorPrice[6] && PlayerPrefs.GetInt("Color6",0) == 0)
            {
                PlayerPrefs.SetInt("Color6",1);
                _Points -= _ColorPrice[6];
            }
            break;
        }
        CheckIfBuy();

    }

    private void CheckIfBuy()
    {
        _PlayButton.interactable = true;
        _BuyButton.interactable = true;
        _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy";
        _BuyButton.image.color = Color.blue;


        switch (_currentPlayer)
        {
            case 0:
                _PlayButton.interactable = true;
                _BuyButton.interactable = false;
                _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                break;

            case 1:
                if(PlayerPrefs.GetInt("Color1") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                    
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[1])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

            case 2:
                if(PlayerPrefs.GetInt("Color2") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[2])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

            case 3:
                if(PlayerPrefs.GetInt("Color3") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[3])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

            case 4:
                if(PlayerPrefs.GetInt("Color4") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[4])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

            case 5:
                if(PlayerPrefs.GetInt("Color5") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[5])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

            case 6:
                if(PlayerPrefs.GetInt("Color6") == 1)
                {
                    _PlayButton.interactable = true;
                    _BuyButton.interactable = false;
                    _BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                   _PlayButton.interactable = false;
                   if(_Points >= _ColorPrice[6])
                        _BuyButton.image.color = Color.blue;
                    else
                        _BuyButton.image.color = Color.red;
                }
                break;

        } 
    }


    private void CamaraPos()
    {
        _currentPlayer = PlayerPrefs.GetInt("PlayerColor");

        _maincamara.transform.position= new Vector3 (_maincamara.transform.position.x +(_currentPlayer*13),
        _maincamara.transform.position.y,_maincamara.transform.position.z);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("PlayerColor",_currentPlayer);
        Debug.Log(PlayerPrefs.GetInt("PlayerColor"));
        
        if(PlayerPrefs.GetInt("Level",2) < 7)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 2));
        else
            SceneManager.LoadScene(Random.Range(2,6));
        // SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 2));
    }

    public void Next()
    {
        if(_OneTabe)
        {
            if(_currentPlayer < _CharParent.transform.childCount - 1)
            {
                _currentPlayer++;
                StartCoroutine(MoveToNext());
                CheckIfBuy();
            }
        }
    }

    public void Pre()
    {
        if(_OneTabe)
        {
            if(_currentPlayer > 0)
            {
                _currentPlayer--;
                StartCoroutine(MoveToBack());
                CheckIfBuy();

            }
        }
    }

    IEnumerator MoveToNext()
    {
        _OneTabe = false;
        Vector3 Tempos = new Vector3 (_maincamara.transform.position.x + _SelectionPos,
        _maincamara.transform.position.y,_maincamara.transform.position.z);
        while(_maincamara.transform.position.x< Tempos.x)
        {
            _maincamara.transform.position = Vector3.MoveTowards(_maincamara.transform.position
            , Tempos,_speed);
            yield return new WaitForSeconds(Time.deltaTime * _speed);
        }
        _OneTabe = true;
        yield return null;
    }

    IEnumerator MoveToBack()
    {
        _OneTabe = false;
        Vector3 _tempos = new Vector3(_maincamara.transform.position.x - _SelectionPos,
        _maincamara.transform.position.y,_maincamara.transform.position.z);
        while (_maincamara.transform.position.x > _tempos.x)
        {
            _maincamara.transform.position = Vector3.MoveTowards(_maincamara.transform.position,
            _tempos,_speed);
            yield return new WaitForSeconds(Time.deltaTime*_speed);
        }
        _OneTabe = true;
        yield return null;
    }

    


}
