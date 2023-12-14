using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpPanelPageFlipping : BasePageFlipping
{
    public Text txPage;

    private void OnEnable()
    {
        // 每次激活自动刷新页码
        UpdatePageIndex();
    }

    public void UpdatePageIndex()
    {
        if (!txPage) return;
        txPage.text = $"{pageIndex}/{totalPageIndex}";
    }

    /// <summary>
    /// 实现滑动
    /// </summary>
    protected override void SlideContent()
    {
        base.SlideContent();
        // 每次滑动结束更新页码
        UpdatePageIndex();
    }
}