Monster = Object:SubClass('Monster')

Monster.cs = nil
Monster.mono = nil
Monster.obj = nil

Monster.data = nil
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

function Monster:Construct(args)
    self.obj = args[1]

    self.cs = LuaMonster()
    self.mono = self.obj:AddComponent(typeof(MonoScript))
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
    self.cs.onSetGrowthAction = function()
        return self:SetGrowth()
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
    self.cs.onInitAction = function(cellList)
        self:Init(cellList)
    end
    self.cs.onSetSpeedAction = function(v)
        self:SetSpeed(v)
    end
    self.cs.onGetSignFatherAction = function()
        return self:GetSignFather()
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

-- 移动
function Monster:Move()
    -- 暂停或死亡禁止移动
    if BossGameManager.isPause == true or self.isDead == true then
        return
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
end

function Monster:OnPush()
end

function Monster:OnGet()
end

function Monster:SetSpeed(v)
    self.speed = v
end

function Monster:GetSignFather()
    return self.signFather
end
