/*Keterangan : Code ini menggunakan prinsip SRP yang dimana code ini hanya bertanggung jawab untuk mengatur animasi*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerController playerController;
    private Animator playerAnimator;
    #endregion

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("IsWalking",playerController.IsWalking());
    }
}
