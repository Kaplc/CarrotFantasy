BossGamePanel = BasePanel:SubClass("BossGamePanel")

function BossGamePanel.Init(self)
    -- 显示倒计时面板
    local countDownPanel = UIManager:ShowPanel('CountDownPanel')
    countDownPanel.panelObj:GetComponent('CountDownPanel'):StartCountDown()
end