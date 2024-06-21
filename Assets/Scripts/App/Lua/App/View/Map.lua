Map = Object:SubClass('Map')

Map.script = nil

function Map.InitLua(self, mapData)
    self.script = Instantiate(Resources.Load('Prefabs/Map')):GetComponent('Map')
    self.script:Init(mapData)
end
