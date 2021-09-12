using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver;
    [SerializeField] DamagePopup TEMP_Damage_Popup_fix;
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
    }

    public void OnGameOver()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        Debug.Log("[GameManager].OnGameOver");
        // TODO! Update stats here instead of on every kill
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
