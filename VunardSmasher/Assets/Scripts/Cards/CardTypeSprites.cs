using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Type")]
public class CardTypeSprites:ScriptableObject
{
    [SerializeField] private List<Sprite> cardSprites;

    public List<Sprite> CardSprites => cardSprites;
}