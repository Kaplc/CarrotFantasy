require('App/Generic/SpawnMonsterData')
require('App/Object/Monster')
require('App/Generic/PrizeCell')
require('App/Generic/ObstaclePrize')
require('App/Object/BossMonster')

Spawner = Object:SubClass('Spawner')

Spawner.cs = nil
Spawner.mono = nil
Spawner.sceneManager = nil
Spawner.gameManager = nil

Spawner.fireTarget = nil
Spawner.signTrf = nil

Spawner.spawnedComplete = false
Spawner.isWaveInProgress = false
Spawner.isStarted = false
Spawner.isPaused = true
Spawner.currentWaveIndex = 0
Spawner.currentWaveMonsterIndex = 0
Spawner.waveTimer = 0
Spawner.monsterSpawnTimer = 0

Spawner.pathList = nil
Spawner.waveDataList = nil
Spawner.obstacleList = nil
Spawner.prizeTowerList = nil

Spawner.obstaclePrizeList = nil

Spawner.spawnedMonsterList = nil
Spawner.spawnedTowerList = nil
Spawner.spawnedObstacleList = nil
Spawner.spawnedBoss = nil

Spawner.nowWaveSpawnList = nil

function Spawner:Construct()
    self.gameManager = GameManager.cs
    self.sceneManager = GameManager.cs.sceneManager

    local obj = Instantiate(Resources.Load('Prefabs/Spawner'))
    Destroy(obj:GetComponent('Spawner'))
    self.cs = LuaSpawner()
    self.mono = obj:AddComponent(typeof(MonoScript))

    self:InitAction()

    self.nowWaveSpawnList = List()
    self.spawnedMonsterList = CS.System.Collections.Generic.List(IMonster)()
    self.spawnedTowerList = CS.System.Collections.Generic.List(ITower)()
    self.spawnedObstacleList = CS.System.Collections.Generic.List(IObstacle)()
    self.obstaclePrizeList = List()

    self.signTrf = self.mono.transform:Find('CollectingFiresSign')
end

function Spawner:InitAction()
    -- monoScript
    self.mono.onUpdateAction = function()
        self:Update()
    end
    -- LuaSpawner
    self.cs.onGetCarrotAction = function()
        return self:GetCarrot()
    end
    self.cs.onPushAllGameObjectAction = function()
        self:OnPushAllGameObject()
    end
    self.cs.onWinJudgeAction = function()
        return self:WinJudge()
    end
    self.cs.onPauseWavesAction = function()
        self:PauseWaves()
    end
    self.cs.onResumeWavesAction = function()
        self:ResumeWaves()
    end
    self.cs.onStartSpawnAction = function()
        self:StartSpawn()
    end
    self.cs.onGetAllMonstersAction = function()
        return self:GetAllMonsters()
    end
    self.cs.onGetCollectingFiresTargetAction = function()
        return self:GetCollectingFiresTarget()
    end
    self.cs.onSetCollectingFiresAction = function(m)
        self:SetCollectingFires(m)
    end
    self.cs.onCancelCollectingFiresTargetAction = function()
        self:CancelCollectingFiresTarget()
    end
    self.cs.onInitAction = function(mapData)
        self:Init(mapData)
    end
    self.cs.onCreateTowerObjectAction = function(towerData, v3)
        self:CreateTowerObject(towerData, v3)
    end
    self.cs.onUpGradeTowerAction = function(v3)
        self:UpGradeTower(v3)
    end
    self.cs.onSellTowerAction = function(v3)
        self:SellTower(v3)
    end
    self.cs.onGetNowWaveCountAction = function()
        return self:GetNowWaveCount()
    end
end

-- 外部获取变量
function Spawner:GetCarrot()
    return nil
end

function Spawner:GetNowWaveCount()
    return 0
end

function Spawner:GetAllMonsters()
    return self.spawnedMonsterList
end

