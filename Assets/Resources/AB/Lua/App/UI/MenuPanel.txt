MenuPanel = BasePanel:SubClass('MenuPanel')

MenuPanel.cs = nil
function MenuPanel:Init()
    self.cs = self.panelObj:GetComponent('MenuPanel')
end