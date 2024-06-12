Map = Object:SubClass('Map')

Map.comp = nil

function Map.Init(self, mapData)
    self.comp = BossGameManager.gameObjectModel:GetObject('Prefabs/Map'):GetComponent('Map')
    self.comp:Init(mapData)
end
