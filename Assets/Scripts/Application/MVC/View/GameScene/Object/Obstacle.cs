using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障碍物当成不会动的怪物处理
/// </summary>
public class Obstacle : Monster
{
    public SpriteRenderer spriteRenderer;
    public Sprite originSprite; // 障碍物原图片

    protected override void Update()
    {
        // 超过2秒没受到伤害或怪物死亡隐藏血条
        if (Time.realtimeSinceStartup - lastWoundTime > 2 || isDead)
        {
            hpImageBg.gameObject.SetActive(false);
        }
    }

    public override void OnGet()
    {
        animator.enabled = true;
        // 刷新血
        hp = data.maxHp;
        // 还原动画参数
        animator.SetBool("Dead", false);

        isDead = false;
    }

    public override void OnPush()
    {
        animator.enabled = false;
        // 还原Sprite
        spriteRenderer.sprite = originSprite;
    }
}
