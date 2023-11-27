using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleTower : BaseTower
{
    
    
    public override void Attack()
    {
        
    }

    public override void OnGet()
    {
        
    }

    public override void OnPush()
    {
        // 复原
        animator.runtimeAnimatorController = controllers[0];
        level = 0;
        
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }
}
