using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class Countdown : MonoBehaviour
{
    [SerializeField] private GameObject[] countdownGO;
    GameManager gm;
    AudioManager audioManager;

    private int previousCountdownNumber;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        audioManager = AudioManager.Instance;

        HideAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.IsCountdownStarted())
        {
            if(gm.GetCountdownToStartTime() >= 2.0f && gm.GetCountdownToStartTime() < 3.0f) ShowCountdown(0);
            if(gm.GetCountdownToStartTime() >= 1.0f && gm.GetCountdownToStartTime() < 2.0f) ShowCountdown(1);
            if(gm.GetCountdownToStartTime() >= 0.0f && gm.GetCountdownToStartTime() < 1.0f) ShowCountdown(2);

            int countDownNumber = Mathf.CeilToInt(gm.GetCountdownToStartTime());

            if(previousCountdownNumber != countDownNumber)
            {   
                previousCountdownNumber = countDownNumber;

                if(previousCountdownNumber == 1) audioManager.Play("Warning2");
                else audioManager.Play("Warning1");
            }
        } 
        else 
        {
            HideAll();
            return;
        }
    }

    void ShowCountdown(int index)
    {
        for(int i=0; i< countdownGO.Length; i++)
        {
            if(i == index) 
            {
                countdownGO[i].SetActive(true);
                LeanTween.rotate(countdownGO[i], new Vector3(0,90,0), 0.2f);
            }
            else countdownGO[i].SetActive(false);
        }
    }

    void HideAll()
    {
        for(int i=0; i< countdownGO.Length; i++)
        {
            countdownGO[i].SetActive(false);
        }
    }
}
