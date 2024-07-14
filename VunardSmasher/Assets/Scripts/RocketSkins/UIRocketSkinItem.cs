using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRocketSkinItem : MonoBehaviour
{
    [SerializeField] private Image visualImg;
    [SerializeField] private Transform panelImg;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject costCoin;
    [SerializeField] private GameObject buyIcon;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private GameObject equiped;
    [SerializeField] private Button equipBtn;
    [SerializeField] private GameObject realmoney;

    private RocketSkinInfo skinInfo;
    private int skinInfoIndex;

    public Action<int> onEquip;

    private void Awake()
    {
        equipBtn.onClick.AddListener(() =>
        {
            onEquip?.Invoke(skinInfoIndex);
        });
    }

    public void SetSkin(int index, bool isEquiped, RocketSkinInfo skinInfo)
    {
        skinInfoIndex = index;
        this.skinInfo = skinInfo;

        visualImg.sprite = skinInfo.SkinVisual;
        nameText.text = skinInfo.SkinName;

        if (skinInfo.Buyed)
        {
            costText.text = "";
            costCoin.SetActive(false);
            equipIcon.SetActive(true);
            buyIcon.SetActive(false);
            realmoney.SetActive(false);
        }
        else
        {
            costText.text = skinInfo.Cost.ToString();
            //costCoin.SetActive(!skinInfo.RealMoney);
            costText.gameObject.SetActive(!skinInfo.RealMoney);
            equipIcon.SetActive(false);
            buyIcon.SetActive(true);
            realmoney.SetActive(skinInfo.RealMoney);
        }

        equiped.SetActive(isEquiped);
        if (isEquiped) panelImg.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z);
        if (isEquiped) PlayerPrefs.SetInt("CurrentRocket", skinInfo.id);
    }
}
