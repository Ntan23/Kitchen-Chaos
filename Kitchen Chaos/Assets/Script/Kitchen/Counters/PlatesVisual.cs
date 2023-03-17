using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesVisual : MonoBehaviour
{
    #region Variables
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] Transform platesVisualPrefab;
    #endregion

    private List<GameObject> platesVisualList = new List<GameObject>();

    void Start()
    {
        platesCounter.OnPlateSpawnned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform platesVisualTransform = Instantiate(platesVisualPrefab, counterTopPoint);

        platesVisualTransform.localPosition += new Vector3(0f, platesVisualList.Count * 0.1f, 0f);

        platesVisualList.Add(platesVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateTaken(object sender, EventArgs e)
    {
        GameObject lastPlate = platesVisualList[platesVisualList.Count-1];
        platesVisualList.Remove(lastPlate);
        Destroy(lastPlate);
    }
}
