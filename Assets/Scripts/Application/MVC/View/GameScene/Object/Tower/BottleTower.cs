using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleTower : BaseTower
{
    public Transform weapon;
    public Transform firePos;
    
    private void Update()
    {
        // 查找目标
        for (int i = 0; i < GameManager.Instance.spawner.monsters.Count; i++)
        {
            Monster monster = GameManager.Instance.spawner.monsters[i];
            if (Vector3.Distance(transform.position, monster.transform.position) < data.attackRange && !target)
            {
                target = monster;
            }
        }
        
        if (target)
        {
            // 看向目标
            LookAtTarget();
            // 大于攻击距离解除锁定
            if (Vector3.Distance(transform.position, target.transform.position) > data.attackRange)
            {
                target = null;
            }
        }
    }

    private void LookAtTarget()
    {
        // 向量
        Vector3 dir = target.transform.position - weapon.position;
        // 计算x轴的角度
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // 
        weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime * data.rotaSpeed);
    }
    

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
        
    }
}
