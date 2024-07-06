BossMonster = Monster:SubClass("BossMonster")

function BossMonster:Construct(obj)
    self.__base.Construct(self, obj)
end

function BossMonster:Wound(v)
    self.__base.Wound(self, v)
    self.hpBg.gameObject:SetActive(false)
    -- 更新game panel 上的血条
    local gamePanel = UIManager:GetPanel('BossGamePanel')
    gamePanel:UpdateHP(self.hp/self.data.maxHp)
end
