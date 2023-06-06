*HOW TO USE
Drop the AdController.prefab to the first scene of your game, maybe in flash or menu scene
+ Fill your admob ID

To show/hide ads is very simple, just add the below code to script that is handle the state of game: Fail, Success,...
In my game, it better place in GameManager.cs script, HideAds in Start() and ShowAds in GameOver() or Success()

+ Show Ads: AdsController.ShowAds();
+ Hide Ads: AdsController.HideAds();