using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public interface IClearCounter 
{
    void Interact(Transform prefab, Transform counterTopPoint);
}

// public abstract class Counter
// {
//     public virtual void Interact(Transform prefab, Transform counterTopPoint)
//     {
        
//     }
// }

// public class ClearCounter : IClearCounter
// {
//     public void Interact(Transform prefab, Transform counterTopPoint)
//     {
//         Debug.Log("Clear Counter Interacted");
//         Transform prefabTransform = Object.Instantiate(prefab,counterTopPoint);
//         prefabTransform.localPosition = Vector3.zero;
//     }
// }

// public class NotClearCounter : Counter
// {
//     public override void Interact(Transform prefab, Transform counterTopPoint)
//     {
//         Debug.Log("Not Clear Counter Interacted");
//     }
// }

public enum counters 
{
    ClearCounter , NotClearCounter
}

[System.Serializable]
public class Counters : MonoBehaviour
{
    public counters counterType;
    [SerializeField] private Counters selectedCounter;
    [SerializeField] private GameObject selectedVisual;
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] Transform counterTopPoint;

    // public void InteractCounter(Counter counter, Transform prefab, Transform counterTopPoint)
    // {
    //     counter.Interact(prefab, counterTopPoint);
    // }

    void Start()
    {
        PlayerInteraction.Instance.OnSelectedCounterChanged += Interaction_OnSelectedCounterChanged;
    }

    private void Interaction_OnSelectedCounterChanged(object sender, PlayerInteraction.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == selectedCounter)
        {
            selectedVisual.SetActive(true);
        }
        else if(e.selectedCounter != selectedCounter)
        {
            selectedVisual.SetActive(false);
        }
    }

    public void CheckWhichCounterIsInteracted()
    {
        switch(counterType)
        {
            case counters.ClearCounter :
                // IClearCounter clearCounterInterface = new ClearCounter();
                // clearCounterInterface.Interact(kitchenObjectsSO.prefab, counterTopPoint);
                // ClearCounter clearCounter =  new ClearCounter();
                // InteractCounter(clearCounter, prefab, counterTopPoint);
                break;
            case counters.NotClearCounter :
                // NotClearCounter notClearCounter =  new NotClearCounter();
                // InteractCounter(notClearCounter,null,null);
                break;
        }
    }
}

[CustomEditor(typeof(Counters))] // replace with the name of your component
public class MyComponentEditor : Editor 
{
    private counters selectedOption;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // show the enum selector
        selectedOption = (counters)EditorGUILayout.EnumPopup("Selected Counter Type", selectedOption);

        // show/hide the variables based on the selected option
        switch (selectedOption) {
            case counters.ClearCounter:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedCounter"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedVisual"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("kitchenObjectsSO"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("counterTopPoint"));
                break;
            default:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedCounter"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedVisual"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}


