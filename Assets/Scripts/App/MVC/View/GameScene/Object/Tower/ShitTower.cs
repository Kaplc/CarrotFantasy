using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitTower : BaseTower
{
    public Transform firePos;

    protected override void Update()
    {
        base.Update();
        
        if (GameManager.Instance.Pause)
        {
            // 游戏暂停停止炮塔动画
            animator.SetBool("Attack", false);
        }
    }

    public override void Attack()
    {
        if (!target) return;
        // 创建子弹预设体并设置目标
        ShitTowerBullet bullet = GameManager.Instance.PoolManager.GetObject(data.bulletsPrefabsPath[level]).GetComponent<ShitTowerBullet>();
        bullet.transform.position = firePos.position;
        bullet.target = target;
        bullet.atk = Atk;
        // 播放攻击音效
        (string, float, bool) soundData;
        soundData.Item1 = "Music/Shit";
        soundData.Item2 = 1;
        soundData.Item3 = false;
        GameFacade.Instance.SendNotification(NotificationName.Game.PLAY_SOUND, soundData);
    }
}
