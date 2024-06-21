BossGamePanel = BasePanel:SubClass('BossGamePanel')

BossGamePanel.imageHp = nil
BossGamePanel.textMoney = nil
BossGamePanel.buttonPause = nil
BossGamePanel.buttonResume = nil
BossGamePanel.buttonMenu = nil

function BossGamePanel.Init(self)
    -- 绑定控件
    self.imageHp = self.panelObj.transform:Find('ImageMenu/ImageHP'):GetComponent('Image')
    self.textMoney = self.panelObj.transform:Find('ImageMenu/TextMoney'):GetComponent('Text')
    self.buttonPause = self.panelObj.transform:Find('ImageMenu/Pause/ButtonPause'):GetComponent('Button')
    self.buttonResume = self.panelObj.transform:Find('ImageMenu/Pause/ButtonResume'):GetComponent('Button')
    self.buttonMenu = self.panelObj.transform:Find('ImageMenu/ButtonMenu'):GetComponent('Button')

    -- 控件初始化
    self.buttonResume.gameObject:SetActive(false)
    self.buttonMenu.onClick:AddListener(
        function()
            self:ButtonMenuOnCliCk()
        end
    )
    self.buttonPause.onClick:AddListener(
        function()
            self:ButtonPauseOnClick()
        end
    )
    self.buttonResume.onClick:AddListener(
        function()
            self:ButtonResumeOnClick()
        end
    )

    -- 显示倒计时面板
    local countDownPanel = UIManager:ShowPanel('CountDownPanel', EUILayers.Bottom)
    countDownPanel.panelObj:GetComponent('CountDownPanel'):StartCountDown()
end

function BossGamePanel:ButtonMenuOnCliCk()
    GameManager.Instance.uiManager:Show('UI/', 'MenuPanel', false)
end

function BossGamePanel:ButtonPauseOnClick()
    self.buttonPause.gameObject:SetActive(false)
    self.buttonResume.gameObject:SetActive(true)
    GameFacade.Instance:SendNotification('PAUSE_GAME');
end

function BossGamePanel:ButtonResumeOnClick()
    self.buttonPause.gameObject:SetActive(true)
    self.buttonResume.gameObject:SetActive(false)
    GameFacade.Instance:SendNotification('RESUME_GAME');
end

function BossGamePanel:UpdateHP(value)
    self.imageHp.fillAmount = value
end

function BossGamePanel:UpdateMoney(value)
    self.textMoney.text = value
end
