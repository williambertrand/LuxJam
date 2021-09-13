using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabPurchaseItem : MonoBehaviour
{
    [SerializeField] private string itemID;
    [SerializeField] private int itemPrice;

    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private TMPro.TMP_Text purchaseDate;
    [SerializeField] private TMPro.TMP_Text purchaseText;

    [SerializeField] private PlayFabInventory inventory;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(ItemInstance item)
    {
        purchaseButton.SetActive(false);
        equipButton.SetActive(true);
        purchaseDate.text = "Purchased: " + item.PurchaseDate.ToString();
        purchaseText.text = "Owned";
    }

    public void Reset()
    {
        purchaseButton.SetActive(true);
        equipButton.SetActive(false);
        purchaseDate.text = "";
        purchaseText.text = "Price: " + itemPrice;
    }

    public void OnLaunch()
    {
        PlayerSelections.selectedShip = itemID;
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void RequestPurchase()
    {
        inventory.MakePurchase(itemID, itemPrice);
    }

}
