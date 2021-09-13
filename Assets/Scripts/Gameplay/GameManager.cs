using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool isGameOver;
    [SerializeField] DamagePopup TEMP_Damage_Popup_fix;

    // Kinda messy, will clean this up after the jam
    [SerializeField] private GameObject defaultShip;
    [SerializeField] private GameObject AdvancedShip;
    [SerializeField] private GameObject ExtremeShip;


    [Header("Player UI Set up")]
    [SerializeField] private TMP_Text bombCountText;
    [SerializeField] private TMP_Text creditsText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cam;

    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isGameOver = false;

        //Set player selected ship
        Debug.Log("Initializing with ship: " + PlayerSelections.selectedShip);
        GameObject ship;

        if (PlayerSelections.selectedShip == null)
        {
            ship = Instantiate(defaultShip);
        } else if (PlayerSelections.selectedShip == "ship-1")
        {
            ship = Instantiate(AdvancedShip);
        }
        else if (PlayerSelections.selectedShip == "ship-2")
        {
            ship = Instantiate(ExtremeShip);
        } else
        {
            Debug.LogError("Could not find prefab for: " + PlayerSelections.selectedShip);
            ship = Instantiate(defaultShip);
        }

        PlayerShipHealth shipHealth = ship.GetComponent<PlayerShipHealth>();
        shipHealth.shieldBar = shieldSlider;
        shipHealth.healthBar = healthSlider;
        shipHealth.SetupSliders();

        PlayerInventory inventory = ship.GetComponent<PlayerInventory>();
        inventory.bombCountText = bombCountText;
        inventory.creditsText = creditsText;

        cam.Follow = ship.transform;
    }

    public void OnGameOver()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        Debug.Log("[GameManager].OnGameOver");
        PlayFabStats.Instance.UpdatePlayerStatistic("kills", ScoreManager.Instance.EnemyKillCount, () =>
        {
            Debug.Log("Updated player kill count stat");
            PlayFabStats.Instance.UpdatePlayerStatistic("maxChain", ScoreManager.Instance.MaxChain, () =>
            {
                Debug.Log("Updated player max chain stat");
                PlayFabStats.Instance.AddCurrency(PlayerInventory.Instance.credits, () =>
                {
                    Debug.Log("Updated player currency");
                    OnStatsUpdateComplete();
                });
            });
        });
    }


    // WAit for stats update to finish before going back to menu scene
    public void OnStatsUpdateComplete()
    {
        Debug.Log("[GameManager].OnStatsUpdateComplete");
        GameMenuText.greetingText = "Well done out there pilot! You managed to destroy " + ScoreManager.Instance.EnemyKillCount + " enemy ships, with a longest explosion chain of " + ScoreManager.Instance.MaxChain + ". You brought in " + PlayerInventory.Instance.credits + " credits.";
        Debug.Log("Updated greeting text: " + GameMenuText.greetingText);
        ScoreManager.Instance.Reset();
        SceneManager.LoadScene(GameScenes.Menu);
    }
}
