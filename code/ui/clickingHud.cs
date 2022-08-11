using Sandbox;
using Sandbox.UI;

public partial class clickingHud : HudEntity<RootPanel>
{
	public clickingHud()
	{
        if(!IsClient) return;
         
        RootPanel.AddChild<ChatBox>();
        RootPanel.AddChild<cashCounter>();
        RootPanel.AddChild<shopMenu>();
    }
}
