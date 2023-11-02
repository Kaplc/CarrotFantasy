using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMoveType
{
    PingPong, // 往返循环
    Loop // 返回开头循环
}

public class MoveTool : BaseSingleton<MoveTool>
{
    
    /// <summary>
    /// 循环运动
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="moveType">循环类型</param>
    /// <param name="speed">速度</param>
    /// <param name="time">持续时间</param>
    public void Move(Transform target, EMoveType moveType, float speed, float time)
    {
        switch (moveType)
        {
            case EMoveType.PingPong:
                break;
            case EMoveType.Loop:
                target.Translate(speed * Time.deltaTime * target.forward);
                break;
        }
    }
}
