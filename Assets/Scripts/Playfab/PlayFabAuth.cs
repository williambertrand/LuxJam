
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabAuth : MonoBehaviour
{

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text infoText;

	[SerializeField] private GameObject authPanel;
	[SerializeField] private GameObject gamePanel;
	[SerializeField] private TMP_Text greetingLabel;

	private string userDisplayName;

    private const string _PlayFabRememberMeIdKey = "PlayFabIdPassGuid";

	/// Generated GUID for guest players
	private string RememberMeId
    {
        get
        {
            return PlayerPrefs.GetString(_PlayFabRememberMeIdKey, null);
        }
        set
        {
            var guid = value ?? Guid.NewGuid().ToString();
            PlayerPrefs.SetString(_PlayFabRememberMeIdKey, guid);
        }
    }

	private void ProceedToGameMenu()
    {
		SceneManager.LoadScene(GameScenes.Menu);
	}

	public void RegisterPlayer()
	{
		Debug.Log("[Auth].RegisterPlayer");
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
		{
			Username = usernameInput.text,
			Password = passwordInput.text,
			RequireBothUsernameAndEmail = false,
			DisplayName = nameInput.text
		};
		PlayFabClientAPI.RegisterPlayFabUser(request, onRegisterSuccess, onRegisterError);
	}

	public void LoginPlayer()
	{
		Debug.Log("[Auth].LoginPlayer");
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
		{
			Username = usernameInput.text,
			Password = passwordInput.text,
		};

		PlayFabClientAPI.LoginWithPlayFab(request, OnPlayFabLoginSuccess, OnLoginError);
	}

	public void LoginGuestPlayer()
	{
		Debug.Log("[Auth].LoginGuestPlayer");

		if (string.IsNullOrEmpty(RememberMeId))
		{
			RememberMeId = Guid.NewGuid().ToString();
		}

		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
		{
			CustomId = RememberMeId,
			CreateAccount = true,
		};

		PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccessGuest, OnLoginError);
	}

	private void OnLoginError(PlayFabError response)
	{
		Debug.Log(response.ToString());
		infoText.text = response.ErrorMessage;
	}

	private void OnPlayFabLoginSuccess(LoginResult response)
	{
		Debug.Log("[Auth].OnPlayFabLoginSuccess");
		infoText.text = "Logged in!";
		GameMenuText.greetingText = "Welcome back, pilot <name>. Remember to head over to the " +
			"Instructions tab if you need a refresher on how to pilot your ship." +
			" Or press Launch below to get out there in the basic, starter ship!" +
            "" +
            " To purchase more advanced ships, head over to the Ships tab.";
		GetUserData();
	}


	private void OnPlayFabLoginSuccessGuest(LoginResult response)
	{
		Debug.Log("[Auth].OnPlayFabLoginSuccess");
		infoText.text = "Logged in as guest!";
		GetUserData();
		// Todo, loading before play
		GameMenuText.greetingText = "We're glad to have your guest expertise in the fleet today, pilot! Head on over to the Instructions tab to get familiar with how to pilot your ship.";
	}

	private void onRegisterError(PlayFabError error)
	{
		Debug.LogError("Register error:" + error.ErrorMessage);
		infoText.text = error.ErrorMessage;
	}

	private void onRegisterSuccess(RegisterPlayFabUserResult result)
	{
		infoText.text = "Registered!";
		GameMenuText.greetingText = "Welcome to the Allied Space Force, pilot <name>. Head on over to the Instructions tab to get familiar with how to pilot your ship. Or check out the Ships tab to see what kind of ships you can purchase once you've collected some credits.";
		GetUserData();
	}

	private void GetUserData()
    {
		Debug.Log("[Auth].GetUserData");
		GetAccountInfoRequest request = new GetAccountInfoRequest();
		PlayFabClientAPI.GetAccountInfo(request, OnGetUserDataSuccess, OnGetUserDataError);
    }

	private void OnGetUserDataSuccess(GetAccountInfoResult result)
    {
		userDisplayName = result.AccountInfo.TitleInfo.DisplayName;
		GameMenuText.greetingText = GameMenuText.greetingText.Replace("<name>", userDisplayName);
		ProceedToGameMenu();
	}

	private void OnGetUserDataError(PlayFabError error)
    {
		Debug.Log(error.ErrorMessage);
    }

}
