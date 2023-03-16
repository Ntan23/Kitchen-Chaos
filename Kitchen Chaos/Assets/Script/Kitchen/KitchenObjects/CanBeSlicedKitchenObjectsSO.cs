using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CanBeSlicedKitchenObjectsSO : ScriptableObject
{
    public KitchenObjectsSO unsliced;
    public KitchenObjectsSO sliced;
    public int maxSliceCount;
} 
