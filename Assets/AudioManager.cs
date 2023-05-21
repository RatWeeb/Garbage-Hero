using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audio;
    [Range(0,1)]
    public float volume;
    private int pastIndex;


    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Ambience());
    }

    IEnumerator Ambience()
    {
        Debug.Log("Started");
        yield return new WaitForSeconds(15f);
        int playing = 0;
        for(int i = 0; i  < audio.Length; i++)
        {
            audio[i].volume = volume;
            if (audio[i].isPlaying)
            {
                playing++;
            }
        }

        int chance = (int)Random.Range(1, 15);

        if(chance == (int)Random.Range(1,15) && playing <= 1)
        {
            PlayRandomAudio();
        }
        StartCoroutine(Ambience());
    }

    void PlayRandomAudio()
    {
        int index = 0;
        do
        {
            index = Random.Range(0, audio.Length);
        } while (index == pastIndex);
        audio[index].Play();

        pastIndex = index;
    }

}
