using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance {get; private set;}
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }
    #endregion

    [SerializeField] private SFX_SO sfx_SO;
    [SerializeField] private Sound[] soundEffects;
    [SerializeField] Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundEffects,sound=>sound.name==name);

        if(s == null)
        {
            //Debug.LogWarning("Sound :"+name+" not found");
            return;
        }

        s.source.PlayOneShot(s.clip);
    }
    
    public void TrashCounter_SoundOnTrashSomething()
    {
        int randomIndex = UnityEngine.Random.Range(0,2);

        if(randomIndex == 0) Play("Trash1");
        else Play("Trash2");
    }

    public void BaseCounters_SoundOnDropSomething()
    {
        int randomIndex = UnityEngine.Random.Range(0,3);

        if(randomIndex == 0) Play("Drop1");
        else if(randomIndex == 1) Play("Drop2");
        else Play("Drop3");
    }

    public void PlayerInteraction_SoundOnPickUpSomething()
    {
        int randomIndex = UnityEngine.Random.Range(0,3);

        if(randomIndex == 0) Play("PickUp1");
        else if(randomIndex == 1) Play("PickUp2");
        else Play("PickUp3");
    }

    public void CuttingCounter_SoundOnCutAction()
    {
        int randomIndex = UnityEngine.Random.Range(0,3);

        if(randomIndex == 0) Play("Chop1");
        else if(randomIndex == 1) Play("Chop2");
        else Play("Chop3");
    }

    public void DeliveryManager_SoundOnOrderComplete()
    {
        int randomIndex = UnityEngine.Random.Range(0,2);

        if(randomIndex == 0) Play("DeliverySuccess1");
        else Play("DeliverySuccess2");
    }

    public void DeliveryManager_SoundOnOrderFailed()
    {
        int randomIndex = UnityEngine.Random.Range(0,2);

        if(randomIndex == 0) Play("DeliveryFail1");
        else Play("DeliveryFail2");
    }

    public void PlayFootstepSound(Vector3 position)
    {
        //PlaySFX(sfx_SO.footstepSFX, position);
        int randomIndex = UnityEngine.Random.Range(0,4);

        if(randomIndex == 0) Play("Footstep1");
        else if(randomIndex == 1) Play("Footstep2");
        else if(randomIndex == 2) Play("Footstep3");
        else Play("Footstep4");
    }
}
