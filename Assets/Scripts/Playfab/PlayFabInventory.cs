using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using TMPro;

[System.Serializable]
public class PlayFabStoreListItem
{
    public string id;
    public PlayFabPurchaseItem purchaseItemUI;
}


// Used by the UI to display the catalog items
public interface ICatalogItem
{
    string Id { get; }
    string DisplayName { get; }
    string CostString { get; }
    bool IsNativeCatalog { get; }
}

public class PlayFabInventory : MonoBehaviour
{
    private const string catalogueV = "jam";
    public string slectedItem = "";
    private Dictionary<string, int> playerWallet = new Dictionary<string, int>();

    [SerializeField] private TMP_Text currencyText;

    [SerializeField] private List<PlayFabStoreListItem> items;


    private void Start()
    {
        RefreshInventory();
    }

    private void OnEnable()
    {
        GetPlayerInventory();
    }


    public void MakePurchase(string itemId, int price)
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
        {
            // In your game, this should just be a constant matching your primary catalog
            CatalogVersion = catalogueV,
            ItemId = itemId,
            Price = price,
            VirtualCurrency = "CR"
        }, PurchaseItemSucess, error => Debug.Log("Error: " + error.ErrorMessage));
    }

    internal void RefreshInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), UpdatePlayerInventory, OnError);
    }

    internal void UpdatePlayerInventory(GetUserInventoryResult result)
    {
        playerWallet = result.VirtualCurrency;
        //inventoryView.UpdatePlayerInventory(result);
        if(result.VirtualCurrency.ContainsKey("CR"))
        {
            currencyText.text = "Credits: " + result.VirtualCurrency["CR"];
        } else
        {
            currencyText.text = "Credits: 0";
        }
        foreach (PlayFabStoreListItem listItem in items)
        {
            //Clear data before showing
            listItem.purchaseItemUI.Reset();
        }

        foreach (var item in result.Inventory)
        {
            foreach (PlayFabStoreListItem listItem in items)
            {
                if (listItem.id == item.ItemId)
                {
                    listItem.purchaseItemUI.SetData(item);
                }
            }
        }
    }


    void GetStore()
    {
    // The prices of catalog items might have changed due to user possible user segment changes based on purchase
    PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest() { StoreId = "store" },
        storeResult =>
        {
            foreach (var item in storeResult.Store)
            {
                // TODO: Add to menu
                Debug.Log("itemId: " + item.ItemId + " => price: " + item.VirtualCurrencyPrices["RM"]);
                foreach(PlayFabStoreListItem listItem in items)
                {
                    if(listItem.id == item.ItemId)
                    {
                        
                    }
                }
            }
        },
        OnError);
    }

    // The double for loop here could be improved, but running out of time :/
    void GetPlayerInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
            result =>
            {
                foreach (PlayFabStoreListItem listItem in items)
                {
                    //Clear data before showing
                    listItem.purchaseItemUI.Reset();
                }

                foreach (var item in result.Inventory)
                {
                    foreach (PlayFabStoreListItem listItem in items)
                    {
                        if (listItem.id == item.ItemId)
                        {
                            listItem.purchaseItemUI.SetData(item);
                        }
                    }
                }
            },
            error => Debug.Log("Inventory error: " + error.ErrorMessage)
        );
    }



    private void PurchaseItemSucess(PurchaseItemResult result)
    {
        Debug.Log("Purchase succesful");
        RefreshInventory();
    }


    private void OnError(PlayFabError error)
    {
        Debug.Log(error);
    }




}
