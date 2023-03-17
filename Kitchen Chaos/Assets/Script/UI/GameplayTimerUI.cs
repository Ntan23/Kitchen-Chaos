using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImg;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerColor();
    }   

    void UpdateTimerColor()
    {
        timerImg.fillAmount = gm.GetGameplayTimerNormalized();

        if(timerImg.fillAmount > 0.8f)
        {
            timerImg.color = Color.blue;
        }
        else if(timerImg.fillAmount > 0.5f && timerImg.fillAmount <= 0.8f)
        {
            timerImg.color = new Color32(156,29,226,255);
        }
        else if(timerImg.fillAmount > 0.3f && timerImg.fillAmount <= 0.5f)
        {
            timerImg.color = Color.magenta;
        }
        else if(timerImg.fillAmount <= 0.3f)
        {
            timerImg.color = Color.red;
        }
        
        
    }
}
