SpawnMonsterData = Object:SubClass('SpawnMonsterData')

SpawnMonsterData.monsterType = nil
SpawnMonsterData.nextSpawnTime = 0
SpawnMonsterData.hard = 0

function SpawnMonsterData:Construct(type, time, hard)
    self.monsterType = type
    self.nextSpawnTime = time
    self.hard = hard
end