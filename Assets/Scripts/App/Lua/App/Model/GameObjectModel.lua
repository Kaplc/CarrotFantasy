GameObjectModel = Object:SubClass('GameObjectModel')

function GameObjectModel.GetObject(self, prefabsName)
    local prefab = CS.UnityEngine.Resources.Load(prefabsName)
    local obj = CS.UnityEngine.GameObject.Instantiate(prefab)
    return obj
end
