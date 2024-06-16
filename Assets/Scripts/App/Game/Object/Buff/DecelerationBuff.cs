using App.Game.Generic.BaseObject;
using App.Game.Object.Monster;

namespace App.Game.Object.Buff
{
    /// <summary>
    /// 便便塔减速Buff
    /// </summary>
    public class DecelerationBuff : BaseBuff
    {

        public DecelerationBuff(float duration) : base(duration)
        {
        
        }

        protected override void OnApplyBuff(IMonster monster)
        {
            // 减速
            monster.SetSpeed(monster.Data.speed / 2f);
        }

        protected override void OnRemoveBuff(IMonster monster)
        {
            // 恢复速度
            monster.SetSpeed(monster.Data.speed);
        }
    }
}
