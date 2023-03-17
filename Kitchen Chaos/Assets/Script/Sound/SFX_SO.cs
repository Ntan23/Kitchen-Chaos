using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SFX_SO : ScriptableObject
{   
    public AudioClip[] chopSFX;
    public AudioClip[] deliveryFailedSFX;
    public AudioClip[] deliveryCompleteSFX;
    public AudioClip[] footstepSFX;
    public AudioClip[] objectDropSFX;
    public AudioClip[] objectPickUpSFX;
    public AudioClip[] stoveCookingSFX;
    public AudioClip[] trashSFX;
    public AudioClip[] warningSFX;
}
