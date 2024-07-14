using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="BG")]
public class BGInfo : ScriptableObject
{
    [SerializeField] private string skinName;
    [SerializeField] private Sprite skinVisual;

    [Space]
    [SerializeField] private Sprite skinBorder;
    [SerializeField] private Sprite skinCenter;
    [SerializeField] private Sprite skinIcon;

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

    public Sprite SkinBorder { get => skinBorder; }
    public Sprite SkinCenter { get => skinCenter; }

    public int Cost { get => cost; }
    public bool Buyed { get => buyed; set => buyed = value; }
    public Sprite SkinIcon { get => skinIcon; }

    public bool RealMoney { get => realMoney; }
    public string BuyID { get => buyID; }
}
