using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeTips : MonoBehaviour, IPoolObject
{
    public Animator animator;
    
    public void OnGet()
    {
        animator.enabled = true;
    }

    public void OnPush()
    {
        animator.enabled = false;
    }
}
