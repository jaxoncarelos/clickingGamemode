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
        panel = Add.Panel("shopMenuOP");
        upgradeMoney = panel.Add.Button("Upgrade Money Per Click", "upgrade");
        costUpgrade = panel.Add.Label("$0", "cost");
        currentAmount = panel.Add.Label("$1", "current");
    }
	public override void Tick()
	{
		base.Tick();
        var ply = Local.Pawn as Pawn;
        if(ply == null) return;

        currentAmount.Text = $"$ssss{ply.moneyPerClick}";
        Parent.SetClass("shopMenuOpen", Input.Down(InputButton.Flashlight));
	}
}