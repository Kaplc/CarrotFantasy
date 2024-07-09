using App.Game.Generic.BaseObject;
using App.Game.Object.Bullet;
using App.Static;
using UnityEngine;

namespace App.Game.Object.Tower
{
    public class BottleTower : BaseTower
    {
        private Transform weapon;
        private bool Pause => GameManager.Instance.sceneManager.IsPause();

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
                // 看向目标
                LookAtTarget();
        }

        private void LookAtTarget()
        {
            // 向量
            var dir = target.Transform.position - weapon.position;
            // 计算x轴的角度
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // 
            weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime * data.rotaSpeed);
        }


        public override void Attack()
        {
            base.Attack();

            if (target != null && attacking)
            {
                // 创建子弹预设体并设置目标
                var bullet = GameManager.Instance.poolManager.GetObject(data.bulletsPrefabsPath[level]).GetComponent<BottleTowerBullet>();
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