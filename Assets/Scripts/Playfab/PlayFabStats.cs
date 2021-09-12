using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabStats : MonoBehaviour
{
    public static PlayFabStats Instance;
    public bool isMenu;


    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text chainText;
    [SerializeField] private TMP_Text currencyText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(isMenu)
        {
            GetPlayerStatistics();
        }
    }

    public delegate void OnStatComplete();

    public void UpdatePlayerStatistic(string name, int value, OnStatComplete onComplete)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest()
            {
                Statistics = new List<StatisticUpdate>() {
                new StatisticUpdate() {
                    StatisticName = name,
                    Value = value
                }
                }
            },
            result => onComplete(),
            onError
        );
    }

    private void onError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        SceneManager.LoadScene(GameScenes.Menu);
    }

    public void GetPlayerStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }


    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            if(eachStat.StatisticName == "kills")
            {
                if(killsText != null)
                {
                    killsText.text = "Alien ships destroyed: " + eachStat.Value;
                }
            } else if (eachStat.StatisticName == "maxChain")
            {
                if(chainText != null)
                {
                    chainText.text = "Longest chain: " + eachStat.Value;
                }
            }
        }
            


        // TODO: Set stats in menu
    }

    public void AddCurrency(long amount, OnStatComplete onComplete)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.Amount = (int)amount;
        request.VirtualCurrency = "CR";
        PlayFabClientAPI.AddUserVirtualCurrency(request,
            result => onComplete(),
            error => Debug.Log("Error adding currency: " + error.ErrorMessage)
        );
    }
}
