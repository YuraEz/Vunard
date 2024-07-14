using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIRocketSkinsManager : MonoBehaviour
{
    [SerializeField] private UIRocketSkinItem itemPrefab;
    [SerializeField] private Transform itemsContent;

    private RocketSkinsManager gunSkinsManager;

    private List<UIRocketSkinItem> spawnedItems;

    private void Start()
    {
        gunSkinsManager = ServiceLocator.GetService<RocketSkinsManager>();
        spawnedItems = new List<UIRocketSkinItem>();
        UpdatePanel();
    }

    private void OnEnable()
    {
        if (gunSkinsManager != null)
        {
            UpdatePanel();
        }
    }

    private void OnDestroy()
    {
        foreach (var item in spawnedItems)
        {
            item.onEquip -= OnSkinClick;
        }
    }

    private void UpdatePanel()
    {
        foreach (var item in spawnedItems)
        {
            item.onEquip -= OnSkinClick;
            Destroy(item.gameObject);
        }

        spawnedItems.Clear();

        for (int i = 0; i < gunSkinsManager.AllSkins.Length; i++)
        {
            RocketSkinInfo skinInfo = gunSkinsManager.AllSkins[i];
            UIRocketSkinItem newItem = Instantiate(itemPrefab, itemsContent);
            newItem.SetSkin(i, skinInfo == gunSkinsManager.CurrentSkin, skinInfo);
            newItem.onEquip += OnSkinClick;
            spawnedItems.Add(newItem);
        }
    }

    private void OnSkinClick(int index)
    {
        gunSkinsManager.EquipNew(index, true);
        UpdatePanel();
    }
}
