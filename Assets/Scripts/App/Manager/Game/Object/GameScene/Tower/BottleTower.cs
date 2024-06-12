using System;
using App.Generic.BaseObject;
using App.MVC.Controller;
using App.MVC.View.GameScene.Object.Bullet;
using App.Static;
using UnityEngine;
using XLua;

namespace App.MVC.View.GameScene.Object.Tower
{
    public class BottleTower : BaseTower
    {
        private Transform weapon;
        private bool Pause => GameManager.Instance.sceneManger.IsPause();
        
        protected override void Awake()
        {
            base.Awake();
            
            animator = GetComponent<Animator>();
            weapon = transform.Find("Weapon");
            firePos = transform.Find("Weapon/FirePos");
        }

        protected override void Update()
        {
            base.Update();
        
            if (Pause)
            {
                // 游戏暂停停止炮塔动画
                animator.SetBool("Attack", false);
                return;
            }

            if (target != null)
            {
                // 看向目标
                LookAtTarget();
            }
        }

        private void LookAtTarget()
        {
            // 向量
            Vector3 dir = TargetTsf.position - weapon.position;
            // 计算x轴的角度
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // 
            weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime * data.rotaSpeed);
        }


        public override void Attack()
        {
            base.Attack();

            if (target != null && attacking)
            {
                // 创建子弹预设体并设置目标
                BottleTowerBullet bullet = GameManager.Instance.PoolManager.GetObject(data.bulletsPrefabsPath[level]).GetComponent<BottleTowerBullet>();
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
        }
    
    }
}