using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractionDetector interactionDetector;
    public float interactDistance;

    private Vector2 inputVector;
    private Vector3 moveDirection;
    private Vector3 lastInteractDirection;
    GameInputManager gameInputManager;
    Detector detector;
    // Start is called before the first frame update
    void Start()
    {
        gameInputManager = GameInputManager.Instance;
        detector = GetComponent<Detector>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = gameInputManager.GetNormalizedMovementVector();

        moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        if(detector.IsInteract(lastInteractDirection))
        {   
            Debug.Log("Hit");
        }
        else if(!detector.IsInteract(lastInteractDirection))
        {
            Debug.Log("-");
        }
    }
}
