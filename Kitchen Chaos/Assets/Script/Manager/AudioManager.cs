using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    [SerializeField] private SFX_SO sfx_SO;
    DeliveryManager deliveryManager;
    // Start is called before the first frame update
    void Start()
    {
        deliveryManager = DeliveryManager.Instance;

        deliveryManager.SoundOnOrderComplete += DeliveryManager_SoundOnOrderComplete;

        deliveryManager.SoundOnOrderFailed += DeliveryManager_SoundOnOrderFailed;

        CuttingCounter.SoundOnCutAction += CuttingCounter_SoundOnCutAction;

        PlayerInteraction.Instance.SoundOnPickUpSomething += PlayerInteraction_SoundOnPickUpSomething;
        
        BaseCounter.SoundOnDropSomething += BaseCounters_SoundOnDropSomething;

        TrashCounter.SoundOnTrashSomething += TrashCounter_SoundOnTrashSomething;
    }

    private void TrashCounter_SoundOnTrashSomething(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySFX(sfx_SO.trashSFX, trashCounter.transform.position);
    }

    private void BaseCounters_SoundOnDropSomething(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySFX(sfx_SO.objectDropSFX, baseCounter.transform.position);
    }

    private void PlayerInteraction_SoundOnPickUpSomething(object sender, System.EventArgs e)
    {
        PlaySFX(sfx_SO.objectPickUpSFX, PlayerInteraction.Instance.transform.position);
    }

    private void CuttingCounter_SoundOnCutAction(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySFX(sfx_SO.chopSFX, cuttingCounter.transform.position);
    }

    private void DeliveryManager_SoundOnOrderComplete(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySFX(sfx_SO.deliveryCompleteSFX, deliveryCounter.transform.position);
    }

    private void DeliveryManager_SoundOnOrderFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySFX(sfx_SO.deliveryFailedSFX, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySFX(AudioClip[] audioClip, Vector3 position, float volume = 1.0f)
    {
        PlaySound(audioClip[Random.Range(0,audioClip.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position)
    {
        PlaySFX(sfx_SO.footstepSFX, position);
    }
}
