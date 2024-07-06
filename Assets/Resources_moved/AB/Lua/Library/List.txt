List = Object:SubClass('List')

List.list = nil
List.count = nil
List.delayRemoveList = nil

function List:Construct()
    self.list = {}
    self.count = 0
end

function List:Add(item)
    self.list[self.count] = item
    self.count = self.count + 1
end

function List:Remove(item)
    for i = 0, self.count - 1 do
        if self.list[i] == item then
            -- 找到项后，将后续项向前移动
            for j = i, self.count - 2 do
                self.list[j] = self.list[j + 1]
            end
            -- 移除最后一个重复的元素
            self.list[self.count - 1] = nil
            self.count = self.count - 1
            return true -- 表示成功移除
        end
    end
    print('未找到该项')
    return false -- 表示未找到该项
end

-- 方便遍历时延迟删除
function List:DelayRemove(item)
    if self.delayRemoveList == nil then
        self.delayRemoveList = List:New()
    end

    for i = 0, self.count - 1 do
        if self.list[i] == item then
            self.delayRemoveList:Add(item)
            return true
        end
    end
    print('未找到该项')
    return false -- 表示未找到该项
end

function List:StartRemove()
    if self.delayRemoveList == nil then
        self.delayRemoveList = List:New()
    end

    for i = 0, self.delayRemoveList.count - 1 do
        self:Remove(self.delayRemoveList:Get(i))
    end
    self.delayRemoveList:Clear()
end

function List:Clear()
    self.list = {}
    self.count = 0
end

function List:Contains(item)
    for i = 0, self.count - 1 do
        if self.list[i] == item then
            return true
        end
    end
    return false
end

function List:Get(index)
    return self.list[index]
end