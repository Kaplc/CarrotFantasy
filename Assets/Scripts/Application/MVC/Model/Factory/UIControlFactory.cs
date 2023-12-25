using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class UIControlFactory : Proxy
{
    public new const string NAME = "UIControlFactory";
    private string path = "UI/Control/";

    public UIControlFactory() : base(NAME)
    {
    }

    public GameObject CreateControl(string name)
    {
        return PoolManager.Instance.GetObject(path + name);
    }

    public void PushControl(GameObject gameObject)
    {
        PoolManager.Instance.PushObject(gameObject);
    }
}