using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    public TextMeshProUGUI prevText;
    public TextMeshProUGUI nextText;
    public TextMeshProUGUI bonusText;
    public bool isWin = false;

    public ScoreManager scoreManager;

    private void Start()
    {
        nextText.text = $"{scoreManager.scoreInGameBfUpt}";

        if (isWin)
        {
            bonusText.text = $"{scoreManager.scoreInGameBfUpt / 6}";
            return;
        }
    }
}
