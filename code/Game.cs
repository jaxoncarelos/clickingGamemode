using System;
using System.Linq;
using Sandbox;
//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
public partial class MyGame : Sandbox.Game
{
	public static WebSocketClient WebSocketClient;
	public MyGame()
	{
		if(IsServer)
		{
			_ = new clickingHud();
		}
	}
	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		// Create a pawn for this client to play with
		var pawn = new Pawn();
		client.Pawn = pawn;
		

		// Get all of the spawnpoints
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			pawn.Transform = tx;
		}

		StartWebSocketRpc( To.Single( client ), client );

		
		pawn.Respawn();
	}
	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{

		base.ClientDisconnect( cl, reason );

	}

	[ClientRpc]
	private async void StartWebSocketRpc(Client client)
	{
		WebSocketClient = new WebSocketClient();
		bool isConnected = await WebSocketClient.Connect();
		if ( isConnected ) Log.Info( "Connection to WS Server Successful" );
		var pawn = client.Pawn as Pawn;
		
		WebSocketClient.SendMessage($"request {client.PlayerId}");

		if(pawn.currentMoney == 0)
		{	
			WebSocketClient.SendMessage($"create {client.PlayerId} 0 1");
			return;
		}


	}
	
	[ConCmd.Server("setValues")]
	public static void setValues(string message)
	{
		Log.Info(message);
		var values = message.Split(" ");

		var ply = ConsoleSystem.Caller.Pawn as Pawn;

		ply.currentMoney = Convert.ToInt64(values[1]);
		ply.moneyPerClick = Convert.ToInt32(values[2]);
	}
	[ConCmd.Server("upgradeMoneyPerClick")]
	public static void upgrade()
	{
		var ply = ConsoleSystem.Caller.Pawn as Pawn;
		if(ply == null) return;
		
		if(ply.currentMoney >= 55 * (ply.moneyPerClick * ply.moneyPerClick))
		{
			ply.currentMoney -= 55 * (ply.moneyPerClick * ply.moneyPerClick);
			ply.moneyPerClick++;
		}
		WebSocketClient.SendMessage($"update {ConsoleSystem.Caller.PlayerId} {ply.currentMoney} {ply.moneyPerClick}");

	}
}