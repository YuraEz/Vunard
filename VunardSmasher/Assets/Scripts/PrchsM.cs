using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing;
using System;

public class PrchsM : MonoBehaviour, IDetailedStoreListener
{
    public BuyBg buybg;

    private static PrchsM _instance;
    public IStoreController _storeController;
    private IExtensionProvider _storeExtensionProvider;

    public Action<bool> onMs;

    public static PrchsM Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject iapManager = new GameObject("PurchaseManager");
                DontDestroyOnLoad(iapManager);
                _instance = iapManager.AddComponent<PrchsM>();
                _instance.InitializePurchasing();
            }
            return _instance;
        }
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("vunard_location", ProductType.NonConsumable);
        builder.AddProduct("vunard_skin", ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        return _storeController != null && _storeExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason reason, string msg)
    {
        Debug.Log($"IAP Initialization Failed: {reason.ToString()} - {msg}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "vunard_location":
                Debug.Log("vunard_location successfully purchased!");
                PokupkaScreen.Instance.ShowSuccess();
                PlayerPrefs.SetInt("BG4", 1);
                PlayerPrefs.SetInt("curBG", 3);
                buybg.BuyBG();

                break;
            case "vunard_skin":
                Debug.Log("vunard_skin successfully purchased!");
                PokupkaScreen.Instance.ShowSuccess();
                PlayerPrefs.SetInt("Card4", 1);
                PlayerPrefs.SetInt("curCard", 3);
                buybg.BuyBG();

                break;
            default:
                Debug.Log($"Unexpected product ID: {args.purchasedProduct.definition.id}");
                PokupkaScreen.Instance.ShowFailed();
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        PokupkaScreen.Instance.ShowFailed();
        Debug.Log($"Purchase of {product.definition.id} failed due to {failureReason}");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"IAP Initialization Failed: {error.ToString()}");
    }

    public void ReactivatePurchases()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.isEditor)
        {
            Debug.Log("Starting purchase restoration...");

            var apple = _storeExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result, error) =>
            {
                if (result)
                {
                    Debug.Log("Purchases successfully restored.");
                  
                    onMs?.Invoke(true);

                }
                else
                {
                    Debug.Log($"Failed to restore purchases. Error: {error}");
                }
            });
        }
        else
        {
            Debug.Log("Restore purchases is not supported on this platform.");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        PokupkaScreen.Instance.ShowFailed();
        Debug.Log($"Purchase of {product.definition.id} failed due to {failureDescription}");
    }
}