-- unity回调
function Spawner:Update()
    if self.isStarted == false or self.isPaused == true then
        return
    end

    if self.currentWaveIndex < self.waveDataList.Count then
        if self.isWaveInProgress == false and self.waveTimer >= self.waveDataList[self.currentWaveIndex].waveDuration then
            self:StartNewWave()
        end

        if self.currentWaveMonsterIndex >= self.nowWaveSpawnList.count then
            self.waveTimer = self.waveTimer + Time.deltaTime
            if self.isWaveInProgress == true then
                self:EndCurrentWave()
            end
        else
            self:HandleMonsterSpawning()
        end
    else
        self.spawnedComplete = true
        if self.spawnedBoss == nil then
            self:CreateBossMonster()
        end
    end

    for i = 0, self.obstaclePrizeList.count - 1 do
        local obstaclePrize = self.obstaclePrizeList:Get(i)
        if obstaclePrize.obstacle.IsDead == true and obstaclePrize:HasPrize() == true then
            -- 生成奖励
            self:CreatePrize(obstaclePrize)
            self.obstaclePrizeList:DelayRemove(obstaclePrize)
        end
    end
    self.obstaclePrizeList:StartRemove()
end

function Spawner:CreateBossMonster()
    local bossObj =
        Instantiate(Resources.Load('AB/Object/Boss/BossMonster' .. GameManager.sceneManager:GetLevelID() - 1))
    local boss = BossMonster(bossObj)

    boss.obj.transform:SetParent(self.cs.transform)
    boss.obj.transform.localScale = Vector3.one
    -- 添加进列表
    self.spawnedMonsterList:Add(boss.cs)
    self.spawnedBoss = boss
    -- 加载怪物数据
    local monsterData = Resources.Load('AB/Data/BossMonsterData/Boss' .. GameManager.sceneManager:GetLevelID() - 1)
    boss:Init(self.pathList, 1, monsterData)
end

function Spawner:CreatePrize(obstaclePrize)
    for i = 0, obstaclePrize.prizeCellList.count - 1 do
        local prizeCell = obstaclePrize.prizeCellList:Get(i)

        -- 加载towerData
        local towerDataMap = Resources.Load('Data/Tower/TowerMap')
        local towerData = towerDataMap:GetData(prizeCell.prizeTowerType)
        local pos = CSMap.GetCellCenterPos(prizeCell.cell)
        local towerObj = self.gameManager.poolManager:GetObject(towerData.prefabsPath)
        local tower = towerObj:GetComponent(typeof(ITower))
        towerObj.transform:SetParent(self.mono.transform)
        towerObj.transform.localScale = Vector3.one
        towerObj.transform.position = pos

        CSMap.GetCell(pos).tower = tower
        self.spawnedTowerList:Add(tower)
    end
end

function Spawner:Init(mapData)
    self.spawnedComplete = false
    self.isWaveInProgress = false
    self.isStarted = false
    self.isPaused = true
    self.currentWaveIndex = 0
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0

    self.pathList = mapData:GetPathList()
    self.waveDataList = mapData:GetWaveData()
    self.obstacleList = mapData:GetObstacle()
    self.prizeTowerList = mapData.towerList

    self:CreateObstacles()
end

-- 出怪逻辑
function Spawner:StartNewWave()
    self.isWaveInProgress = true
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0

    self.nowWaveSpawnList:Clear()

    local list = self.waveDataList[self.currentWaveIndex].eachWaveDataList
    for i = 0, list.Count - 1 do
        local e = list[i]
        for j = 0, e.monsterCount - 1 do
            local d = SpawnMonsterData:New(e.monsterType, e.monsterDuration, e.hard)
            self.nowWaveSpawnList:Add(d)
        end
    end
end

function Spawner:EndCurrentWave()
    self.isWaveInProgress = false
    self.currentWaveIndex = self.currentWaveIndex + 1
end

function Spawner:HandleMonsterSpawning()
    if self.currentWaveIndex == 0 and self.currentWaveMonsterIndex == 0 then
        local type = self.nowWaveSpawnList:Get(self.currentWaveMonsterIndex).monsterType
        local hard = self.nowWaveSpawnList:Get(self.currentWaveMonsterIndex).hard
        self:SpawnerMonster(type, hard)
        self.currentWaveMonsterIndex = self.currentWaveMonsterIndex + 1
        self.monsterSpawnTimer = 0
    else
        self.monsterSpawnTimer = self.monsterSpawnTimer + Time.deltaTime
        if self.monsterSpawnTimer >= self.nowWaveSpawnList:Get(self.currentWaveMonsterIndex).nextSpawnTime then
            local type = self.nowWaveSpawnList:Get(self.currentWaveMonsterIndex).monsterType
            local hard = self.nowWaveSpawnList:Get(self.currentWaveMonsterIndex).hard
            self:SpawnerMonster(type, hard)
            self.currentWaveMonsterIndex = self.currentWaveMonsterIndex + 1
            self.monsterSpawnTimer = 0
        end
    end
