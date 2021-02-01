using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PauseMusic : MonoBehaviour
{
    [SerializeField]
    AudioClip pauseMusicClip;

    private AudioSource pauseMusicSource;



    // Start is called before the first frame update
    void Start()
    {
        pauseMusicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPauseMusic()
    {
        pauseMusicSource.clip = pauseMusicClip;
        pauseMusicSource.loop = true;
        pauseMusicSource.PlayScheduled(0.1);
    }
}
