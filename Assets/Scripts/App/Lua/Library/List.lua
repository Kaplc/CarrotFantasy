List = Object:SubClass('List')

List.list = nil
List.count = nil

function List:Construct()
    self.list = {}
    self.count = 0
end

function List:Add(item)
    self.list[self.count] = item
    self.count = self.count + 1
end

function List:Clear()
    self.list = {}
    self.count = 0
end

function List:Get(index)
    return self.list[index]
end