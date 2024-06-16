using App.Static;
using UnityEngine;
using XLua;

namespace App.Game.Object.Obstacle
{
    /// <summary>
    /// 障碍物当成不会动的怪物处理
    /// </summary>
    [LuaCallCSharp]
    public class Obstacle : Monster.Monster, IObstacle
    {
        private SpriteRenderer spriteRenderer;
        private Sprite originSprite; // 障碍物原图片

        protected override void Awake()
        {
            base.Awake();
            
            signFather = transform.Find("SignFather");
            spriteRenderer = GetComponent<SpriteRenderer>();
            originSprite = spriteRenderer.sprite;
            hpImageBg = transform.Find("HpHolder");
            hpImageFg = transform.Find("HpHolder/HpSlider");
            
            hpImageBg.gameObject.SetActive(false);
        }

        protected override void Update()
        {
            // 超过2秒没受到伤害或怪物死亡隐藏血条
            if (Time.time - lastWoundTime > 2 || IsDead)
            {
                hpImageBg.gameObject.SetActive(false);
            }
        }

        public override void Wound(int woundHp)
        {
            Hp -= woundHp;
        }

        protected override void Dead()
        {
            // 回收
            GameManager.Instance.poolManager.PushObject(gameObject);
            // 记录到统计信息
            GameFacade.Instance.SendNotification(NotificationName.Data.CHANGE_DESTROYOBSTACLE_COUNT, +1);
        }

        public override void OnGet()
        {
            animator.enabled = true;
            // 刷新血
            hp = data.maxHp;
            // 还原动画参数
            animator.SetBool("Dead", false);

            IsDead = false;
        }

        public override void OnPush()
        {
            animator.enabled = false;
            // 还原Sprite
            spriteRenderer.sprite = originSprite;
            // 标记死亡
            IsDead = true;
        }
    
    }
}
