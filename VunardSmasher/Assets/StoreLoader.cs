using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StoreLoader : MonoBehaviour
{
    [SerializeField] private CardsSkinFolder[] cardList;
    [SerializeField] private Sprite[] bgList;
    [SerializeField] private Image bgImg;

    [SerializeField] private Image cardDecor1;
    [SerializeField] private Image cardDecor2;

    [SerializeField] private float animTime = 1.0f;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    void Start()
    {
        int CurrentBG = PlayerPrefs.GetInt("curBG", 0);
        int CurrentCardSkin = PlayerPrefs.GetInt("curCard", 0);

        ServiceLocator.GetService<Player>().cardTypes = cardList[CurrentCardSkin].cardTypes;
        ServiceLocator.GetService<CardsManager>().cardTypes = cardList[CurrentCardSkin].cardTypes;
        bgImg.sprite = bgList[CurrentBG];

        ServiceLocator.GetService<Player>().OnStart();
        ServiceLocator.GetService<CardsManager>().OnStart();


        //ChangeDecor();
        //gun.PlaceNewBall();
    }

    public void DecorAnim()
    {
        cardDecor1.GetComponent<Animator>().SetTrigger("Change");
        cardDecor2.GetComponent<Animator>().SetTrigger("Change");
        Invoke("ChangeDecor", animTime);
    }

    public void ChangeDecor()
    {
        int cardTypeIndex = Random.Range(0, ServiceLocator.GetService<Player>().cardTypes.Count);
        int cardNumberIndex = Random.Range(0, ServiceLocator.GetService<CardsManager>().cardTypes[cardTypeIndex].CardSprites.Count);

        cardDecor1.sprite = ServiceLocator.GetService<Player>().cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];

        cardTypeIndex = Random.Range(0, ServiceLocator.GetService<Player>().cardTypes.Count);
        cardNumberIndex = Random.Range(0, ServiceLocator.GetService<CardsManager>().cardTypes[cardTypeIndex].CardSprites.Count);

        cardDecor2.sprite = ServiceLocator.GetService<Player>().cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];

    }
}
