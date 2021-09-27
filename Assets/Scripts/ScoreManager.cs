using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int Score, HighScore, Combo, MaxCombo, MaxLen;

    public void SetZero()
    {
        Combo = 0;
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
        HighScore = HighScore < Score ? Score : HighScore;
        PlayerPrefs.SetInt("SCORE", Score);
        PlayerPrefs.SetInt("HIGHSCORE", HighScore);
    }

    public void SetCombo(bool CountCombo)
    {
        if (CountCombo)
        {
            Combo++;
            MaxCombo = MaxCombo < Combo ? Combo : MaxCombo;
        }
        else
        {
            Combo = 0;
        }
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

    public int GetMaxScore() { return PlayerPrefs.GetInt("HIGHSCORE"); }

    public int GetMaxCombo() { return PlayerPrefs.GetInt("MAXCOMBO"); }

    public int GetCombo() { return Combo; }

    public int GetLen() { return PlayerPrefs.GetInt("MAXLEN"); }

}
