using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleTower : BaseTower
{
    public Transform weapon;
    public Transform firePos;

    protected override void Update()
    {
        base.Update();
        
        if (GameManager.Instance.Pause)
        {
            // 游戏暂停停止炮塔动画
            animator.SetBool("Attack", false);
            return;
        }

        if (target)
        {
            // 看向目标
            LookAtTarget();
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
        // 播放攻击音效
        (string, float, bool) soundData;
        soundData.Item1 = "Music/Bottle";
        soundData.Item2 = 1;
        soundData.Item3 = false;
        GameFacade.Instance.SendNotification(NotificationName.Game.PLAY_SOUND, soundData);
    }

    public override void OnGet()
    {
        base.OnPush();
    }

    public override void OnPush()
    {
        base.OnPush();
    }
}