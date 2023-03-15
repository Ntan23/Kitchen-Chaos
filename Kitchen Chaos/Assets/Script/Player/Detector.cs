/*Keterangan : Code ini menggunakan prinsip ISP yang dimana code ini bertanggung jawab untuk mendeteksi adanya collision maupun interaksi dengan menggunakan 2 interface dikarenakan collision dan interaksi memiliki fungsi yang berbeda.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionDetector
{
    bool DetectCollision(Vector3 moveDir, GameObject player, PlayerController playerController);
}

public interface IInteractionDetector
{
    bool DetectInteraction(Vector3 moveDir, GameObject player, PlayerInteraction playerInteraction, LayerMask counterLayerMask);
    Transform GetInteractedObject();
}

public class Detector : MonoBehaviour
{
    #region InterfaceVariables
    private ICollisionDetector collisionDetector;
    private IInteractionDetector interactionDetector;
    #endregion

    #region BoolVariables
    private bool canMove;
    [Header("Interaction")]
    public bool isInteract;
    #endregion

    #region OtherVariables
    public Transform interactedObject;
    [Header("Player")]
    [SerializeField] GameObject player;
    PlayerController playerController;
    PlayerInteraction playerInteraction;
    [Header("Others")]
    [SerializeField] private LayerMask counterLayerMask;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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

    public void DetectInteraction(Vector3 moveDir)
    {
        isInteract = interactionDetector.DetectInteraction(moveDir, player, playerInteraction,counterLayerMask);

        interactedObject = interactionDetector.GetInteractedObject();
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
    RaycastHit raycastHit;

    public bool DetectInteraction(Vector3 moveDir , GameObject player, PlayerInteraction playerInteraction, LayerMask counterLayerMask)
    {
        isInteract = Physics.Raycast(player.transform.position, moveDir, out raycastHit,playerInteraction.interactDistance,counterLayerMask);

        return isInteract;
    }

    public Transform GetInteractedObject()
    {
        return raycastHit.transform;
    }
}
