using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class RocketSkinsManager : MonoBehaviour
{
    [SerializeField] private RocketSkinInfo[] allSkins;
    [SerializeField] private RocketSkinInfo currentSkin;

    private ScoreManager scoreManager;

    public Action<RocketSkinInfo> onSkinEquip;

    public RocketSkinInfo[] AllSkins { get => allSkins; }
    public RocketSkinInfo CurrentSkin { get => currentSkin; }

  
    private void Start()
    {
        PrchsM.Instance.InitializePurchasing();
        scoreManager = ServiceLocator.GetService<ScoreManager>();
    }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }


    public void EquipNew(int index, bool tryBuy = false)
    {
        RocketSkinInfo newSkin = allSkins[index];

        if (newSkin.RealMoney)
        {
         //   int buyed = PlayerPrefs.GetInt(newSkin.BuyID, 0);
           // newSkin.Buyed = buyed == 1;

            if (newSkin.Buyed == false)
            {
                TryBuyProduct(newSkin.BuyID);
                newSkin.Buyed = true;
           //     PlayerPrefs.SetInt(newSkin.BuyID, 1);
            }
        }
        else if (tryBuy && !newSkin.Buyed)
        {
            //  if (scoreManager.Score < newSkin.Cost) return;

            //  scoreManager.ChangeValue(-newSkin.Cost);

            if (PlayerPrefs.GetInt("score", 0) < newSkin.Cost) return;

            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score", 0) - newSkin.Cost);

            scoreManager.ChangeValue(0);

            newSkin.Buyed = true;
        }

        currentSkin = newSkin;
        onSkinEquip?.Invoke(currentSkin);
    }

    private void TryBuyProduct(string stringId)
    {
        if (!PrchsM.Instance.IsInitialized())
        {
            Debug.Log("IAP is not initialized.");
            PokupkaScreen.Instance.ShowFailed();
            return;
        }

        Product product = PrchsM.Instance._storeController.products.WithID(stringId);

        PokupkaScreen.Instance.ShowLoading();

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
            PrchsM.Instance._storeController.InitiatePurchase(product);
            print("Купил");
        }
        else
        {
            Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
            PokupkaScreen.Instance.ShowFailed();
        }
    }
}
