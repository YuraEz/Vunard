using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBGSkin : MonoBehaviour
{

    [SerializeField] private Transform panelImg;
    [SerializeField] private Image visualImg;

    [Space]
    [SerializeField] private Image borderImg;
    [SerializeField] private Image centerImg;
    [SerializeField] private Image iconImg;

    [Space]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private GameObject costCoin;
    [SerializeField] private GameObject buyIcon;

    [SerializeField] private GameObject equipIcon;
    [SerializeField] private GameObject equiped;
    [SerializeField] private GameObject equiped1;
    [SerializeField] private GameObject equiped2;
    [SerializeField] private Button equipBtn;
    [SerializeField] private GameObject realmoney;

    private BGInfo skinInfo;
    private int skinInfoIndex;

    public Action<int> onEquip;

    private void Awake()
    {
        equipBtn.onClick.AddListener(() =>
        {
            onEquip?.Invoke(skinInfoIndex);
        });
    }

    public void SetSkin(int index, bool isEquiped, BGInfo skinInfo)
    {

        skinInfoIndex = index;
        this.skinInfo = skinInfo;

        visualImg.sprite = skinInfo.SkinVisual;
        nameText.text = skinInfo.SkinName;

        if (skinInfo.Buyed)
        {
            costText.text = "";
            costCoin.SetActive(false);
            //equipIcon.SetActive(true);
            buyIcon.SetActive(false);
            realmoney.SetActive(false);
        }
        else
        {
            costText.text = skinInfo.Cost.ToString();
            costCoin.SetActive(!skinInfo.RealMoney);
            costText.gameObject.SetActive(!skinInfo.RealMoney);
            //equipIcon.SetActive(false);
            buyIcon.SetActive(true);
            realmoney.SetActive(skinInfo.RealMoney);
        }

        equiped.SetActive(isEquiped);
        if (isEquiped) panelImg.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);
        if (isEquiped) PlayerPrefs.SetInt("CurrentBG", skinInfo.id);
    }
}
/*
skinInfoIndex = index;
this.skinInfo = skinInfo;

//  borderImg.sprite = skinInfo.SkinBorder;
//  centerImg.sprite = skinInfo.SkinCenter;

if (skinInfo.SkinIcon)
{
    iconImg.sprite = skinInfo.SkinIcon;
}
else
{
    //iconImg.gameObject.SetActive(false);
}

if (skinInfo.Buyed)
{
    costText.text = "";
    costObj.SetActive(false);
    buyIcon.SetActive(false);
    realmoney.SetActive(false);

    if (isEquiped)
    {
        iconImg.gameObject.SetActive(false);
    }
}
else
{
    costText.text = skinInfo.Cost.ToString();
    costText.gameObject.SetActive(!skinInfo.RealMoney);
    costObj.SetActive(!skinInfo.RealMoney);
    buyIcon.SetActive(true);
    realmoney.SetActive(skinInfo.RealMoney);
}

equiped.SetActive(isEquiped);
equiped1.SetActive(isEquiped);
equiped2.SetActive(isEquiped);
if (isEquiped) panelImg.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);
}
}
 */