using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamingManager : MonoBehaviour
{
    public List<GameObject> lives;
    public int maxLives;
    public int livesAmount;

    [Space]
    public int firstAbility;
    public TextMeshProUGUI firstText; 
    public Button useFirst;

    [Space]
    public int secondAbility;
    public TextMeshProUGUI secondText;
    public Button useSecond;

    private UIManager uiManager;
    private ScoreManager scoreManager;
    public Gun gun;

    public CircleManager[] cycles;

    [Button]
    public void AddRockets()
    {
        PlayerPrefs.SetInt("secondAbility", PlayerPrefs.GetInt("secondAbility", 0) + 5);
        UpdateText();   
    }

    private void Start()
    {
        useFirst.onClick.AddListener(UseFirst);
        useSecond.onClick.AddListener(UseSecond);

        uiManager = ServiceLocator.GetService<UIManager>();
        scoreManager = ServiceLocator.GetService<ScoreManager>();
        ShowLives();
        UpdateText();

    }

    public void UpdateText()
    {
        firstAbility = PlayerPrefs.GetInt("firstAbility", 0);
        secondAbility = PlayerPrefs.GetInt("secondAbility", 0);

        firstText.SetText(firstAbility.ToString());
        secondText.SetText(secondAbility.ToString());
    }

    public void AddAbility()
    {
        int randomChoice = Random.Range(0, 2); 

        if (randomChoice == 0)
        {
            firstAbility += 1;
            PlayerPrefs.SetInt("firstAbility", PlayerPrefs.GetInt("firstAbility", 0) +1);

        }
        else
        {
            secondAbility += 1;
            PlayerPrefs.SetInt("secondAbility", PlayerPrefs.GetInt("secondAbility", 0) +1);
        }

        UpdateText();
    }

    private void UseFirst()
    {
        if (PlayerPrefs.GetInt("firstAbility", 0) <= 0) return;
        foreach (CircleManager circle in cycles)
        {
            circle.ability1();
        }
        PlayerPrefs.SetInt("firstAbility", PlayerPrefs.GetInt("firstAbility", 0) -1);
        PlayerPrefs.SetInt("Goal5", PlayerPrefs.GetInt("Goal5", 0) + 1);
        UpdateText();
    }

    private void UseSecond()
    {
        if (PlayerPrefs.GetInt("secondAbility", 0) <= 0) return;
        gun.SpawnGoldenRocket();
        PlayerPrefs.SetInt("secondAbility", PlayerPrefs.GetInt("secondAbility", 0) - 1);
        PlayerPrefs.SetInt("Goal3", PlayerPrefs.GetInt("Goal3", 0) + 1);
        UpdateText();
    }


    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }


    public void ChangeLives(int value)
    {
        livesAmount += value;
        if (livesAmount == 0)
        {
            uiManager.ChangeScreen("lose");
            scoreManager.FinishGame();
        }
        if (livesAmount > maxLives) livesAmount = maxLives;
        ShowLives();
    }

    public void ShowLives()
    {
        foreach (GameObject live in lives)
        {
            live.SetActive(false);
        }
        for (int i = 0; i < lives.Count; i++)
        {
            if (i < livesAmount)
            {
                lives[i].SetActive(true);
            }
            else
            {
                lives[i].SetActive(false);
            }
        }
    }
}
