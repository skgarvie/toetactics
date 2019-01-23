using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class IntroPlayer : MonoBehaviour
{
	public Button FadeButton;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private bool canClick = true;
    [SerializeField] private GameObject startButton;

	public AnimationCurve FadeOutCurve;
	public Color BGColor1;
	public Color FadeColor = Color.black;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PauseVideo());
    }


    public IEnumerator PauseVideo()
    {
        yield return new WaitForSeconds(12f);
        videoPlayer.Pause();
        canClick = true;
        startButton.SetActive(true);
    }
    public void OnClick()
    {
        if(!canClick) {
            return;
        }

        StartCoroutine(SplashScreenDone_Routine());
    }

    	private IEnumerator SplashScreenDone_Routine()
	{
		var startTime = 0f;
		var endTime = 1f;

		var fadeImage = FadeButton.GetComponent<Image>();
		var startingColor = fadeImage.color;
		

		while (startTime <= endTime)
		{
			fadeImage.color = Color.Lerp(startingColor, FadeColor, FadeOutCurve.Evaluate(startTime / endTime));
			yield return null;
			startTime += Time.deltaTime;
		}
		
		SceneManager.LoadSceneAsync("Game");
	}

}
