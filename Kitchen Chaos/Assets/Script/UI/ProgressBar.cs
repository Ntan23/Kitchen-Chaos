using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject hasProgressGO;
    [SerializeField] Image progressBarImg;
    private IHasProgress hasProgress;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        hasProgress = hasProgressGO.GetComponent<IHasProgress>();

        if(hasProgress == null) Debug.LogError("GameObject doesnt not implement IHasProgress");

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        progressBarImg.fillAmount = 0.0f;

        gameObject.SetActive(false);
    }

    private void HasProgress_OnProgressChanged(float progressValue)
    {
        progressBarImg.fillAmount = progressValue;
        UpdateProgressBarColor();

        if(progressBarImg.fillAmount == 0.0f || progressBarImg.fillAmount == 1.0f) StartCoroutine(Hide());
        else gameObject.SetActive(true);

        if(hasProgressGO.GetComponent<StoveCounter>() != null)
        {   
            if(hasProgressGO.GetComponent<StoveCounter>().GetState() == StoveCounter.State.Fried)
            {   
                bool show = progressValue >= 0.5f;

                if(show)
                {   
                    void UpdateBarColor(Color color)
                    {
                        progressBarImg.color = color;
                    }

                    LeanTween.value(progressBarImg.gameObject, UpdateBarColor, Color.red, Color.yellow, 0.3f);
                }
            }
        }
    }

    private void UpdateProgressBarColor()
    {
        if(progressBarImg.fillAmount <= 0.3f) progressBarImg.color = Color.red;
        else if(progressBarImg.fillAmount > 0.3f && progressBarImg.fillAmount <= 0.7f) progressBarImg.color = Color.yellow;
        else if(progressBarImg.fillAmount > 0.7f) progressBarImg.color = Color.green;
        
        if(hasProgressGO.GetComponent<StoveCounter>() != null)
        {
            if(hasProgressGO.GetComponent<StoveCounter>().GetState() == StoveCounter.State.Fried)
            {
                if(progressBarImg.fillAmount <= 0.3f) progressBarImg.color = Color.green;
                else if(progressBarImg.fillAmount > 0.3f && progressBarImg.fillAmount <= 0.4f) progressBarImg.color = Color.yellow;
            }
        }
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
