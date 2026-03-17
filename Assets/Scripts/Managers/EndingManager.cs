using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [SerializeField] UnityEvent OnEnd;

    bool hasPlayed = false;

    private void Update()
    {
        if (videoPlayer.isPlaying && hasPlayed == false)
        {
            hasPlayed = true;
        }
        if (!videoPlayer.isPlaying && hasPlayed == true)
        {
            EndIntro();
        }
    }
    
    void EndIntro()
    {
        SceneManager.LoadScene("Main Menu");
        OnEnd.Invoke();
    }
}
