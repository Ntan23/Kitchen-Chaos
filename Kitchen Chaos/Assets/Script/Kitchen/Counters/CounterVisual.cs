/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk highlight counter yang didekati.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter selectedCounter;
    [SerializeField] private GameObject[] selectedVisual;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInteraction.Instance.OnSelectedCounterChanged += Interaction_OnSelectedCounterChanged;
    }

    private void Interaction_OnSelectedCounterChanged(BaseCounter selected)
    {
        if(selected == selectedCounter) ShowVisual();
        else if(selected != selectedCounter) HideVisual();
    }

    private void ShowVisual()
    {
        foreach(GameObject visualGO in selectedVisual) visualGO.SetActive(true);
    }

    private void HideVisual()
    {
        foreach(GameObject visualGO in selectedVisual) visualGO.SetActive(false);
    }
}
