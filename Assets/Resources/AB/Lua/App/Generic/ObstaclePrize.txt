ObstaclePrize = Object:SubClass('ObstaclePrize')

ObstaclePrize.obstacle = nil
ObstaclePrize.prizeCellList = nil

function ObstaclePrize:Construct(obstacle)
    self.prizeCellList = List:New()
    self.obstacle = obstacle
end

function ObstaclePrize:AddPrizeCell(cell)
    self.prizeCellList:Add(cell)
end

function ObstaclePrize:HasPrize()
    if self.prizeCellList.count > 0 then
        return true
    end
    
    return false
end
