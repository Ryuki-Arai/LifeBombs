using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    ScoreManager ScoreManage;
    private void Start()
    {
        ScoreManage = GetComponent<ScoreManager>();
    }
    public void OnClick()
    {
        if (gameObject.name == "StartButton")
        {
            ScoreManage.SetZero();
            SceneManager.LoadScene("Game");
        }
        else if (gameObject.name == "ResultButton")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
