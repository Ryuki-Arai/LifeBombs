using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScript : MonoBehaviour
{
    public static bool result;
    TextMeshProUGUI gmRsl;
    int Score,MaxCombo,MaxLen;
    public GameObject score;
    public GameObject combo;
    public GameObject len;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI lenText;
    ScoreManager ScoreManage;

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
    TextMeshProUGUI gmRsl;
>>>>>>> a4f409f58588705fe5fc43782cbb20b4413df77f
>>>>>>> e0bfe9d8b07ae9a9755bda4bd721c603393722f8
    // Start is called before the first frame update
    void Start()
    {
        gmRsl = GetComponent<TextMeshProUGUI>();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> e0bfe9d8b07ae9a9755bda4bd721c603393722f8
        score = GameObject.Find("ScoreText");
        combo = GameObject.Find("ComboText");
        len = GameObject.Find("LengthText");
        scoreText = score.GetComponent<TextMeshProUGUI>();
        comboText = combo.GetComponent<TextMeshProUGUI>();
        lenText = len.GetComponent<TextMeshProUGUI>();
        ScoreManage = GetComponent<ScoreManager>();
        GetResult();
<<<<<<< HEAD
=======
=======
>>>>>>> a4f409f58588705fe5fc43782cbb20b4413df77f
>>>>>>> e0bfe9d8b07ae9a9755bda4bd721c603393722f8
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> e0bfe9d8b07ae9a9755bda4bd721c603393722f8
        if (result) gmRsl.text = "Congratulation!!!";
        else gmRsl.text = "That's too bad...";
        
        scoreText.text = "Score\n" + Score.ToString("N0");
        comboText.text = "MaxCombo\n" + MaxCombo + "Combo";
        lenText.text = "MaxLen\n" + MaxLen + "Length";
    }

    private void GetResult()
    {
        Score = ScoreManage.GetScore();
        MaxCombo = ScoreManage.GetCombo();
        MaxLen = ScoreManage.GetLen();
<<<<<<< HEAD
=======
=======
        if (result)
        {
            gmRsl.text = "Congratulation!!!";
        }
        else
        {
            gmRsl.text = "That's too bad...";
        }
>>>>>>> a4f409f58588705fe5fc43782cbb20b4413df77f
>>>>>>> e0bfe9d8b07ae9a9755bda4bd721c603393722f8
    }
}
