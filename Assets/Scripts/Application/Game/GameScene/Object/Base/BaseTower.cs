using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour, IPoolObject
{
    public TowerData data;
    public int ID => data.id;
    public int Atk => data.atk;
    public int RotaSpeed => data.rotaSpeed;

    public abstract void Attack();

    public abstract void OnGet();
    
    public abstract void OnPush();
}
