using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
public class shopMenu : Panel
{
    Panel panel;
    Button upgradeMoney;
    Label costUpgrade;
    Label currentAmount;
    public shopMenu()
    {
        StyleSheet.Load("/ui/shopMenu.scss");
        panel = Add.Panel();
        upgradeMoney = panel.Add.Button("upgrade");
        upgradeMoney.Text = "Upgrade Money";
        upgradeMoney.Style.FontColor = Color.White;
    }
	public override void Tick()
	{
		base.Tick();

        Parent.SetClass("shopMenuOpen", Input.Down(InputButton.Flashlight));
	}
}