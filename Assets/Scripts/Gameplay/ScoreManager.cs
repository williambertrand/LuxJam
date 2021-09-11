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
        }
    }


    public float averageChain;  // TBD 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
