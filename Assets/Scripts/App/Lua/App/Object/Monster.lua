Monster = Object:SubClass('Monster')

Monster.cs = {}
Monster.mono = {}
Monster.obj = {}
Monster.gameManager = nil

Monster.data = null
Monster.hp = 0
Monster.isDead = false
Monster.growth = 1
Monster.speed = 0

Monster.nextCell = nil
Monster.pathIndex = 0
Monster.pathList = nil
-- 血条
Monster.hpBg = nil
Monster.hpFg = nil
Monster.lastWoundTime = 0
-- 集火标志
Monster.signFather = nil
Monster.animator = nil
Monster.canMove = false
Monster.buffsList = nil

function Monster:Construct(monsterObj)
    self.gameManager = GameManager.Instance
    self.buffsList = List:New()
    self.obj = monsterObj

    if self.obj:GetComponent(typeof(LuaMonster)) == nil then
        self.cs = self.obj:AddComponent(typeof(LuaMonster))
    else
        self.cs = self.obj:GetComponent(typeof(LuaMonster))
    end

    if self.obj:GetComponent(typeof(MonoScript)) == nil then
        self.mono = self.obj:AddComponent(typeof(MonoScript))
    else
        self.mono = self.obj:GetComponent(typeof(MonoScript))
    end

    self:InitCsAction()

    self.animator = self.obj:GetComponent('Animator')
    self.hpBg = self.obj.transform:Find('HpHolder')
    self.hpFg = self.obj.transform:Find('HpHolder/HpSlider')
    self.signFather = self.obj.transform:Find('SignFather')

    -- 默认隐藏血条
    self.hpBg.gameObject:SetActive(false)
end

function Monster:InitCsAction()
    self.cs.onGetHpAction = function()
        return self:GetHp()
    end
    self.cs.onSetHpAction = function(v)
        self:SetHP(v)
    end
    self.cs.onGetGrowthAction = function()
        return self:GetGrowth()
    end
    self.cs.onSetGrowthAction = function(v)
        return self:SetGrowth(v)
    end
    self.cs.onGetIsDeadAction = function()
        return self:GetIsDead()
    end
    self.cs.onSetIsDeadAction = function(isDead)
        self:SetIsDead(isDead)
    end
    self.cs.onGetDataAction = function()
        return self:GetData()
    end
    self.cs.onGetTransformAction = function()
        return self:GetTransfrom()
    end
    self.cs.onWoundAction = function(v)
        self:Wound(v)
    end
    self.cs.onPushAction = function()
        self:OnPush()
    end
    self.cs.onGetAction = function()
        self:OnGet()
    end
    self.cs.onInitAction = function(cellList, hard, data)
        self:Init(cellList, hard, data)
    end
    self.cs.onSetSpeedAction = function(v)
        self:SetSpeed(v)
    end
    self.cs.onGetSignFatherAction = function()
        return self:GetSignFather()
    end
    self.cs.onDeadAction = function()
        self:Dead()
    end
    self.cs.onAddBuffEffectAction = function(buffEffct)
        self:AddBuffEffect(buffEffct)
    end
    self.cs.onMouseDownAction = function()
        self:OnMouseDown()
    end

    -- unity 回调
    self.mono.onUpdateAction = function()
        self:Update()
    end
end

-- unity回调
function Monster:Update()
    if self.canMove == true then
        self:Move()
    end
    -- 自动隐藏血条
    if Time.time - self.lastWoundTime > 2 or self.isDead == true then
        self.hpBg.gameObject:SetActive(false)
    end
end

function Monster:OnMouseDown()
    if self.isDead == true then
        return
    end

    local gr = self.gameManager.uiManager.canvas:GetComponent('GraphicRaycaster')
    local eventData = CS.UnityEngine.EventSystems.PointerEventData(CS.UnityEngine.EventSystems.EventSystem.current)
    eventData.position = Input.mousePosition
    local results = CS.System.Collections.Generic.List(CS.UnityEngine.EventSystems.RaycastResult)()
    gr:Raycast(eventData, results)
    if results.Count > 0 and results[0].gameObject.name ~= 'ImageAttackRange' then
        return
    end

    self.gameManager.sceneManager:SetFireTarget(self.cs)
