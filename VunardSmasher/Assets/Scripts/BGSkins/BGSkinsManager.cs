using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class BGSkinsManager : MonoBehaviour
{
    [SerializeField] private BGInfo[] allSkins;
    [SerializeField] private BGInfo currentSkin;

    private ScoreManager scoreManager;

    public Action<BGInfo> onSkinEquip;

    public BGInfo[] AllSkins { get => allSkins; }
    public BGInfo CurrentSkin { get => currentSkin; }

    private BGInfo tryBuyBg;

    private void Start()
    {
       // PurchaseManager.Instance.InitializePurchasing();
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
        BGInfo newSkin = allSkins[index];
        if (newSkin.RealMoney)
        {
         //   int buyed = PlayerPrefs.GetInt(newSkin.BuyID, 0);
          //  newSkin.Buyed = buyed == 1;

            if (newSkin.Buyed == false)
            {
                newSkin.Buyed = false;
                tryBuyBg = newSkin;
                TryBuyProduct(newSkin.BuyID);
                
              
           //     PlayerPrefs.SetInt(newSkin.BuyID, 1);
            }
        }
        else if (tryBuy && !newSkin.Buyed)
        {
           // if (scoreManager.Score < newSkin.Cost) return;

           // scoreManager.ChangeValue(-newSkin.Cost);

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
            tryBuyBg.Buyed = false;
            onSkinEquip?.Invoke(currentSkin);
            return;
        }

        Product product = PrchsM.Instance._storeController.products.WithID(stringId);

        PokupkaScreen.Instance.ShowLoading();

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
            //PurchaseManager.Instance.bgInfo = tryBuyBg;
            PrchsM.Instance._storeController.InitiatePurchase(product);

            //tryBuyBg.Buyed = true;
        }
        else
        {
            Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
            PokupkaScreen.Instance.ShowFailed();
            tryBuyBg.Buyed = false;
        }
        onSkinEquip?.Invoke(currentSkin);
    }
}
