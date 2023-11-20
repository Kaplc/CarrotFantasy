using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRole : MonoBehaviour, IPoolObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// 适用于对象池回收时复原
    /// </summary>
    /// <param name="obj"></param>
    public virtual void OnPush(GameObject obj)
    {
        // 复原数据
        
        // 回收
        GameManager.Instance.poolManager.PushObject(obj);
    }

    /// <summary>
    /// 使用对象池时初始化
    /// </summary>
    /// <returns></returns>
    public virtual GameObject OnGet(string fullName)
    {
        // 取出
        GameObject obj = GameManager.Instance.poolManager.GetObject(fullName);
        // 初始化
        
        return null;
    }
}
