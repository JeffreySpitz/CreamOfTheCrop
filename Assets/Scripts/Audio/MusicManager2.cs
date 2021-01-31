using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager2 : MonoBehaviour
{
    [SerializeField]
    AudioClip[] mainMusicThemes;

    public AudioSource musicAudioSource1;
    private int musicAudioSource1Samples;
    private double musicAudioSource1DBL;
    [SerializeField]
    AudioSource musicAudioSource2;
    private int musicAudioSource2Samples;
    private double musicAudioSource2DBL;
    [SerializeField]
    AudioSource musicAudioSource3;
    //private int musicAudioSource3Samples;
    //private double musicAudioSource3DBL;
    private int sampleRate = 44100;


    private double time;




    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void PlayPlaylist()
    {
        PlayTrack1();
        PlayTrack2();
        PlayTrack3();
    }
    private void PlayTrack1()
    {
        musicAudioSource1.clip = mainMusicThemes[0];
        musicAudioSource1.PlayScheduled(2);
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




}
