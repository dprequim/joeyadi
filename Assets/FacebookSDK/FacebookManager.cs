using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System.Linq;
using System;

public class FacebookManager : MonoBehaviour {
	public static FacebookManager Instance;

	public int facebookInviteReward = 10;

	void Awake(){
		if (FacebookManager.Instance != null)
			Destroy (gameObject);

		Instance = this;
//		DontDestroyOnLoad (gameObject);

		if (!FB.IsInitialized) {
			FB.Init (InitCallback, OnHideUnity);
		} else
			FB.ActivateApp ();
	}

	public void Login(){
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			InviteFriends ();
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled login");
		}
	}



	public void InviteFriends(){
		FB.Mobile.AppInvite (new Uri ("https://fb.me/892708710750483"), null, AppInviteCallback);
	}

	public static int SavedCoins{ 
		get { return PlayerPrefs.GetInt (GlobalValue.Coins, 0); } 
		set { PlayerPrefs.SetInt (GlobalValue.Coins, value); } 
	}

	private void AppInviteCallback(IResult result){
		if (result == null)
		{
			return;
		}

		if (!result.Cancelled) {
			SavedCoins += facebookInviteReward;
		}
//
//		this.LastResponseTexture = null;
//
//		// Some platforms return the empty string instead of null.
//		if (!string.IsNullOrEmpty(result.Error))
//		{
//			this.Status = "Error - Check log for details";
//			this.LastResponse = "Error Response:\n" + result.Error;
//		}
//		else if (result.Cancelled)
//		{
//			this.Status = "Cancelled - Check log for details";
//			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
//		}
//		else if (!string.IsNullOrEmpty(result.RawResult))
//		{
//			this.Status = "Success - Check log for details";
//			this.LastResponse = "Success Response:\n" + result.RawResult;
//		}
//		else
//		{
//			this.LastResponse = "Empty Response\n";
//		}

		Debug.Log(result.ToString());
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}
}
