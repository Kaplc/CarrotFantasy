local jsonutility = require('Assets.Scripts.App.Lua.Library.JsonUtility')

BossGameDataManager = Object:SubClass('BossGameDataManager')

BossGameDataManager.cs = nil

BossGameDataManager.path = ''
BossGameDataManager.processData = nil

function BossGameDataManager:Construct()
    self.cs = LuaSceneDataManager()
    GameManager.cs.sceneManager:SetSceneDataManager(self.script)

    self.path = Application.persistentDataPath .. '/ProcessData.json'

    self.processData = self:LoadProcessData()
    -- 默认解锁的关卡
    if self.processData == nil then
        self.processData = {
            [1] = {id = 1, prize = 'None'}
        }
    end
end

function BossGameDataManager:Load(path)
    return Resources.Load(path)
end

function BossGameDataManager:LoadProcessData()
    local file = io.open(self.path, "r")

    -- 检查文件是否成功打开
    if file then
        -- 读取文件的所有内容
        local jsonString = file:read("*all")
        
        local tbl, pos, err = jsonutility.decode(jsonString, 1, jsonutility.null)
        self.processData = tbl
    
        -- 关闭文件
        file:close()

        return self.processData
    else
        -- 创建新文件
        return nil
    end
end

function BossGameDataManager:SaveProcessData()
    local str = jsonutility:encode(self.processData, { indent = true })

    local file = io.open(self.path, 'w')

    -- 检查文件是否成功打开
    if file then
        -- 写入数据到文件
        file:write(str)
        -- 关闭文件
        file:close()
    else
        print('无法打开文件进行写入操作')
    end
end

function BossGameDataManager:GetProcessData()
    return self.processData
end
