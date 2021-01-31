using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] mainMusicThemes;

    [SerializeField]
    AudioSource musicAudioSource1;
    [SerializeField]
    AudioSource musicAudioSource2;

    private int currentPlaylistLocation = 0;
    private int currentAudioSourcePlaying;
    private double currentClipStartTime;


    private AudioSource nextAudioSourceToPlay;
    private AudioClip nextAudioClipToPlay;
    private AudioClip currenttAudioClipPlaying;
    private double nextClipToStartTime; 



    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
           // StartMusicPlaylist();
        }
    }
    
}
