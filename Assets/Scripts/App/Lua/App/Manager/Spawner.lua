Spawner = Object:SubClass('Spawner')

Spawner.csScript = nil
Spawner.mono = nil

Spawner.monstersObjList = {}
Spawner.towersObjList = {}
Spawner.obstacelsObjList = {}
Spawner.collectingFireTarget = nil
Spawner.sign = nil

Spawner.pathList = {}
Spawner.obstacleList = {}

Spawner.complete = false
Spawner.isStarted = false
Spawner.isPaused = true
Spawner.isWaveInProgress = false
Spawner.currentWaveIndex = 0
Spawner.currentWaveMonsterIndex = 0
Spawner.waveTimer = 0
Spawner.monsterSpawnTimer = 0
Spawner.waveDataList = {}
Spawner.nowWaveSpawnList = {}

function Spawner.Init(self, mapData)
    self.mapData = mapData

    -- 读取格子信息
    self.pathList = PointClassToCell.ToCellList(self.mapData.pathList)
    self.waveDataList = self.mapData.waveDataList

    for i = 0, self.mapData.obstacleList.Count - 1 do
        local cell = Cell(Point(self.mapData.obstacleList[i].x, self.mapData.obstacleList[i].y))
        cell.obstacleName = self.mapData.obstacleList[i].obstacleType:ToString()
        self.obstacleList[i] = cell
    end

    -- 生成
    self:CreateObstacle()

    -- 移除C#组件
    local comp = BossGameManager.gameObjectModel:GetObject('Prefabs/Spawner'):GetComponent('Spawner')
    Destroy(comp)
    -- lua cs
    self.csScript = LuaSpawner()
    self:InitCsScript()
    -- 添加MonoScript
    self.mono = self.comp.gameObject:AddComponent(typeof(MonoScript))
    self:InitMonoScript()
end

function Spawner.InitCsScript(self)
    
end

function Spawner.GetCollectingFiresTarget(self)
    return self.collectingFireTarget
end

function Spawner.GetAllMonsters(self)
    -- 创建cs列表
    local l = CS.System.Collections.Generic.List(CS.System.Object)()

    return self.monstersObjList
end

function Spawner.InitMonoScript(self)
    self.mono.onUpdateAction = function()
        self:Update() 
    end
end

function Spawner.Update(self)
    if self.isStarted == false or self.isPaused == true then
        return
    end

    if self.currentWaveIndex < self.waveDataList.Count then
        if self.isWaveInProgress == false and self.waveTimer >= self.waveDataList[self.currentWaveIndex].waveDuratiom then
            self:StartNewWave()
        end

        if self.currentWaveMonsterIndex >= self.nowWaveSpawnList.Count then
            self.waveTimer = self.waveTimer + Time.deltaTime
            if self.isWaveInProgress then
                self:EndCurrentWave()
            end
        else
            self:HandleMonsterSpawning()
        end
    end
end

function Spawner.StartSpawn(self)
    self.isStarted = true
    self.isPaused = false
    self.isWaveInProgress = false
    self.currentWaveIndex = 0
    self.currentWaveMonsterIndex = 0
    self.waveTimer = 0
    self.monsterSpawnTimer = 0
end

function Spawner.StartNewWave(self)
    self.isWaveInProgress = true
    self.waveTimer = 0
    self.currentWaveMonsterIndex = 0
    self.monsterSpawnTimer = 0

    self.nowWaveSpawnList = {}
    for i = 0, self.waveDataList[self.currentWaveIndex].eachWaveDataList.Count - 1 do
        local e = self.waveDataList[self.currentWaveIndex].eachWaveDataList[i]
        for j = 0, e.monsterCount - 1 do
            local d = SpawnMonsterData:New()
            d.monsterType = e.monsterType
            d.hard = e.hard
            d.nextSpawnTime = e.monsterDuration
            table.insert(self.nowWaveSpawnList, d)
        end
    end
end

function Spawner.PauseSpawn(self)
    self.isPaused = true
end

function Spawner.ResumSpaw(self)
    self.isPaused = false
end

function Spawner.EndCurrentWave(self)
    self.isWaveInProgress = false
    self.currentWaveIndex = self.currentWaveIndex + 1
end

function Spawner.HandleMonsterSpawning(self)
    if self.currentWaveIndex == 0 and self.currentWaveMonsterIndex == 0 then
        self:SpawnMonster()

    end
end

function Spawner.SpawnMonster(self, type, hard)
    local monster = BossGameManager.gameObjectModel:GetObject('Prefabs/Monster')
    -- 移除C#组件
    Destroy(monster:GetComponent('monster'))
    -- 添加Lua组件
    local monsterComp = monster:AddComponent(typeof(MonoScript))
end

function Spawner.CreateObstacle(self)
end

function Spawner.SetCollectingFires(self)
end
