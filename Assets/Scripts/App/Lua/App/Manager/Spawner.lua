require('App/Generic/SpawnMonsterData')
require('App/Object/Monster')

Spawner = Object:SubClass('Spawner')

Spawner.script = nil
Spawner.monoScript = nil
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

Spawner.spawnedMonsterList = {}
Spawner.spawnedTowerList = {}
Spawner.spawnedObstacleList = {}

Spawner.nowWaveSpawnList = {}

function Spawner:Construct()
    self.gameManager = GameManager.Instance

    local obj = Instantiate(Resources.Load('Prefabs/Spawner'))
    Destroy(obj:GetComponent('Spawner'))
    self.script = obj:AddComponent(typeof(LuaSpawner))
    self.monoScript = obj:AddComponent(typeof(MonoScript))

    self:InitAction()

    self.nowWaveSpawnList = List:New()
    self.spawnedMonsterList = List:New()
    self.spawnedTowerList = List:New()
    self.spawnedObstacleList = List:New()
end

function Spawner.InitLua(self, mapData)
    self.script:Init(mapData)
end

function Spawner:InitAction()
    -- monoScript
    self.monoScript.onUpdateAction = function()
        self:Update()
    end
    -- LuaSpawner
    self.script.onGetCarrotAction = function()
        return self:GetCarrot()
    end
    self.script.onPushAllGameObjectAction = function()
        self:PushAllGameObject()
    end
    self.script.onWinJudgeAction = function()
        return self:WinJudge()
    end
    self.script.onPauseWavesAction = function()
        self:PauseWaves()
    end
    self.script.onResumeWavesAction = function()
        self:ResumeWaves()
    end
    self.script.onStartSpawnAction = function()
        self:StartSpawn()
    end
    self.script.onGetAllMonstersAction = function()
        return self:GetAllMonsters()
    end
    self.script.onGetCollectingFiresTargetAction = function()
        return self:GetCollectingFiresTarget()
    end
    self.script.onSetCollectingFiresAction = function(m)
        self:SetCollectingFires(m)
    end
    self.script.onCancelCollectingFiresTargetAction = function()
        self:CancelCollectingFiresTarget()
    end
    self.script.onInitAction = function(mapData)
        self:Init(mapData)
    end
    self.script.onCreateTowerObjectAction = function(towerData, v3)
        self:CreateTowerObject(towerData, v3)
    end
    self.script.onUpGradeTowerAction = function(v3)
        self:UpGradeTower(v3)
    end
    self.script.onSellTowerAction = function(v3)
        self:SellTower(v3)
    end
    self.script.onGetNowWaveCountAction = function()
        return self:GetNowWaveCount()
    end
end

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

    self.sceneManager = self.gameManager.sceneManager

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
    local prefabs = Resources.Load('Object/Monster/' .. type:ToString())
    if prefabs == nil then
        prefabs = Resources.Load('AB/Monster/' .. type:ToString())
    end
    local obj = Instantiate(prefabs)
    DestroyImmediate(obj:GetComponent('Monster'), true)
    -- 实例化并进行lua初始化
    local monster = Monster:New(obj)
    -- 设置位置
    monster.obj.transform:SetParent(self.script.transform)
    monster.obj.transform.localScale = Vector3.one
    -- 添加进列表
    self.spawnedMonsterList:Add(monster)
    -- 加载怪物数据
    local monsterDataMap = Resources.Load('Data/Monster/MonsterDataMap')
    if monsterData == nil then
        monsterData = Resources.Load('AB/Data/')
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
function Spawner:UpGradeTower()
end

function Spawner:SellTower()
end

function Spawner:CreateObstacles()
    for i = 0, self.obstacleList.Count - 1 do
        if self.obstacleList[i].obstacleName ~= 'None' then
            local cell = self.obstacleList[i]
            local obj = self.gameManager.poolManager:GetObject('Object/Obstacle/' .. cell.obstacleName)
            obj.transform:SetParent(self.script.transform)
            obj.transform.localScale = Vector3.one
            obj.transform.position = CSMap.GetCellCenterPos(cell)
            cell.obstacle = obj

            self.spawnedObstacleList[i] = obj:GetComponent('Obstacle')
        end
    end
end

function Spawner:CreateTowerObject()
end

-- 集火
function Spawner:SetCollectingFires(monster)
    self.fireTarget = monster
end
function Spawner:GetCollectingFiresTarget()
    return self.fireTarget
end

function Spawner:CancelCollectingFiresTarget()
    self.fireTarget = nil
end

-- 缓存池
function Spawner:OnPushAllGameObject()
    self.signTrf.gameObject.SetActive(false)
end

function Spawner:OnPushAllMonsters()
    for i = 0, self.spawnedMonsterList.Count - 1 do
        if self.spawnedMonsterList[i].IsDead == true then
            self.gameManager.poolManager:PushObject(self.spawnedMonsterList[i].Transform.gameObject)
        end
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
