using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    private PlayerController playerController;
    private float footstepTimer;
    private float maxFootstepTimer = 0.15f;

    void Awake()
    {   
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        footstepTimer -= Time.deltaTime;

        if(footstepTimer < 0f)
        {
            footstepTimer = maxFootstepTimer;

            if(playerController.IsWalking()) AudioManager.Instance.PlayFootstepSound(playerController.transform.position);
        }
    }
}
