using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CanBeBurnedKitchenObjectsSO : ScriptableObject
{
    public KitchenObjectsSO cooked;
    public KitchenObjectsSO burned;
    public float maxTimeToBurn;
}
