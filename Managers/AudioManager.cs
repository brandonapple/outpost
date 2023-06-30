using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }
   
    public static void PlayClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Clips/" + clipName);
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, MainCam.instance.transform.position,OptionsManager.audioVolumeValue);
        }

        AudioClip clipB = Resources.Load<AudioClip>("Sounds/" + clipName);
        if (clipB!= null)
        {
            AudioSource.PlayClipAtPoint(clipB, MainCam.instance.transform.position, OptionsManager.audioVolumeValue);
        }
    }
    public static void PlayClips(string clipFileName)
    {
        PlayClips(clipFileName, .75f);
    }
    public static void PlayClips(string clipFileName,float volome)
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Clips/" + clipFileName);
        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        AudioSource.PlayClipAtPoint(clip, MainCam.instance.transform.position,volome * OptionsManager.audioVolumeValue);
    }
  
}
