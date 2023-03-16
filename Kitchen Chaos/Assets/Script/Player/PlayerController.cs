/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk mengontrol pergerakan player.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VectorVariables
    private Vector2 inputVector;
    private Vector3 moveDirection;
    private Vector3 moveDirectionX;
    private Vector3 moveDirectionZ;
    #endregion

    #region BoolVariables
    private bool isWalking;
    private bool canMove;
    #endregion

    #region FloatVariables
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    [Header("Player")]
    public float playerRadius = 0.7f;
    public float playerHeight = 2.0f;
    [HideInInspector] public float moveDistance;
    #endregion

    #region OtherVariableS
    GameInputManager gameInputManager;
    CollisionDetector collisionDetector;
    Detector detector;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameInputManager = GameInputManager.Instance;
        // collisionDetector = GetComponent<CollisionDetector>();
        detector = GetComponent<Detector>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        inputVector = gameInputManager.GetNormalizedMovementVector();

        moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        moveDistance = speed * Time.deltaTime;

        canMove = detector.CanMove(moveDirection);

        if(!canMove)
        {
            //Attempt Only On The X-Axis
            moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = moveDirection.x != 0 && detector.CanMove(moveDirectionX);

            if(canMove) /*Can Move Only On The X-Axis*/ moveDirection = moveDirectionX;
            
            else if(!canMove)
            {
                //Attempt Only On The Z-Axis
                moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && detector.CanMove(moveDirectionZ);

                if(canMove) /*Can Move Only On The Z-Axis*/ moveDirection = moveDirectionZ;
            }
        }

        if(canMove) transform.position += moveDirection * moveDistance;
        
        transform.forward += Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking = moveDirection != Vector3.zero;
    }
}
