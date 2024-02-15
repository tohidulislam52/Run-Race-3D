using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    public string a,b,c;
    public Image thirdplace;
    void Start()
    {
        
    }

    void Update()
    {
        texts[0].text = a;
        texts[1].text = b;
        texts[2].text = c;

    }
}
