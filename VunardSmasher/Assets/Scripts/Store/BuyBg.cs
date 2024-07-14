using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class BuyBg : MonoBehaviour
{
    [SerializeField] private string bgPlayerPref;
    [SerializeField] private int ProductPrice= 2500;
    [SerializeField] private int bgIndex;
    [SerializeField] private Button useBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI selectText;

    [Space]
    [SerializeField] private string curProductType;

    [Space]
    [SerializeField] bool isDefault;

    [Space]
    [SerializeField] bool donateItem;
    [SerializeField] private string donateItemId;

    private ScoreManager scoreManager;

    private void Start()
    {
        useBtn.onClick.AddListener(SelectItem);
        
        if (donateItem) buyBtn.onClick.AddListener(() => TryBuyProduct(donateItemId));
        else buyBtn.onClick.AddListener(BuyBG);


        scoreManager = ServiceLocator.GetService<ScoreManager>();

        if (isDefault) 
        {
            PlayerPrefs.SetInt(bgPlayerPref, 1);
            SelectItem();
        } 

        int hasBG = PlayerPrefs.GetInt(bgPlayerPref, 0);

        if (hasBG == 1) 
        {
            buyBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);
        }
        else
        {
            buyBtn.gameObject.SetActive(true);
            useBtn.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (PlayerPrefs.GetInt(curProductType, 0) != bgIndex)
        {
            selectText.text = "SELECT";
        }
    }

    public void SelectItem()
    {
        PlayerPrefs.SetInt(curProductType, bgIndex);
        selectText.text = "SELECTED";
    }

    public void BuyBG()
    {
        print("Buy");
        if (PlayerPrefs.GetInt("score", 0) >= ProductPrice)
        {
            scoreManager.ChangeValue(-ProductPrice);

            buyBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);

            PlayerPrefs.SetInt(bgPlayerPref, 1);
            SelectItem();
        }
    }

    //private void TryBuyProduct(string stringId)
    //{

    //    if (!PurchaseManager.Instance.IsInitialized())
    //    {
    //        Debug.Log("IAP is not initialized.");
    //        PopukayuMagazinAz.Instance.ShowFailed();
    //        tryBuyBg.Buyed = false;
    //        onSkinEquip?.Invoke(currentSkin);
    //        return;
    //    }
    //    
    //    Product product = PurchaseManager.Instance._storeController.products.WithID(stringId);
    //    
    //    PopukayuMagazinAz.Instance.ShowLoading();
    //    
    //    if (product != null && product.availableToPurchase)
    //    {
    //        Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
    //        PurchaseManager.Instance.bgInfo = tryBuyBg;
    //        PurchaseManager.Instance._storeController.InitiatePurchase(product);
    //    
    //        //tryBuyBg.Buyed = true;
    //    }
    //    else
    //    {
    //        Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
    //        PopukayuMagazinAz.Instance.ShowFailed();
    //        tryBuyBg.Buyed = false;
    //    }
    //    onSkinEquip?.Invoke(currentSkin);
    //

        private void TryBuyProduct(string stringId)
        {
        PrchsM.Instance.buybg = this;

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
            //BuyBG();
            }
            else
            {
                Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
                PokupkaScreen.Instance.ShowFailed();
            }
        }
    //}
}
