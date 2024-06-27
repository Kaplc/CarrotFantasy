BossGameDataManager = Object:SubClass('BossGameDataManager')

BossGameDataManager.cs = nil

function BossGameDataManager:Construct()
    self.cs = LuaSceneDataManager()
    GameManager.cs.sceneManager:SetSceneDataManager(self.script)
end

function BossGameDataManager:Load(path)
    return Resources.Load(path)
end