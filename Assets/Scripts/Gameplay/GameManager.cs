using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OnGameOver()
    {
        Debug.Log("[GameManager].OnGameOver");
        // TODO! Update stats here instead of on every kill
        PlayFabStats.Instance.UpdatePlayerStatistic("kills", ScoreManager.Instance.EnemyKillCount, () =>
        {
            Debug.Log("Updated player kill count stat");
            PlayFabStats.Instance.UpdatePlayerStatistic("maxChain", ScoreManager.Instance.MaxChain, () =>
            {
                Debug.Log("Updated player max chain stat");
                OnStatsUpdateComplete();
            });
        });
    }


    // WAit for stats update to finish before going back to menu scene
    public void OnStatsUpdateComplete()
    {
        Debug.Log("[GameManager].OnStatsUpdateComplete");
        ScoreManager.Instance.Reset();
        SceneManager.LoadScene(GameScenes.Menu);
    }
}
