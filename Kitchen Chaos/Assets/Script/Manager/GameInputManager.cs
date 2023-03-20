/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk input dari player.*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{   
    #region singleton & player Input
    public static GameInputManager Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null) Instance = this;

        playerInputActions = new PlayerInputActions();

        if(PlayerPrefs.HasKey("InputBinding")) playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("InputBinding"));
        
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }
    #endregion
    
    #region ForEvent
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnKeyRebind;
    #endregion

    #region VectorVariables
    private Vector2 inputVector;
    #endregion

    #region OtherVariables
    private PlayerInputActions playerInputActions;
    GameManager gm;
    #endregion

    public enum Binding {
        MoveUp, MoveDown, MoveLeft, MoveRight, Interact, InteractAlternate, Pause
    }

    void  Start()
    {
        gm = GameManager.Instance;
    }

    void OnDestroy()
    {
        if(playerInputActions != null)
        {
            playerInputActions.Player.Interact.performed -= Interact_performed;
            playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
            playerInputActions.Player.Pause.performed -= Pause_performed;
                
            playerInputActions.Dispose();
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if(gm.IsWaitingToStart() || gm.IsGamePlaying()) OnInteractAction?.Invoke(this,EventArgs.Empty);
        else if(!gm.IsGamePlaying()) return;
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        if(gm.IsGamePlaying()) OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        else if(!gm.IsGamePlaying()) return;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if(!gm.IsGameOver()) OnPauseAction?.Invoke(this, EventArgs.Empty);
        else if(gm.IsGameOver()) return;
    }

    public Vector2 GetNormalizedMovementVector()
    {
        if(gm.IsGamePlaying())
        {
            inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        }

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default :
            case Binding.MoveUp :
                return playerInputActions.Player.Movement.bindings[1].ToDisplayString();
            case Binding.MoveDown :
                return playerInputActions.Player.Movement.bindings[2].ToDisplayString();
            case Binding.MoveLeft :
                return playerInputActions.Player.Movement.bindings[3].ToDisplayString();
            case Binding.MoveRight :
                return playerInputActions.Player.Movement.bindings[4].ToDisplayString();    
            case Binding.Interact :
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate :
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause :
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }   
    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch(binding)
        {
            default :
            case Binding.MoveUp :
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 1;
                break;
            case Binding.MoveDown :
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft :
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 3;
                break;
            case Binding.MoveRight :
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 4;
                break;
            case Binding.Interact :
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate :
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause :
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => {
            callback.Dispose();
            playerInputActions.Player.Enable();
            OnActionRebound();
            
            PlayerPrefs.SetString("InputBinding",playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            
            OnKeyRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}
