using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitButt()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

}
