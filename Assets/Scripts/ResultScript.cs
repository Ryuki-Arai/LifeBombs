using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScript : MonoBehaviour
{
    public static bool result;
    TextMeshProUGUI gmRsl;
    int Score,MaxCombo,MaxChain;
    public GameObject score;
    public GameObject combo;
    public GameObject chain;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI chainText;
    ScoreManager ScoreManage;

    // Start is called before the first frame update
    void Start()
    {
        gmRsl = GetComponent<TextMeshProUGUI>();
        score = GameObject.Find("ScoreText");
        combo = GameObject.Find("ComboText");
        chain = GameObject.Find("ChainText");
        scoreText = score.GetComponent<TextMeshProUGUI>();
        comboText = combo.GetComponent<TextMeshProUGUI>();
        chainText = chain.GetComponent<TextMeshProUGUI>();
        ScoreManage = GetComponent<ScoreManager>();
        GetResult();
    }

    // Update is called once per frame
    void Update()
    {
        if (result) gmRsl.text = "Success!!";
        else gmRsl.text = "Faild...";
        
        scoreText.text = "Score\n" + Score.ToString("N0");
        comboText.text = "MaxCombo\n" + MaxCombo;
        chainText.text = "MaxChain : " + MaxChain;
    }

    private void GetResult()
    {
        Score = ScoreManage.GetScore();
        MaxCombo = ScoreManage.GetMaxCombo();
        MaxChain = ScoreManage.GetChain();
    }
}
