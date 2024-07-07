GameManager = Object:SubClass('GameManager')

GameManager.cs = nil
GameManager.sceneManager = nil

GameManager.addressablesManager = nil

function GameManager:Init()
    if self.cs == nil then
        self.cs = GameObject.Find('GameManager'):GetComponent('GameManager')
        self.addressablesManager = self.cs.addressablesManager
    end

    self:StartBossGame()
end

function GameManager:StartBossGame()
    self.sceneManager = BossGameManager()
    self.cs:SetSceneManager(self.sceneManager.cs)

    -- 预加载BossPanel的资源
    self.addressablesManager:PreloadAssetsAsync(
        function (done)
            if done then
                UIManager:ShowPanel('BossPanel', EUILayers.Middle, nil)
            end
        end,
        LuaPreloadAssetInfo(typeof(Sprite), 'BossPanel', 'Copper'),
        LuaPreloadAssetInfo(typeof(Sprite), 'BossPanel', 'Gold'),
        LuaPreloadAssetInfo(typeof(Sprite), 'BossPanel', 'Sliver'),
        LuaPreloadAssetInfo(typeof(GameObject), 'BossPanel')
    )
end
