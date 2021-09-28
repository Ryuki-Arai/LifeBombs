using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{

    ScoreManager ScoreManage;
    int highscore;
    TextMeshProUGUI HScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManage = GetComponent<ScoreManager>();
        HScoreText = GetComponent<TextMeshProUGUI>();
        highscore = ScoreManage.GetMaxScore();
    }

    // Update is called once per frame
    void Update()
    {
        HScoreText.text = "HighScore\n" + highscore.ToString("N0");
    }
}
