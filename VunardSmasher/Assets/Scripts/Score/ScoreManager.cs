using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private int score;
    public int results;
    public int Score { get { return score; } }

    public Action<int> onBalanceChange;

    private void OnEnable()
    {
       // score = PlayerPrefs.GetInt("score", 0);
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    [Button]
    private void add100()
    {
        ChangeValue(100);
        FinishGame();
    }

    public void ChangeValue(int value)
    {
        score += value; 
        onBalanceChange?.Invoke(score);
    }

    public void FinishGame()
    {
        results = score;
        PlayerPrefs.SetInt("score", score + PlayerPrefs.GetInt("score", 0));
        if (PlayerPrefs.GetInt("record", 0) < results) PlayerPrefs.SetInt("record", results);
        if (results >= 1000) PlayerPrefs.SetInt("goal5", PlayerPrefs.GetInt("goal5", 0) + 1);
        if (results >= 1500) PlayerPrefs.SetInt("goal6", PlayerPrefs.GetInt("goal6", 0) + 1);
        if (results >= 2000) PlayerPrefs.SetInt("goal7", PlayerPrefs.GetInt("goal7", 0) + 1);
        if (results >= 5000) PlayerPrefs.SetInt("goal8", PlayerPrefs.GetInt("goal8", 0) + 1);
    }


    public int scoreInGameBfUpt;
    public void Finish(bool e)
    {

    }
    public void UpdateGame(int few)
    {

    }
    public void UpdateScore(int fw)
    {

    }
}
