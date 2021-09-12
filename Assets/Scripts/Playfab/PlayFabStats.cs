using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabStats : MonoBehaviour
{
    public static PlayFabStats Instance;
    public bool isMenu;

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


    public void UpdatePlayerStatistic(string name, int value)
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
            result => Debug.Log("Updated! Player stat: " + name),
            error => Debug.Log(error.GenerateErrorReport())
        );
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
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
        // TODO: Set stats in menu
    }

    public void AddCurrency(int amount)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.Amount = amount;
        request.VirtualCurrency = "RM";
        PlayFabClientAPI.AddUserVirtualCurrency(request,
            result => Debug.Log("Added currency!"),
            error => Debug.Log("Error adding currency: " + error.ErrorMessage)
        );
    }
}
