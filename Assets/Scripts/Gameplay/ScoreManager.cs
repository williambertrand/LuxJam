using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    #region Singeton
    public static ScoreManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] private TMP_Text enemyText;
    private int enemyKills;
    public int EnemyKillCount
    {
        get
        {
            return enemyKills;
        }
        set
        {
            enemyKills = value;
            enemyText.text = "" + enemyKills;
            // Todo: potentially just send this update on player death
            PlayFabStats.Instance.UpdatePlayerStatistic("kills", value);
        }
    }


    [SerializeField] private TMP_Text chainText;
    private int maxChain;
    public int MaxChain
    {
        get {
            return maxChain;
        }
        set
        {
            maxChain = value;
            chainText.text = "" + maxChain;
            PlayFabStats.Instance.UpdatePlayerStatistic("maxChain", value);
        }
    }


    public float averageChain;  // TBD 

    public void Reset()
    {
        MaxChain = 0;
        EnemyKillCount = 0;
    }
}
