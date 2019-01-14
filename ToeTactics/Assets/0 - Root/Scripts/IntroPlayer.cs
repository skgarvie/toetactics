using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour
{

    [SerializeField] private VideoPlayer videoPlayer;

    // Use this for initialization
    void Start()
    {
        videoPlayer.loopPointReached += EndReached;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
