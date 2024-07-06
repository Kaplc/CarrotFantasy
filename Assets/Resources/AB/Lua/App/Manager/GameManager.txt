GameManager = Object:SubClass('GameManager')

GameManager.cs = nil
GameManager.sceneManager = nil

function GameManager:Init()
    if self.cs == nil then
        self.cs =  GameObject.Find('GameManager'):GetComponent('GameManager')
    end

   self:StartBossGame()
end

function GameManager:StartBossGame()
    self.sceneManager = BossGameManager()
    self.cs:SetSceneManager(self.sceneManager.cs)
    
    UIManager:ShowPanel('BossPanel', EUILayers.Middle)
end