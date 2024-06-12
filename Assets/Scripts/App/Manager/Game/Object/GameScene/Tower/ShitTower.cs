using System;
using App.Generic.BaseObject;
using App.Manager.Game.SceneManager.Interf;
using App.MVC.Controller;
using App.MVC.View.GameScene.Object.Bullet;
using App.Static;
using UnityEngine;

namespace App.MVC.View.GameScene.Object.Tower
{
    public class ShitTower : BaseTower
    {
        private ISceneManger SceneManger => GameManager.Instance.sceneManger;
        
        protected override void Awake()
        {
            base.Awake();
            firePos = transform.Find("FirePos");
        }

        protected override void Update()
        {
            base.Update();
        
            if (SceneManger.IsPause())
            {
                // 游戏暂停停止炮塔动画
                animator.SetBool("Attack", false);
            }
        }

        public override void Attack()
        {
            if (target is null) return;
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
}
