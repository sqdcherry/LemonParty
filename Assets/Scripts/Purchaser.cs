using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class Purchaser : MonoBehaviour, IStoreListener
{ 

    private void BuyCoins(int coins)
    {
        UIManager.instance.UpdateLemonsCountText(coins);
    }
    private void PurchaseSecondProduct(Product product)
    {
        BuyCoins(5000);
    }

    public void PurchaseFirstProduct(Product product)
    {
        switch (product.transactionID)
        {
            case 0
                {
                    BuyCoins(1500);
                }
                break;
        }
    }


    public void PurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        PopUpManager.instance.StartPopUpAnimation("Operation was failed");
    }

    // Some other event handlers - can be empty.

    public void OnInitializeFailed(InitializationFailureReason error)
    { }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    { }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    { }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    { }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}
