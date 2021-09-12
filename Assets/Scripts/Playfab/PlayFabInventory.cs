using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;


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


    private void OnEnable()
    {
        Debug.Log("MENU ENABLED");
        GetStore();
    }


    void MakePurchase()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
        {
            // In your game, this should just be a constant matching your primary catalog
            CatalogVersion = catalogueV,
            ItemId = slectedItem,
            Price = 5,
            VirtualCurrency = "AU"
        }, PurchaseItemSucess, error => Debug.Log("Error: " + error.ErrorMessage));
    }

    internal void RefreshInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), UpdatePlayerInventory, OnError);
    }

    internal void UpdatePlayerInventory(GetUserInventoryResult result)
    {
        playerWallet = result.VirtualCurrency;
        //inventoryView.UpdatePlayerInventory(result);
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
            }
        },
        OnError);
    }



    private void PurchaseItemSucess(PurchaseItemResult result)
    {
        Debug.Log("Purchase succesful");
    }


    private void OnError(PlayFabError error)
    {
        Debug.Log(error);
    }




}
