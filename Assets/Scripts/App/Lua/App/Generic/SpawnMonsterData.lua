SpawnMonsterData = Object:SubClass('SpawnMonsterData')

SpawnMonsterData.monsterType = nil
SpawnMonsterData.nextSpawnTime = 0
SpawnMonsterData.hard = 0

function SpawnMonsterData:Construct(args)
    self.monsterType = args[1]
    self.nextSpawnTime = args[2]
    self.hard = args[3]
end