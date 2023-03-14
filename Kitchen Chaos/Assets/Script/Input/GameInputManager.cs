/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk input dari player.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{   
    #region singleton
    public static GameInputManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private PlayerInputActions playerInputActions;
    private Vector2 inputVector;
    [SerializeField] private bool useNewInputSystem;

    void Start()
    {
        if(useNewInputSystem)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
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
