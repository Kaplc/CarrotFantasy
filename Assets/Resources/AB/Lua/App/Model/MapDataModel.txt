MapDataModel = Object:SubClass('MapDataModel')

MapDataModel.mapData = nil

function MapDataModel.Load(self, mapIndex)
    local mapData = Resources.Load('AB/Data/BossMap' .. mapIndex)
    return mapData
end
