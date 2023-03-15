/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk interaksi player dengan objek.*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerInteraction Instance {get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs 
    {
        public ClearCounter selectedCounter;
    }

    #region FloatVariables
    public float interactDistance;
    #endregion

    #region VectorVariables
    private Vector2 inputVector;
    private Vector3 moveDirection;
    private Vector3 lastInteractDirection;
    #endregion

    #region OtherVariables
    GameInputManager gameInputManager;
    Detector detector;
    ClearCounter selectedCounter;
    private KitchenObjects kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        gameInputManager = GameInputManager.Instance;
        detector = GetComponent<Detector>();

        gameInputManager.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = gameInputManager.GetNormalizedMovementVector();

        moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDirection != Vector3.zero) lastInteractDirection = moveDirection;

        detector.DetectInteraction(lastInteractDirection);

        if(detector.isInteract)
        {   
            if(detector.interactedObject.TryGetComponent(out ClearCounter counters))
            {
                if(counters != selectedCounter)
                {
                    SetSelectedCouter(counters);
                }
            }
            else
            {
                SetSelectedCouter(null);
            }
        }
        else
        {
            SetSelectedCouter(null);
        }
    }

    private void SetSelectedCouter(ClearCounter selectedCounters)
    {
        selectedCounter = selectedCounters;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
