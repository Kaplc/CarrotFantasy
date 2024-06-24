BossGamePanel = BasePanel:SubClass('BossGamePanel')

BossGamePanel.mono = nil

BossGamePanel.imageHp = nil
BossGamePanel.textMoney = nil
BossGamePanel.textTime = nil
BossGamePanel.imagePrize = nil
BossGamePanel.buttonPause = nil
BossGamePanel.buttonResume = nil
BossGamePanel.buttonMenu = nil

BossGamePanel.totalTime = 0
BossGamePanel.time = 0
BossGamePanel.startGame = false
BossGamePanel.prize = nil

function BossGamePanel.Init(self)
    self.totalTime = 0
    self.startGame = false
    -- 添加mono
    self.mono = self.panelObj:AddComponent(typeof(MonoScript))
    self.mono.onUpdateAction = function()
        self:Update()
    end
    -- 绑定控件
    self.imageHp = self.panelObj.transform:Find('ImageMenu/ImageHPBg/ImageHP'):GetComponent('Image')
    self.textMoney = self.panelObj.transform:Find('ImageMenu/TextMoney'):GetComponent('Text')
    self.buttonPause = self.panelObj.transform:Find('ImageMenu/Pause/ButtonPause'):GetComponent('Button')
    self.buttonResume = self.panelObj.transform:Find('ImageMenu/Pause/ButtonResume'):GetComponent('Button')
    self.buttonMenu = self.panelObj.transform:Find('ImageMenu/ButtonMenu'):GetComponent('Button')
    self.textTime = self.panelObj.transform:Find('Time/TextTime'):GetComponent('Text')
    self.imagePrize = self.panelObj.transform:Find('Time/ImagePrize'):GetComponent('Image')

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

    -- 默认金牌
    self:UpdatePrize('Gold')
    -- 显示倒计时面板
    local countDownPanel = UIManager:ShowPanel('CountDownPanel', EUILayers.Bottom)
    countDownPanel.panelObj:GetComponent('CountDownPanel'):StartCountDown()
end

function BossGamePanel:Update()
    if self.startGame == true then
        self.time = self.time - Time.deltaTime
        if self.time > 0 then
            self:UpdateTime(math.ceil(self.time))
        else
            self.time = 0
            self:UpdateTime(0)
        end

        local t = self.time / self.totalTime
        if t >= 0.75 then
            self:UpdatePrize('Gold')
        elseif t >= 0.5 and t < 0.75 then
            self:UpdatePrize('Sliver')
        else
            self:UpdatePrize('Copper')
        end
    end
end

function BossGamePanel:ButtonMenuOnCliCk()
    GameManager.Instance.uiManager:Show('UI/', 'MenuPanel', false)
end

function BossGamePanel:ButtonPauseOnClick()
    self.buttonPause.gameObject:SetActive(false)
    self.buttonResume.gameObject:SetActive(true)
    GameFacade.Instance:SendNotification('PAUSE_GAME')
end

function BossGamePanel:ButtonResumeOnClick()
    self.buttonPause.gameObject:SetActive(true)
    self.buttonResume.gameObject:SetActive(false)
    GameFacade.Instance:SendNotification('RESUME_GAME')
end

function BossGamePanel:UpdateHP(value)
    self.imageHp.fillAmount = value
end

function BossGamePanel:UpdateMoney(value)
    self.textMoney.text = value
end

function BossGamePanel:UpdateTime(time)
    self.textTime.text = time
end

function BossGamePanel:StartCountDown(time)
    self.totalTime = time
    self.time = time
    self.startGame = true
end

function BossGamePanel:UpdatePrize(prize)
    local s = Resources.Load('AB/Art/Prize/' .. prize, typeof(CS.UnityEngine.Sprite))
    self.imagePrize.sprite = s
    self.imagePrize:SetNativeSize()
    self.prize = prize
end

function BossGamePanel:GetPrize()
    return self.prize
end
