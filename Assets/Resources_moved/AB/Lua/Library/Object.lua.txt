-- 定义万物之父
Object = {}

Object.__name = 'Object'
Object.__index = Object

-- 定义new实例化方法,
function Object:New(...)
    -- 创建新表作为对象
    local newObj = {}

    -- 设置该对象的类为调用者
    setmetatable(newObj, self)
    newObj.__name = 'new ' .. self.__name .. '()'
    newObj.__index = newObj
    -- 调用构造函数，并传递所有参数
    if newObj.Construct then
        newObj:Construct(...)
    end

    return newObj
end

-- 定义继承方法
function Object:SubClass(subClass)
     local new_class = {
        __name = subClass,
        __base = self,
    }
    new_class.__index = new_class
    setmetatable(new_class, self) -- 设置子类的元表为父类

    -- 设置 __call 元方法，使类可以使用 () 实例化对象
    setmetatable(
        new_class,
        {
            __index = self, -- 设置元表的 __index 指向父类，用于继承父类的静态方法和字段
            __call = function(cls, ...)
                return cls:New(...) -- 调用子类的 New 方法实例化对象
            end
        }
    )

    -- 将新类保存到全局表中，以便之后可以直接通过名字访问该子类
    _G[subClass] = new_class

    return new_class
end

function Object:__tostring()
    return self.__name
end

function Object:__call()
end
