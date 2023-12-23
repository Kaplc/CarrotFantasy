using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

public class UIControlFactory : Proxy
{
    public new const string NAME = "UIControlFactory";
    private string path = "UI/Button/";

    public UIControlFactory() : base(NAME)
    {
    }

    public GameObject CreateControl(string name)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>(path + name));
    }
}