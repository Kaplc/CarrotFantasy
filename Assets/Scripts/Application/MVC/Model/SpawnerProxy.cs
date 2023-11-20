using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;


/// <summary>
/// 出怪Proxy
/// </summary>
public class SpawnerProxy : Proxy
{
    public new const string NAME = "SpawnerProxy";
    public MapData mapData; // 当前关卡数据
    
    public SpawnerProxy() : base(NAME)
    {
        
    }
    
    
    
}
