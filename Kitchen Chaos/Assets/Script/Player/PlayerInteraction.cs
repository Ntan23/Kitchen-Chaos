/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk interaksi player dengan objek.*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IKitchenObjectParent
{
    #region Singleton
    public static PlayerInteraction Instance {get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    #region ForEvent
    public delegate void OnSelectedCounterChange(BaseCounter selectedCounter);
    public event OnSelectedCounterChange OnSelectedCounterChanged;
    public event EventHandler SoundOnPickUpSomething;
    #endregion

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
    BaseCounter selectedCounter;
    private KitchenObjects kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameInputManager = GameInputManager.Instance;
        detector = GetComponent<Detector>();

        gameInputManager.OnInteractAction += GameInput_OnInteractAction;
        gameInputManager.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;

        if(selectedCounter != null) selectedCounter.Interact(this);
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        
        if(selectedCounter != null) selectedCounter.InteractAlternate(this);
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
            if(detector.interactedObject.TryGetComponent(out BaseCounter counters))
            {
                if(counters != selectedCounter) SetSelectedCouter(counters);
            }
            else SetSelectedCouter(null);
        }
        else SetSelectedCouter(null);
    }

    private void SetSelectedCouter(BaseCounter selectedCounters)
    {
        selectedCounter = selectedCounters;

        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }

    public Transform GetKitchenObjectParentTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null) SoundOnPickUpSomething?.Invoke(this, EventArgs.Empty);
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
