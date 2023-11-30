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
                if (!monster.isDead)
                {
                    target = monster;
                }
                else
                {
                    target = null;
                }
            }
        }
        
        if (target)
        {
            // 看向目标
            LookAtTarget();
            // 大于攻击距离解除锁定
            if (Vector3.Distance(transform.position, target.transform.position) > data.attackRange)
            {
                animator.SetBool("Attack", false);
                target = null;
            }
            
            // 攻击
            if (Time.time > lastAtkTime + AtkCd)
            {
                lastAtkTime = Time.time;
                // Attack();
                animator.SetBool("Attack", true);
            }
            
            // 打死怪物
            if (target != null && target.isDead)
            {
                animator.SetBool("Attack", false);
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
        if (!target) return;
        // 创建子弹预设体并设置目标
        BaseBullet bullet = GameManager.Instance.PoolManager.GetObject(data.bulletsPrefabsPath[level]).GetComponent<BaseBullet>();
        bullet.transform.position = firePos.position;
        bullet.target = target;
        bullet.atk = Atk;
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
