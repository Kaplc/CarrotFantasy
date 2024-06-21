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

function Spawner.InitLua(self, mapData)
    self.gameManager = GameManager.Instance

    local obj = Instantiate(Resources.Load('Prefabs/Spawner'))
    Destroy(obj:GetComponent('Spawner'))
    self.script = obj:AddComponent(typeof(LuaSpawner))
    self.monoScript = obj:AddComponent(typeof(MonoScript))

    self:InitAction()

    self.script:Init(mapData)
end

function Spawner.InitAction(self)
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
        self:SetCollectingFires()
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

function Spawner.Update(self)
    if self.isStarted == false or self.isPaused == true then
        return
    end

    if self.currentWaveIndex < self.waveDataList.Count then
        if self.isWaveInProgress == true and self.waveTimer >= self.waveDataList[self.currentWaveIndex].waveDuration then
            self:StartNewWave()
        end

        if self.currentWaveMonsterIndex >= self.nowWaveSpawnList.Count then
            self.waveTimer = self.waveTimer + Time.deltaTime
            if self.isWaveInProgress == true then
                self:EndCurremtWave()
            end
        else
            self:HandleMonsterSpawning()
        end
    end
end

function Spawner.Init(self, mapData)
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
function Spawner.StartNewWave(self)
    self.isWaveInProgress = true
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0

    self.nowWaveSpawnList.Clear()

    local list = self.waveDataList[self.currentWaveIndex].eachWaveDataList
    for i = 0, e.Count - 1 do
        local e = list[0]
        for j = 0, e.monsterCount - 1 do
            local d = SpawnMonsterData()
            d.monsterType = e.monsterType
            d.nextSpawnTime = e.monsterDuration
            d.hard = e.hard
            self.nowWaveSpawnList[0] = d
        end
    end
end

function Spawner.EndCurrentWave(self)
    self.isWaveInProgress = false
    self.currentWaveIndex = self.currentWaveIndex + 1
end

function Spawner.HandleMonsterSpawning(self)
    if self.currentWaveIndex == 0 and self.currentWaveMonsterIndex == 0 then
        local type = self.nowWaveSpawnList[self.currentWaveMonsterIndex].monsterType
        local hard = self.nowWaveSpawnList[self.currentWaveMonsterIndex].hard
        self:SpawnerMonster(type, hard)
        self.currentWaveMonsterIndex = self.currentWaveMonsterIndex + 1
        self.monsterSpawnTimer = 0
    else
        self.monsterSpawnTimer  = self.monsterSpawnTimer + Time.deltaTime
        if self.monsterSpawnTimer >= self.nowWaveSpawnList[self.currentWaveMonsterIndex] then
            local type = self.nowWaveSpawnList[self.currentWaveMonsterIndex].monsterType
            local hard = self.nowWaveSpawnList[self.currentWaveMonsterIndex].hard
            self:SpawnerMonster(type, hard)
            self.currentWaveMonsterIndex = self.currentWaveMonsterIndex + 1
            self.monsterSpawnTimer = 0
        end
    end
end

function Spawner.SpawnerMonster(self, type, hard)
    local monster = self.gameManager.poolManage:GetObject('Object/Monster/' .. type):GetComponent('Monster')
    monster.transform:SetParent(self.script.transform)
    monster.transform.localScale = Vector3.one
    monster.transform.position = CSMap.GetCellCenterPos(self.pathList[0])
    monster.data.maxHp = monster.data.maxHp * hard
    monster:Init(self.pathList)
    monster:Add(monster)
end

function Spawner.StartSpawn(self)
    self.isStarted = true
    self.isPaused = false
    self.currentWaveIndex = 0
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0
    self.isWaveInProgress = false
end

-- 塔升级出售
function Spawner.UpGradeTower(self)
    
end

function Spawner.SellTower(self)
    
end



function Spawner.CreateObstacles(self)

    for i = 0, self.obstacleList.Count -1 do
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

function Spawner.CreateTowerObject(self)
   
end

-- 集火
function Spawner.SetCollectingFires(monster)
    
end
function Spawner.GetCollectingFiresTarget(self)
    self.script.base:GetCollectingFiresTarget()
end

-- 缓存池
function Spawner.OnPushAllGameObject(self)
    self.signTrf.gameObject.SetActive(false)

end

function Spawner.OnPushAllMonsters(self)
    for i = 0, self.spawnedMonsterList.Count - 1 do
        if self.spawnedMonsterList[i].IsDead == true then
            self.gameManager.poolManager:PushObject(self.spawnedMonsterList[i].Transform.gameObject)
        end
    end
    self.spawnedMonsterList:Clear()
end

function Spawner.OnPushAllTowers(self)
    for i = 0, self.spawnedTowerList.Count - 1 do
        self.gameManager.poolManager:PushObject(self.spawnedTowerList[i].Transform.gameObject)
    end
    self.spawnedTowerList:Clear()
end

function Spawner.OnPushAllObstacles(self)
    for i = 0, self.spawnedObstacleList.Count - 1 do
        self.gameManager.poolManager:PushObject(self.spawnedObstacleList[i].Transform.gameObject)
    end
    self.spawnedObstacleList:Clear()
end
