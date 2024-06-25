PrizeCell = Object:SubClass('PrizeCell')

PrizeCell.cell = nil
PrizeCell.prizeTowerType = nil

function PrizeCell:Construct(args)
    self.cell = args[1]
    self.prizeTowerType = args[2]
end