using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CanBeCookedKitchenObjectsSO : ScriptableObject
{
    public KitchenObjectsSO uncooked;
    public KitchenObjectsSO cooked;
    public float maxTimeToCook;
}
