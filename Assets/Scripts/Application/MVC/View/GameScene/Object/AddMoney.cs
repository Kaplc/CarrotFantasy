using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddMoney : MonoBehaviour, IPoolObject
{
    public Animator animator;
    public TextMeshPro textMeshPro;

    public void PushSelf()
    {
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }
    
    public void OnGet()
    {
        animator.enabled = true;
    }

    public void OnPush()
    {
        animator.enabled = false;
    }
}
