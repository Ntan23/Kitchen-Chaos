using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIngredientSingleIcon : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetKitchenObjectSO(KitchenObjectsSO kitchenObjectsSO)
    {
        icon.sprite = kitchenObjectsSO.sprite;
    }
}
