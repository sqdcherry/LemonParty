using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class Purchaser : MonoBehaviour, IStoreListener
{
    public string firstProductId, secondProductId;
    [SerializeField] private CodelessIAPButton[] IAPButtons;

    private void Start()
    {
        if (IAPButtons == null || IAPButtons.Length < 2)
        {
            Debug.LogError("IAPButtons array is not properly initialized.");
            return;
        }

        IAPButtons[0].onPurchaseComplete.AddListener(PurchaseFirstProduct);
        IAPButtons[0].onPurchaseFailed.AddListener(PurchaseFailed);

        IAPButtons[1].onPurchaseComplete.AddListener(PurchaseSecondProduct);
        IAPButtons[1].onPurchaseFailed.AddListener(PurchaseFailed);
    }

    private void BuyCoins(int coins)
    {
        UIManager.instance.UpdateLemonsCountText(coins);
    }

    public void PurchaseFirstProduct(Product product)
    {
        BuyCoins(1500);
    }

    private void PurchaseSecondProduct(Product product)
    {
        BuyCoins(5000);
    }

    public void PurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.Log($"Purchase failed for product {product.definition.id}. Reason: {purchaseFailureDescription.reason}");
        PopUpManager.instance.StartPopUpAnimation("Operation was failed");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Initialization failed: {error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (purchaseEvent.purchasedProduct.definition.id == firstProductId)
        {
            PurchaseFirstProduct(purchaseEvent.purchasedProduct);
        }
        else if (purchaseEvent.purchasedProduct.definition.id == secondProductId)
        {
            PurchaseSecondProduct(purchaseEvent.purchasedProduct);
        }
        else
        {
            Debug.LogError("Unknown product ID: " + purchaseEvent.purchasedProduct.definition.id);
            return PurchaseProcessingResult.Pending;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }
}
