Map = Object:SubClass('Map')

Map.obj = nil
Map.mono = nil

Map.rowNum = 8
Map.colNum = 12
Map.mapLeftDownPos = 0
Map.cellWidth = 0
Map.cellHeight = 0

Map.bgSr = nil
Map.roadSr = nil

Map.cellList = {}
Map.towerDataList = {}
Map.mapData = nil

Map.isBuilting = false

function Map.Init(self, obj, mapData)
    -- 移除C#组件
    Destroy(obj:GetComponent('Map'))
    self.mono = obj:AddComponent(typeof(MonoScript))
    self.mono.onUpdateAction = function()
        self:Update()
    end
    -- 初始化数据
    self.obj = obj
    self.mapData = mapData
    self.bgSr = obj:GetComponent('SpriteRenderer')
    self.roadSr = obj.transform:Find('Road'):GetComponent('SpriteRenderer')
    self.bgSr.sprite = mapData.mapBgTexture
    self.roadSr.sprite = mapData.mapFgTexture

    self:CellInit(mapData)
end

function Map.Update(self)
    if Input.GetMouseButtonDown(0) then
        self:OnMouseLButtonDown()
    end
end

function Map.OnMouseLButtonDown(self)
    -- 射线检测是否已经被建造面板遮挡
    local gr = CS.UIManager.Instance.canvas:GetComponent('GraphicRaycaster')
    local ped = CS.UnityEngine.EventSystems.PointerEventData(CS.UnityEngine.EventSystems.EventSystem.current)
    ped.position = CS.UnityEngine.Input.mousePosition
    local results = CS.System.Collections.Generic.List(CS.UnityEngine.EventSystems.RaycastResult)()
    gr:Raycast(ped, results)

    if results.Count > 0 and results[0].gameObject.name == 'ImageAttackRange' then
        print('被遮挡')
        return
    end

    local cell = self:GetMouseButtonDownPosCell()

    if cell == nil then
        return
    end

    if cell.IsTowerPos then
        if self.isBuilting then
            -- 关闭建造面板
            UIManager:HidePanel('BuiltPanel')
        else
            local panel = UIManager:ShowPanel('BuiltPanel')
            -- 打开建造面板
            if cell.tower == nil then
                -- 创建
                panel:ShowCreateTower(self:GetCellPos(cell), self.towerDataList)
            else
                -- 升级
                panel:ShowUpgradeTower(self:GetCellPos(cell))
            end
        end
    else
        -- 禁止建造
        local panel = UIManager:ShowPanel('BuiltPanel')
        panel:ShowCantBuilt(self:GetCellPos(cell))
    end
end

function Map.CellInit(self, mapData)
    self.cellWidth = self.bgSr.size.x / self.colNum
    self.cellHeight = self.bgSr.size.y / self.rowNum
    self.mapLeftDownPos = self.obj.transform.position - Vector3(self.bgSr.size.x / 2, self.bgSr.size.y / 2, 0)

    self.cellList = {}
    for x = 0, self.colNum - 1 do
        for y = 0, self.rowNum - 1 do
            local cell = Cell(Point(x, y))
            self.cellList[x .. y] = cell
        end
    end

    -- 初始化格子信息
    -- 障碍物
    for i = 0, mapData.obstacleList.Count - 1 do
        local cell = self.cellList[mapData.obstacleList[i].x .. mapData.obstacleList[i].y]
        cell.hasObstacle = true
        cell.obstacleName = self.mapData.obstacleList[i].obstacleType:ToString()
    end

    -- 建造点
    for i = 0, mapData.towerPosList.Count - 1 do
        local cell = self.cellList[mapData.towerPosList[i].x .. mapData.towerPosList[i].y]
        cell.IsTowerPos = true
    end
end

function Map.GetCellPos(self, cell)
    return self.mapLeftDownPos +
        Vector3(cell.X * self.cellWidth + self.cellWidth / 2, cell.Y * self.cellHeight + self.cellHeight / 2, 0)
end

function Map.WorldPosGetCell(self, pos)
    local x = math.floor((pos.x - self.mapLeftDownPos.x) / self.cellWidth)
    local y = math.floor((pos.y - self.mapLeftDownPos.y) / self.cellHeight)
    return self.cellList[x .. y]
end

function Map.GetMouseButtonDownPosCell(self)
    local pos = Camera.main:ScreenToWorldPoint(Input.mousePosition)
    local cell = self:WorldPosGetCell(pos)
    return cell
end
