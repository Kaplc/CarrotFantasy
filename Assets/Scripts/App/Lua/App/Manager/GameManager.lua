GameManager = Object:SubClass('GameManager')

GameManager.cs = nil
GameManager.sceneManager = nil
GameManager.sceneDataManager = nil

function GameManager:Init()
   self.cs =  GameObject.Find('GameManager'):GetComponent('GameManager')

   self:StartBossGame()
end

function GameManager:StartBossGame()
    self.sceneManager = BossGameManager()
    UIManager:ShowPanel('BossPanel', EUILayers.Middle)
end