using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionDetector
{
    bool DetectCollision(Vector3 moveDir, GameObject player, PlayerController playerController);
}

public interface IInteractionDetector
{
    bool DetectInteraction(Vector3 moveDir, GameObject player, PlayerInteraction playerInteraction);
}

public class Detector : MonoBehaviour
{
    #region InterfaceVariables
    private ICollisionDetector collisionDetector;
    private IInteractionDetector interactionDetector;
    #endregion

    #region boolVariables
    private bool canMove;
    private bool isInteract;
    #endregion

    #region otherVariables
    PlayerController playerController;
    PlayerInteraction playerInteraction;
    GameObject player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController>();
        playerInteraction = GetComponent<PlayerInteraction>();

        collisionDetector = new Collision();
        interactionDetector = new Interaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanMove(Vector3 moveDir)
    {  
        canMove = !collisionDetector.DetectCollision(moveDir, player, playerController);

        return canMove;
    }

    public bool IsInteract(Vector3 moveDir)
    {
        isInteract = interactionDetector.DetectInteraction(moveDir, player, playerInteraction);

        return isInteract;
    }
}

public class Collision : ICollisionDetector
{
    bool canMove;

    public bool DetectCollision(Vector3 moveDir , GameObject player, PlayerController playerController)
    {
        canMove = Physics.CapsuleCast(player.transform.position, player.transform.position + Vector3.up * playerController.playerHeight, playerController.playerRadius, moveDir, playerController.moveDistance);

        return canMove;
    }
}

public class Interaction : IInteractionDetector
{
    bool isInteract;

    public bool DetectInteraction(Vector3 moveDir , GameObject player, PlayerInteraction playerInteraction)
    {
        isInteract = Physics.Raycast(player.transform.position, moveDir, out RaycastHit rayCastHit,playerInteraction.interactDistance);

        return isInteract;
    }
}
