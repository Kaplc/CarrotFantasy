using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    public TowerData data;
    public int ID => data.id;
    public int Atk => data.atk;
    public int RotaSpeed => data.rotaSpeed;

    public abstract void Attack();
    
}
