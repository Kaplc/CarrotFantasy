using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseTower : MonoBehaviour, IPoolObject
{
    public TowerData data;
    public int ID => data.id;
    public int Atk => data.atk;
    public int RotaSpeed => data.rotaSpeed;
    public int level;

    public abstract void Attack();

    public abstract void OnGet();
    
    public abstract void OnPush();
}