end

function Spawner:SpawnerMonster(type, hard)
    -- 获取预设体
    local monsterObj = self.gameManager.poolManager:GetObject('Object/Monster/' .. type:ToString())
    DestroyImmediate(monsterObj:GetComponent('Monster'), true)
    -- 实例化并进行lua初始化
    local monster = Monster(monsterObj)
    -- 设置位置
    monster.obj.transform:SetParent(self.cs.transform)
    monster.obj.transform.localScale = Vector3.one
    -- 添加进列表
    self.spawnedMonsterList:Add(monster.cs)
    -- 加载怪物数据
    local monsterDataMap = Resources.Load('Data/Monster/MonsterDataMap')
    if monsterDataMap == nil then
        monsterDataMap = Resources.Load('AB/Data/')
    end
    local monsterData = monsterDataMap:GetData(type)
    monster:Init(self.pathList, hard, monsterData)
end

-- 开始、暂停、继续
function Spawner:StartSpawn()
    self.isStarted = true
    self.isPaused = false
    self.currentWaveIndex = 0
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0
    self.isWaveInProgress = false
end

function Spawner:PauseWaves()
    self.isPaused = true
end

function Spawner:ResumeWaves()
    self.isPaused = false
end

-- 塔升级出售
function Spawner:UpGradeTower(pos)
    local tower = CSMap.GetCell(pos).tower
    if tower == nil then
        return
    end
    local level = tower:GetLevel()
    local data = tower:GetData()

    if level == 2 then
        return
    end

    if self.sceneManager:GetMoney() < data.prices[level + 1] then
        return
    end

    tower:UpGrade()

    self.sceneManager:UpdateMoney(-data.prices[level + 1])
    GameFacade.Instance:SendNotification('HIDE_BUILT_PANEL')
end

function Spawner:SellTower(pos)
    local cell = CSMap.GetCell(pos)
    local tower = cell.tower
    if tower == nil then
        return
    end
    local level = tower:GetLevel()
    local data = tower:GetData()

    self.sceneManager:UpdateMoney(data.sellPrices[level])
    -- 回收对象
    self.gameManager.poolManager:PushObject(tower.gameObject)
    self.spawnedTowerList:Remove(tower)
    cell.tower = nil
    GameFacade.Instance:SendNotification('HIDE_BUILT_PANEL')
end

function Spawner:CreateTowerObject(towerData, pos)
    if self.sceneManager:GetMoney() <= towerData.prices[0] then
        return
    end
    local towerObj = self.gameManager.poolManager:GetObject(towerData.prefabsPath)
    local tower = towerObj:GetComponent(typeof(ITower))
    towerObj.transform:SetParent(self.mono.transform)
    towerObj.transform.localScale = Vector3.one
    towerObj.transform.position = pos

    self.sceneManager:UpdateMoney(-towerData.prices[0])
    CSMap.GetCell(pos).tower = tower
    GameFacade.Instance:SendNotification('HIDE_BUILT_PANEL')
    self.spawnedTowerList:Add(tower)
end

