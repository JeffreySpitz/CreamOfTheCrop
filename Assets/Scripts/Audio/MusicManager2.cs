using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicManager2 : MonoBehaviour
{
    [SerializeField]
    AudioClip[] mainMusicThemes;
  


    public AudioSource musicAudioSource1;
    private double musicAudioSource1DBL;
    [SerializeField]
    AudioSource musicAudioSource2;
    private double musicAudioSource2DBL;
    [SerializeField]
    AudioSource musicAudioSource3;

    public PauseMusic pauseMusic;

    public AudioMixerSnapshot inGame;
    public AudioMixerSnapshot pause;


    private double time;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("m"))
        {
            PlayPlaylist();
        }

        if (Input.GetKeyDown("t"))
        {
            PauseMusicFadeIn();
        }

        if (Input.GetKeyDown("y"))
        {
            PauseMusicFadeOut();
        }
    }

    public void PlayPlaylist()
    {
        PlayTrack1();
        pauseMusic.PlayPauseMusic();
        PlayTrack2();
        PlayTrack3();
        
    }
    private void PlayTrack1()
    {
        musicAudioSource1.clip = mainMusicThemes[0];
        musicAudioSource1.PlayScheduled(0.1);
        time = AudioSettings.dspTime;
        musicAudioSource1DBL = mainMusicThemes[0].length;
    }

    private void PlayTrack2()
    {
        musicAudioSource2.clip = mainMusicThemes[1];
        musicAudioSource2.PlayScheduled(time + musicAudioSource1DBL);
        musicAudioSource2DBL = mainMusicThemes[1].length;
    }

    private void PlayTrack3()
    {
        musicAudioSource3.clip = mainMusicThemes[2];
        musicAudioSource3.loop = true;
        musicAudioSource3.PlayScheduled(time + musicAudioSource1DBL + musicAudioSource2DBL);

    }

    public void PauseMusicFadeIn()
    {
        pause.TransitionTo(2);
      
    }

    public void PauseMusicFadeOut()
    {
        inGame.TransitionTo(2);

    }


}
