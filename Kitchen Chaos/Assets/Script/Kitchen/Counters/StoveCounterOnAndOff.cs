using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterOnAndOff : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnEffect;
    [SerializeField] private GameObject stoveParticlesFX;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State state)
    {
        if(state == StoveCounter.State.Frying || state == StoveCounter.State.Fried) isOn = true;
        else isOn = false;

        stoveOnEffect.SetActive(isOn);
        stoveParticlesFX.SetActive(isOn);
    }
}
