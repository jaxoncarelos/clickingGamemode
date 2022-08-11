using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;


public class cashCounter : Panel
{
    public Label cashCounter;
    public cashCounter()
    {
        cashCounter = Add.Label("$0", "cash");
        StyleSheet.Load( "/ui/cashCounter.scss" );
    }

	public override void Tick()
	{
		base.Tick();
        var ply = Local.Pawn as Pawn;
        if(ply == null) return;
        cashCounter.Text = $"${ply.currentMoney}";
	}
}