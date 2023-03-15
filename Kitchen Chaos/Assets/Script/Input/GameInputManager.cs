/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk input dari player.*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{   
    #region singleton
    public static GameInputManager Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
    
    #region VectorVariables
    private Vector2 inputVector;
    #endregion

    #region BoolVariables
    [SerializeField] private bool useNewInputSystem;
    #endregion

    #region OtherVariables
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    #endregion

    void Start()
    {
        if(useNewInputSystem)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Interact.performed += Interact_performed;
        }
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    void Update()
    {
        if(!useNewInputSystem)
        {
            if(Input.GetKey(KeyCode.E)) OnInteractAction?.Invoke(this,EventArgs.Empty);
            else return; 
        }
    }

    public Vector2 GetNormalizedMovementVector()
    {
        if(!useNewInputSystem)
        {
            inputVector = new Vector2(0f,0f);

            if(Input.GetKey(KeyCode.W)) inputVector.y += 1;
            if(Input.GetKey(KeyCode.A)) inputVector.x -= 1;
            if(Input.GetKey(KeyCode.S)) inputVector.y -= 1;
            if(Input.GetKey(KeyCode.D)) inputVector.x += 1;
        }
        
        if(useNewInputSystem)
        {
            inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        }

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
