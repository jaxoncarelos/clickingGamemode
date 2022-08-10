using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
public class shopMenu : Panel
{
    Panel panel;
    public shopMenu()
    {
        StyleSheet.Load("/ui/shopMenu.scss");
        panel = Add.Panel();
    }
	public override void Tick()
	{
		base.Tick();

        Parent.SetClass("shopMenuOpen", Input.Down(InputButton.Flashlight));
	}
}