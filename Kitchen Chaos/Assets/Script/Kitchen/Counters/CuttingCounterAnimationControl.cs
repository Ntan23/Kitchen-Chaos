using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimationControl : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cuttingCounter.OnCutAction += CuttingCounter_OnCutAction;
    }

    private void CuttingCounter_OnCutAction(object sender, EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}
