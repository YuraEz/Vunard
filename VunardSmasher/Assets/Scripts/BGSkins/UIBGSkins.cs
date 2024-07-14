using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIBGSkins : MonoBehaviour
{
    [SerializeField] private UIBGSkin itemPrefab;
    [SerializeField] private Transform itemsContent;

    private BGSkinsManager gunSkinsManager;

    private List<UIBGSkin> spawnedItems;

    private void Start()
    {
        gunSkinsManager = ServiceLocator.GetService<BGSkinsManager>();
        spawnedItems = new List<UIBGSkin>();
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
            BGInfo skinInfo = gunSkinsManager.AllSkins[i];
            UIBGSkin newItem = Instantiate(itemPrefab, itemsContent);
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
