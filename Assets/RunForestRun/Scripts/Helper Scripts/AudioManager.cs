using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;

    public float lowPitchRange = .95f;  //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f; //The highest a sound effect will be randomly pitched.
    public List<AudioClip> animalSounds;

    public bool keepPlaying = true;
    int randomInt;
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    public void BeginAnimalSoundsWithStart()
    {
        StartCoroutine(PlayAnimalSound());
    }

    IEnumerator PlayAnimalSound()
    {
        //Loop all the time through game may not be good practice
        while (keepPlaying)
        {
            randomInt = Random.Range(0, animalSounds.Count - 1);
            efxSource.PlayOneShot(animalSounds[randomInt]);
            yield return new WaitForSeconds(5f);


        }
    }

 
}
