using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdBtn : MonoBehaviour {
    [SerializeField]
    private string placementId = null;

    private Button _button;
    // Use this for initialization
    void Start () {
        _button = GetComponent<Button>();

        if (_button)
        {
            _button.interactable = false;
            _button.onClick.AddListener(ShowAd);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_button == null) return;

        _button.interactable = (
            Advertisement.isInitialized &&
            Advertisement.IsReady(placementId)
        );
    }

    public void ShowAd()
    {
        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show(placementId, options);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
