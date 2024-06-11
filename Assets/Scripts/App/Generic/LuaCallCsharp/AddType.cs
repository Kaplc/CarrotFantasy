using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using XLua;

[CSharpCallLua]
public static class AddType
{
    [CSharpCallLua]
    public static List<System.Type> CSharpCallLua = new List<System.Type>()
    {
        typeof(System.Action),
        typeof(UnityEngine.Events.UnityAction),
        typeof(UnityEngine.Events.UnityAction<Scene, LoadSceneMode>),
        typeof(UnityEngine.EventSystems.PointerEventData)
    };

    [LuaCallCSharp]
    public static List<System.Type> LuaCallCSharp = new List<System.Type>()
    {
        typeof(SceneManager),
    };
}