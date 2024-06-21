BossGameDataManager = Object:SubClass('BossGameDataManager')

BossGameDataManager.script = nil

function BossGameDataManager.InitLua(self)
    self.script = LuaSceneDataManager()
    GameManager.Instance.sceneManager:SetSceneDataManager(self.script)
end

function BossGameDataManager.Load(self, path)
    return Resources.Load(path)
end