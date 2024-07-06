BossGameLosePanel = BasePanel:SubClass('BossGameLosePanel')

BossGameLosePanel.txBossName = nil
BossGameLosePanel.btnRestart = nil
BossGameLosePanel.btnSelectLevel = nil

function BossGameLosePanel:Init()
    self.txBossName = self.panelObj.transform:Find('TextBossName'):GetComponent('Text')
    self.btnRestart = self.panelObj.transform:Find('ButtonRestart'):GetComponent('Button')
    self.btnSelectLevel = self.panelObj.transform:Find('ButtonSelectLevel'):GetComponent('Button')

    self.btnRestart.onClick:AddListener(function()
        self:OnClickRestart()
    end)

    self.btnSelectLevel.onClick:AddListener(function ()
        self:OnClickSelectLevel()
    end)
end

function BossGameLosePanel:UpdateBossName(name)
    self.txBossName.text = name
end

function BossGameLosePanel:OnClickRestart()
    UIManager:HidePanel('BossGameLosePanel')
    GameManager.sceneManager:RestartGame()
end

function BossGameLosePanel:OnClickSelectLevel()
    UIManager:HidePanel('BossGameLosePanel')
    GameManager.sceneManager:SelectLevel()
end