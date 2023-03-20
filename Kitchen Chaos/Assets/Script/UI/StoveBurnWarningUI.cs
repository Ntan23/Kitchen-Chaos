using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();

        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    
        gameObject.SetActive(false);
    }

    private void StoveCounter_OnProgressChanged(float progressValue)
    {
        if(stoveCounter.GetState() == StoveCounter.State.Fried)
        {
            bool show = progressValue >= 0.5f && progressValue <= 1.0f;

            if(show)
            {
                gameObject.SetActive(true);

                animator.SetTrigger("Flashing");
            }
            else if(!show) gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
