using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private bool canClick = true;
    [SerializeField] private GameObject startButton;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(PauseVideo());

        // videoPlayer.loopPointReached += EndReached;

    }

    // Update is called once per frame
    void Update()
    {
        if (canClick)
        {

            // var touch = Input.GetTouch(0);
            // touch.phase == TouchPhase.Began
            if (Input.GetMouseButtonDown(0))
            {
                canClick = false;
                OnClick();
            }
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        canClick = true;
        startButton.SetActive(true);
    }

    public IEnumerator PauseVideo()
    {
        yield return new WaitForSeconds(14f);
        videoPlayer.Pause();
        canClick = true;
        startButton.SetActive(true);
    }
    void OnClick()
    {
        SceneManager.LoadScene("Game");
    }

}
