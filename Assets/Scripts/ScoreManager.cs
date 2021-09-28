using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int Score, HighScore, Combo, MaxCombo, MaxChain;

    public void SetZero()
    {
        Combo = 0;
        PlayerPrefs.SetInt("SCORE", 0);
        Score = PlayerPrefs.GetInt("SCORE");
        PlayerPrefs.SetInt("MAXCOMBO", 0);
        MaxCombo = PlayerPrefs.GetInt("MAXCOMBO");
        PlayerPrefs.SetInt("MAXCHAIN", 0);
        MaxChain = PlayerPrefs.GetInt("MAXCHAIN");
        PlayerPrefs.Save();
    }

    public void SetScore(int PulsScore)
    {
        Score += PulsScore;
        PlayerPrefs.SetInt("SCORE", Score);
        SetMaxScore();
    }

    public void SetMaxScore()
    {
        HighScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (HighScore < Score) PlayerPrefs.SetInt("HIGHSCORE", Score);
        else PlayerPrefs.SetInt("HIGHSCORE", HighScore);
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

    public void SetChain(int NewChain)
    {
        if (NewChain > MaxChain)
        {
            MaxChain = NewChain;
        }
        PlayerPrefs.SetInt("MAXCHAIN", MaxChain);
    }

    public int GetScore() { return PlayerPrefs.GetInt("SCORE"); }

    public int GetMaxScore() { return PlayerPrefs.GetInt("HIGHSCORE"); }

    public int GetMaxCombo() { return PlayerPrefs.GetInt("MAXCOMBO"); }

    public int GetCombo() { return Combo; }

    public int GetChain() { return PlayerPrefs.GetInt("MAXCHAIN"); }

}
