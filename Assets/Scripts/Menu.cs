using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Menu : MonoBehaviour
{

    [SerializeField] private TMP_Text greetingLabel;

    public void LaunchGame()
    {
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void LogOut()
    {
        SceneManager.LoadScene(GameScenes.Auth);
    }

    private void Start()
    {
        if(GameMenuText.greetingText != null)
        {
            greetingLabel.text = GameMenuText.greetingText;
        } else
        {
            greetingLabel.text = "Welcome back, captain!";
        }
    }
}
