GameManager = Object:SubClass('GameManager')

GameManager.cs = nil
GameManager.sceneManager = nil
GameManager.sceneDataManager = nil

function GameManager:Init()
    if self.cs == nil then
        self.cs =  GameObject.Find('GameManager'):GetComponent('GameManager')
    end

   self:StartBossGame()
end

function GameManager:StartBossGame()
    if self.sceneManager == nil then
        self.sceneManager = BossGameManager()
    end

    UIManager:ShowPanel('BossPanel', EUILayers.Middle)
end