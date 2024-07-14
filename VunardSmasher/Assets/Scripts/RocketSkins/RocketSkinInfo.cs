using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun Skin")]
public class RocketSkinInfo : ScriptableObject
{
    [SerializeField] private string skinName;
    [SerializeField] private Sprite skinVisual;
  //  [SerializeField] private Sprite gameSkin;

    [Space]
    [SerializeField] private int cost;
    [SerializeField] private bool buyed;

    [Space]
    [SerializeField] private bool realMoney;
    [SerializeField] private string buyID;

    [Space]
    public int id;

    public string SkinName { get => skinName; }
    public Sprite SkinVisual { get => skinVisual; }

    public int Cost { get => cost; }
    public bool Buyed { get => buyed; set => buyed = value; }
    public bool RealMoney { get => realMoney; }
    public string BuyID { get => buyID; }
    //public Sprite GameSkin { get => gameSkin; }
}
