PrizeCell = Object:SubClass('PrizeCell')

PrizeCell.cell = nil
PrizeCell.prizeTowerType = nil

function PrizeCell:Construct(cell, prizeTowerType)
    self.cell = cell
    self.prizeTowerType = prizeTowerType
end