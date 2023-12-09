
using System;

/// <summary>
/// 一波怪中的每组相同类型的怪
/// </summary>
[Serializable]
public class GroupData
{
    public MonsterData monsterData;
    public int count; // 相同类型有多少个怪
}