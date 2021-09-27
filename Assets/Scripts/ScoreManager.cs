using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int Score, MaxCombo, MaxLen;

    public void SetZero()
    {
        
        PlayerPrefs.SetInt("SCORE", 0);
        Score = PlayerPrefs.GetInt("SCORE");
        PlayerPrefs.SetInt("MAXCOMBO", 0);
        MaxCombo = PlayerPrefs.GetInt("MAXCOMBO");
        PlayerPrefs.SetInt("MAXLEN", 0);
        MaxLen = PlayerPrefs.GetInt("MAXLEN");
        PlayerPrefs.Save();
    }

    public void SetScore(int PulsScore)
    {
        Score += PulsScore;
        PlayerPrefs.SetInt("SCORE", Score);
    }

    public void SetCombo(bool CountCombo)
    {
        if (CountCombo) MaxCombo++;
        else MaxCombo = 0;
        PlayerPrefs.SetInt("MAXCOMBO", MaxCombo);
    }

    public void SetLen(int NewLen)
    {
        if (NewLen > MaxLen)
        {
            MaxLen = NewLen;
        }
        PlayerPrefs.SetInt("MAXLEN", MaxLen);
    }

    public int GetScore() { return PlayerPrefs.GetInt("SCORE"); }

    public int GetCombo() { return PlayerPrefs.GetInt("MAXCOMBO"); }

    public int GetLen() { return PlayerPrefs.GetInt("MAXLEN"); }

}