end

-- 移动
function Monster:Move()
    -- 暂停或死亡禁止移动
    if self.gameManager.sceneManager:IsPause() == true or self.isDead == true then
        return
    end
    if self.nextCell == nil then
        print(self.isDead)
    end

    -- 判断是否到达格子
    local distance = Vector3.Distance(CSMap.GetCellCenterPos(self.nextCell), self.obj.transform.position)
    if distance < 0.1 and self.isDead == false then
        -- 下一个格子
        self.pathIndex = self.pathIndex + 1
        if self.pathIndex > self.pathList.Count - 1 then
            -- 到达终点重新循环
            self.pathIndex = 0
        end
        self.nextCell = self.pathList[self.pathIndex]
    end

    local dir = CSMap.GetCellCenterPos(self.nextCell) - self.obj.transform.position
    dir:Normalize()
    self.obj.transform:Translate(dir * Time.deltaTime * self.speed)
end

-- 重写
function Monster:Init(cellList, hard, data)
    self.data = data
    self.growth = hard
    self.pathList = cellList

    self.hp = self.data.maxHp * self.growth
    self.speed = self.data.speed
    -- 设置在开头
    self.obj.transform.position = CSMap.GetCellCenterPos(self.pathList[0])
    self.pathIndex = 0
    self.nextCell = self.pathList[0]
    self.canMove = true
    self.isDead = false
end

function Monster:GetHp()
    return self.hp
end

function Monster:SetHP(v)
    self.hp = v
end

function Monster:GetGrowth()
    return self.growth
end

function Monster:SetGrowth(v)
    self.growth = v
end

function Monster:GetIsDead()
    return self.isDead
end

function Monster:SetIsDead(isDead)
    self.isDead = isDead
end

function Monster:GetData()
    return self.data
end

function Monster:GetTransfrom()
    return self.obj.transform
end

function Monster:Wound(v)
    self.hp = self.hp - v
    -- 更新血条
    self.hpBg.gameObject:SetActive(true)
    local maxHp = self.data.maxHp * self.growth
    self.hpFg.localScale = Vector3(self.hp / maxHp, 1, 1)
    self.lastWoundTime = Time.time

    if self.hp <= 0 then
        self.hp = 0
        self.isDead = true
        self.gameManager.sceneManager:UpdateMoney(math.floor(self.data.baseMoney * self.growth))
        -- 加钱ui
        local ui =
            GameManager.Instance.factoryManager.UIControlFactory:CreateControl('AddMoneyTips'):GetComponent(
            'AddMoneyTips'
        )
        ui.textMeshPro.text = '+' .. math.floor(self.data.baseMoney * self.growth)
        ui.transform.position = self.mono.transform.position
        CS.DG.Tweening.ShortcutExtensions.DOMoveY(ui.transform, ui.transform.position.y + 2, 0.5)
        -- 移除buff
        self:ClearAllBuffs()

        if self.gameManager.sceneManager.Spawner:GetCollectingFiresTarget() == self.cs then
            self.gameManager.sceneManager:CancelFire()
        end
        -- 触发死亡动画
        self.animator:SetBool('Dead', true)
    end
end

function Monster:ClearAllBuffs()
    for i = 0, self.buffsList.count - 1 do
        self.gameManager.poolManager:PushObject(self.buffsList:Get(i).gameObject)
    end
    self.gameManager.buffManager:RemoveAllBuffs(self.cs)
    self.buffsList:Clear()
end

function Monster:AddBuffEffect(buffEffct)
    self.buffsList:Add(buffEffct)
end

function Monster:Dead()
    self.gameManager.poolManager:PushObject(self.mono.gameObject)
    self.gameManager.eventCenter:TriggerEvent('JudgeWin')
    self.gameManager.sceneManager:UpdateKillMonsterCount(1)
end

function Monster:OnPush()
    self.nextCell = nil
    self.animator:SetBool('Dead', false)
end

function Monster:OnGet()
    self.pathIndex = 0
end

function Monster:SetSpeed(v)
    self.speed = v
end

function Monster:GetSignFather()
    return self.signFather
end
