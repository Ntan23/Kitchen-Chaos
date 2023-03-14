/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk mendetect collision yang terjadi.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool canMove;
    PlayerController playerController;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanMove(Vector3 moveDir)
    {  
        canMove = !Physics.CapsuleCast(player.transform.position, player.transform.position + Vector3.up * playerController.playerHeight, playerController.playerRadius, moveDir, playerController.moveDistance);

        return canMove;
    }
}
