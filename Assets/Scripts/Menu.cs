using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text greetingLabel;

    public void LaunchGame()
    {
        // Reset selected ship
        PlayerSelections.selectedShip = null;
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void LogOut()
    {
        SceneManager.LoadScene(GameScenes.Auth);
    }

    private void Start()
    {
        Debug.Log("[Menu].start");

        if(GameMenuText.greetingText != null)
        {
            greetingLabel.text = GameMenuText.greetingText;
        } else
        {
            greetingLabel.text = "Welcome back, pilot!";
        }
    }

    private void OnEnable()
    {
        Debug.Log("[Menu].onEnable");
        if (GameMenuText.greetingText != null)
        {
            greetingLabel.text = GameMenuText.greetingText;
        }
        else
        {
            greetingLabel.text = "Welcome back, pilot!";
        }
    }
}
