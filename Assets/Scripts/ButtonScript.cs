using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void OnClick()
    {
        if (gameObject.name == "StartButton")
        {
            SceneManager.LoadScene("Game");
        }
        else if (gameObject.name == "ResultButton")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
