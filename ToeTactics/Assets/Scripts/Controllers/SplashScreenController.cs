using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashScreenController : MonoBehaviour
{

	public string MainScene;
	public Button FadeButton;
	public AnimationCurve FadeOutCurve;
	public Color BGColor1;
	public List<VideoClip> IntroClips = new List<VideoClip>();
    private VideoPlayer _videoPlayer;

	private void OnEnable()
	{
		var rng = Random.Range(0, IntroClips.Count);

		if (_videoPlayer == null)
			_videoPlayer = GetComponent<VideoPlayer>();

		_videoPlayer.clip = IntroClips[rng];

		if (_videoPlayer.clip.name.Contains("gameboy"))
		{
			Camera.main.backgroundColor = BGColor1;
		}
	}

	private void Awake()
    {
	    _videoPlayer = GetComponent<VideoPlayer>();
	    _videoPlayer.loopPointReached += source => SplashScreenDone();
    }

	public void SplashScreenDone()
	{
		StartCoroutine(SplashScreenDone_Routine());
		FadeButton.enabled = false;
	}
	
	private IEnumerator SplashScreenDone_Routine()
	{
		var startTime = 0f;
		var endTime = 1f;

		var fadeImage = FadeButton.GetComponent<Image>();
		var startingColor = fadeImage.color;
		

		while (startTime <= endTime)
		{
			fadeImage.color = Color.Lerp(startingColor, Color.black, FadeOutCurve.Evaluate(startTime / endTime));
			yield return null;
			startTime += Time.deltaTime;
		}
		
		SceneManager.LoadSceneAsync(MainScene);
	}
}
