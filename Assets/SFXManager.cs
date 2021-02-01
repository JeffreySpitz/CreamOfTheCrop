using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] plop;
    [SerializeField]
    AudioClip fire;
    [SerializeField]
    AudioClip fireStop;
    [SerializeField]
    AudioClip levelComplete;

    [SerializeField]
    AudioSource plopSource;

    [SerializeField]
    AudioSource fireSource;

    [SerializeField]
    AudioSource levelCompleteSource;

    private int lastRandomINT = 0;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown("i"))
        //{
        //    PlopSFX();
        //}

        //if (Input.GetKeyDown("o"))
        //{
        //    FireSFX();
        //}

        //if (Input.GetKeyDown("l"))
        //{
        //    StopFireSFX();
        //}



        //if (Input.GetKeyDown("u"))
        //{
        //    LevelCompleteSFX();
        //}
    }

    public void LevelCompleteSFX()
    {
        levelCompleteSource.loop = false;
        levelCompleteSource.PlayOneShot(levelComplete);
    }

    public void FireSFX()
    {
        fireSource.loop = true;
        fireSource.clip = fire;
        fireSource.Play();
    }

    public void StopFireSFX()
    {
        fireSource.loop = false;
        fireSource.clip = fireStop;
        fireSource.Play();
    }

    public void PlopSFX()
    {
        levelCompleteSource.loop = false;

        int i = Random.Range(0, plop.Length);

        plopSource.PlayOneShot(plop[i]);



      


        
    }
}
