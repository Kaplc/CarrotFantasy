Map = Object:SubClass('Map')

Map.script = nil

function Map:Construct()
    self.script = Instantiate(Resources.Load('Prefabs/Map')):GetComponent('Map')
end

function Map.InitLua(self, mapData)
    self.script:Init(mapData)
end
