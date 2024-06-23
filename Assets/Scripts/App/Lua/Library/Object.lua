-- 定义万物之父
Object = {}

Object.name = "Object"

-- 定义new实例化方法, 
function Object:New(...)
    local args = {...}
    -- 创建新表作为对象
    local newObj = {}

    -- 设置该对象的类为调用者
    setmetatable(newObj, self)
    self.__index = self
    newObj:Construct(args)

    return newObj
end

-- 定义继承方法
function Object:SubClass(subClass)
    -- 大G表中创建以该子类类名命名的类, 之后可以直接通过名字访问该子类
    _G[subClass] = {}

    -- self为调用者是父类，设置为元表
    setmetatable(_G[subClass], self)
    self.__index = self

    -- 保存父类属性
    _G[subClass].__base = self;
    
    -- 保存类名
    self.__name = subClass
    return _G[subClass]
end

-- 定义类Python的实例化方法用()实例化对象
Object.__call = function(self) 
    return Object.New(self) 
end

Object.__tostring = function(self)
    return self.__name
end

