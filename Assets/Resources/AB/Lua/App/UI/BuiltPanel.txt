BuiltPanel = BasePanel:SubClass('BuiltPanel')

BuiltPanel.mono = nil

BuiltPanel.cantBuiltIconRect = nil
BuiltPanel.cantBuitltIconShowTime = nil

BuiltPanel.createPanel = nil
BuiltPanel.createTowerIconsRect = nil

BuiltPanel.upgradePanel = nil
BuiltPanel.btnUpgrade = nil
BuiltPanel.txUpgradeMoney = nil
BuiltPanel.btnSell = nil
BuiltPanel.txSellMoney = nil
BuiltPanel.imgAttackRange = nil

function BuiltPanel.Init(self)
    -- 移除C#组件
    Destroy(self.panelObj:GetComponent('BuiltPanel'))
    -- 添加MonoScript
    self.mono = self.panelObj:AddComponent(typeof(MonoScript))
    self.mono.onUpdateAction = function()
        self:Update()
    end

    -- 查找组件
    self.cantBuiltIconRect = self.panelObj.transform:Find('CantBuiltIcon'):GetComponent('RectTransform')

    self.createPanel = self.panelObj.transform:Find('CreatePanel').gameObject
    self.createTowerIconsRect = self.panelObj.transform:Find('CreatePanel/TowerIcons'):GetComponent('RectTransform')

    self.upgradePanel = self.panelObj.transform:Find('UpGradePanel').gameObject
    self.btnUpgrade = self.panelObj.transform:Find('UpGradePanel/ButtonUpGrade'):GetComponent('Button')
    self.txUpgradeMoney = self.panelObj.transform:Find('UpGradePanel/ButtonUpGrade/TextMoney'):GetComponent('Text')
    self.btnSell = self.panelObj.transform:Find('UpGradePanel/ButtonSell'):GetComponent('Button')
    self.txSellMoney = self.panelObj.transform:Find('UpGradePanel/ButtonSell/TextMoney'):GetComponent('Text')
    self.imgAttackRange = self.panelObj.transform:Find('UpGradePanel/ImageAttackRange'):GetComponent('Image')

    self.createPanel:SetActive(false)
    self.upgradePanel:SetActive(false)
    self.cantBuiltIconRect.gameObject:SetActive(false)
end

function BuiltPanel.Update(self)
    self.cantBuitltIconShowTime = self.cantBuitltIconShowTime - Time.deltaTime

    if self.cantBuitltIconShowTime <= 0 then
        self.cantBuiltIconRect.gameObject:SetActive(false)
    end
end

function BuiltPanel.ShowCantBuilt(self, pos)
    self.createPanel:SetActive(false)
    self.upgradePanel:SetActive(false)

    self.cantBuiltIconRect.gameObject:SetActive(true)
    self.cantBuitltIconShowTime = 1
    local p = self:WorldPosToUIPos(pos)
    print(p)
    self.cantBuiltIconRect.anchoredPosition = Vector2(p.x, p.y)
    print(self.cantBuiltIconRect.anchoredPosition)
end

function BuiltPanel.ShowCreateTower(self, pos, towerDataList)
    self.upgradePanel:SetActive(false)
    self.cantBuiltIconRect.gameObject:SetActive(false)

    self.createPanel:SetActive(true)
    self.createPanel:GetComponent('RectTransform').anchoredPosition = self:WorldPosToUIPos(pos)

    for k, v in pairs(towerDataList) do
    end
end

function BuiltPanel.ShowUpgradeTower(self, pos)
    self.createPanel:SetActive(false)
    self.cantBuiltIconRect.gameObject:SetActive(false)

    self.upgradePanel:SetActive(true)

    self.imgAttackRange.rectTransform.sizeDelta = Vector2(self.panelObj.attackRange * 2, self.panelObj.attackRange * 2)
end

function BuiltPanel.WorldPosToUIPos(self, pos)
    local vp = CS.UIManager.Instance.uiCamera:WorldToViewportPoint(pos)
    local sp = CS.UIManager.Instance.uiCamera:ViewportToScreenPoint(vp)
    print(sp)
    local rect = self.panelObj.transform:GetComponent('RectTransform')
    local uiPos = Vector2(0, 0)
    local success, o1, o2 =
        CS.UnityEngine.RectTransformUtility.ScreenPointToWorldPointInRectangle(
        rect,
        sp,
        CS.UIManager.Instance.uiCamera,
        uiPos
    )
    if success then
        return o1
    else
        return nil
    end
end
