using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    AudioClip BGmusic;
    [SerializeField]
    AudioClip FaulMusic;
    [SerializeField]
    AudioClip newball;
    [SerializeField]
    AudioClip pinCollide;

    [SerializeField]
    AudioClip Winner;

    AudioSource musicSource;
   // float sfxVolume = 0.5f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }
    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource = CreateAudioSource();
        musicSource.transform.parent = this.transform;
        musicSource.clip = BGmusic;
        musicSource.loop = true;
        musicSource.volume = 0.3f;
        musicSource.Play();
    }

    public void PlayFaulAudio()
    {
       DefaultAudiosSetting(FaulMusic);
    }
    public void NewballAudio()
    {
       DefaultAudiosSetting(newball);
    }
    public void PinCollideAudio()
    {
       DefaultAudiosSetting(pinCollide);
    }
    public void PlayWinnerAudio(){
        DefaultAudiosSetting(Winner);
    }

    

    void DefaultAudiosSetting(AudioClip tempClip){
        musicSource = CreateAudioSource();
        musicSource.transform.parent = this.transform;
        musicSource.clip = tempClip;
        musicSource.loop = false;
        musicSource.volume = 0.3f;
        musicSource.Play();
    }

    

    AudioSource CreateAudioSource()
    {
        GameObject created = new GameObject(System.Guid.NewGuid().ToString());
        return created.AddComponent<AudioSource>();

    }
}
