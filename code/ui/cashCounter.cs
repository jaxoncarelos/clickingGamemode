using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;


public class cashCounter : Panel
{
    public Label cashCounterLbl;
    public cashCounter()
    {
        cashCounterLbl = Add.Label("$0", "cash");
        StyleSheet.Load( "/ui/cashCounter.scss" );
    }

	public override void Tick()
	{
		base.Tick();
        var ply = Local.Pawn as Pawn;
        if(ply == null) return;
        cashCounterLbl.Text = $"${ply.currentMoney}";
	}
}