function Spawner:CreateObstacles()
    for i = 0, self.obstacleList.Count - 1 do
        local name = self.obstacleList[i].obstacleName
        if name ~= 'None' then
            local cell = self.obstacleList[i]
            local obj = self.gameManager.poolManager:GetObject('Object/Obstacle/' .. cell.obstacleName)
            obj.transform:SetParent(self.cs.transform)
            obj.transform.localScale = Vector3.one
            obj.transform.position = CSMap.GetCellCenterPos(cell)
            cell.obstacle = obj
            self.spawnedObstacleList:Add(obj:GetComponent(typeof(IObstacle)))
            -- 绑定摧毁奖励
            -- 判断是否有奖励在对应方格中
            local obstaclePrize = ObstaclePrize(obj:GetComponent(typeof(IObstacle)))

            if string.find(name, '1X1') then
                -- 1X1
                for i = 0, self.prizeTowerList.Count - 1 do
                    local prizeTower = self.prizeTowerList[i]

                    if prizeTower.x == cell.X and prizeTower.y == cell.Y then
                        local prizeCell = PrizeCell(cell, prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                end
            elseif string.find(name, '1X2') then
                -- 1X2
                for i = 0, self.prizeTowerList.Count - 1 do
                    local prizeTower = self.prizeTowerList[i]

                    if prizeTower.x == cell.X and prizeTower.y == cell.Y then
                        local prizeCell = PrizeCell(cell, prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                    if prizeTower.x == cell.X + 1 and prizeTower.y == cell.Y then
                        local prizeCell = PrizeCell(Cell(Point(cell.X + 1, cell.Y)), prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                end
            elseif string.find(name, '4X4') then
                -- 4X4
                for i = 0, self.prizeTowerList.Count - 1 do
                    local prizeTower = self.prizeTowerList[i]

                    if prizeTower.x == cell.X and prizeTower.y == cell.Y then
                        local prizeCell = PrizeCell(cell, prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                    if prizeTower.x == cell.X + 1 and prizeTower.y == cell.Y then
                        local prizeCell = PrizeCell(Cell(Point(cell.X + 1, cell.Y)), prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                    if prizeTower.x == cell.X and prizeTower.y == cell.Y + 1 then
                        local prizeCell = PrizeCell(Cell(Point(cell.X, cell.Y + 1)), prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                    if prizeTower.x == cell.X + 1 and prizeTower.y == cell.Y + 1 then
                        local prizeCell = PrizeCell(Cell(Point(cell.X + 1, cell.Y + 1)), prizeTower.towerType)
                        obstaclePrize:AddPrizeCell(prizeCell)
                    end
                end
            end

            self.obstaclePrizeList:Add(obstaclePrize)
        end
    end
end

-- 集火
function Spawner:SetCollectingFires(monster)
    self.fireTarget = monster
    self.signTrf.gameObject:SetActive(true)
    self.signTrf:SetParent(monster:GetSignFather())
    self.signTrf.localPosition = Vector3.zero
    self.signTrf.localScale = Vector3.one
end
function Spawner:GetCollectingFiresTarget()
    return self.fireTarget
end

function Spawner:CancelCollectingFiresTarget()
    self.fireTarget = nil
    self.signTrf.gameObject:SetActive(false)
    self.signTrf:SetParent(self.mono.transform)
end

-- 缓存池
function Spawner:OnPushAllGameObject()
    self.obstaclePrizeList:Clear()

    self.signTrf.gameObject:SetActive(false)
    self:OnPushAllMonsters()
    self:OnPushAllObstacles()
    self:OnPushAllTowers()
end

function Spawner:OnPushAllMonsters()
    for i = 0, self.spawnedMonsterList.Count - 1 do
        self.gameManager.poolManager:PushObject(self.spawnedMonsterList[i].Transform.gameObject)
    end
    self.spawnedMonsterList:Clear()
end

function Spawner:OnPushAllTowers()
    for i = 0, self.spawnedTowerList.Count - 1 do
        self.gameManager.poolManager:PushObject(self.spawnedTowerList[i].Transform.gameObject)
    end
    self.spawnedTowerList:Clear()
end

function Spawner:OnPushAllObstacles()
    for i = 0, self.spawnedObstacleList.Count - 1 do
        self.gameManager.poolManager:PushObject(self.spawnedObstacleList[i].Transform.gameObject)
    end
    self.spawnedObstacleList:Clear()
end

-- Win
function Spawner:WinJudge()
    -- 出怪完成
    if self.spawnedComplete == false then
        return false
    end

    -- 所有怪物死亡
    for i = 0, self.spawnedMonsterList.Count - 1 do
        local m = self.spawnedMonsterList[i]
        if m.IsDead == false then
            return false
        end
    end

    return true
end
