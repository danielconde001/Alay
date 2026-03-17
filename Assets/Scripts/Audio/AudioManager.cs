using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource sfxPlayer;

    [SerializeField] List<AudioClip> clips;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(string p_name) 
    {

        for (int i = 0; i < clips.Count; i++)
        {
            if (clips[i].name == p_name)
            {
                sfxPlayer.PlayOneShot(clips[i]);
            }
        }
    }
}
