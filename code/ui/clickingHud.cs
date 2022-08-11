using Sandbox;
using Sandbox.UI;

public partial class ClickingHud : HudEntity<RootPanel>
{
	public ClickingHud()
	{
        if(!IsClient) return;
         
        RootPanel.AddChild<ChatBox>();
        RootPanel.AddChild<CashCounter>();
        RootPanel.AddChild<shopMenu>();
    }
}
