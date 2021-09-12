using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnGameOver()
    {
        // Update stats

        OnStatsUpdateComplete();
    }


    // WAit for stats update to finish before going back to menu scene
    public void OnStatsUpdateComplete()
    {
        ScoreManager.Instance.Reset();
        SceneManager.LoadScene(GameScenes.Menu);
    }
}
