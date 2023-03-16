using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceProgressBar : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image progressBarImg;

    // Start is called before the first frame update
    void Start()
    {
        cuttingCounter.OnCutProgressChanged += CuttingCounter_OnCutProgressChanged;

        progressBarImg.fillAmount = 0f;

        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnCutProgressChanged(int sliceCount, int maxSliceCount)
    {
        progressBarImg.fillAmount = (float)sliceCount/maxSliceCount;
        UpdateProgressBarColor();

        if(progressBarImg.fillAmount == 1.0f) StartCoroutine(Hide());
        else gameObject.SetActive(true);
    }

    private void UpdateProgressBarColor()
    {
        if(progressBarImg.fillAmount <= 0.2f)
        {
            progressBarImg.color = Color.red;
        }
        else if(progressBarImg.fillAmount > 0.2f && progressBarImg.fillAmount <= 0.7f)
        {
            progressBarImg.color = Color.yellow;
        }
        else if(progressBarImg.fillAmount > 0.7f)
        {
            progressBarImg.color = Color.green;
        }
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}
