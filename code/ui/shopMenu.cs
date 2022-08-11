using Sandbox;
using Sandbox.UI;
using System;
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
        upgradeMoney = panel.Add.Button("Upgrade", "upgrade");
        upgradeMoney.AddEventListener( "onclick", () => {
            ConsoleSystem.Run("upgradeMoneyPerClick");
        });
        costUpgrade = panel.Add.Label(null, "cost");
        currentAmount = panel.Add.Label(null, "current");
        costUpgrade.Style.FontColor = "white";
        currentAmount.Style.FontColor = "white";
    }
	public override void Tick()
	{
		base.Tick();
        var ply = Local.Pawn as Pawn;
        if(ply == null) return;

        costUpgrade.Text = $"Cost: ${Math.Floor(100 * Math.Sqrt(ply.moneyPerClick))}";
        currentAmount.Text = $"Current: ${ply.moneyPerClick}";
        Parent.SetClass("shopMenuOpen", Input.Down(InputButton.Flashlight));
	}
}