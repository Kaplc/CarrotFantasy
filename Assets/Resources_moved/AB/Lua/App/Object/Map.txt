Map = Object:SubClass('Map')

Map.cs = nil

function Map:Construct()
    self.cs = Instantiate(Resources.Load('Prefabs/Map')):GetComponent('Map')
end

function Map:Init(mapData)
    self.cs:Init(mapData)
end
