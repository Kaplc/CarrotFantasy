using UnityEngine;
using UnityEngine.Events;
using XLua;


[LuaCallCSharp]
public class MonoScript : MonoBehaviour
{
    public UnityAction onEnableAction;
    public UnityAction onAwakeAction;
    public UnityAction onStartAction;
    public UnityAction onUpdateAction;
    public UnityAction onDisableAction;
    public UnityAction onDestroyAction;

    private void OnEnable()
    {
        onEnableAction?.Invoke();
    }

    private void Awake()
    {
        onAwakeAction?.Invoke();
    }

    private void Start()
    {
        onStartAction?.Invoke();
    }

    private void Update()
    {
        onUpdateAction?.Invoke();
    }

    private void OnDisable()
    {
        onDisableAction?.Invoke();
    }

    private void OnDestroy()
    {
        onDestroyAction?.Invoke();
    }
}
