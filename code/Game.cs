﻿using System;
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
	}
	[ConCmd.Server("upgradeMoneyPerClick")]
	public static void upgrade()
	{
		Log.Info("ran");
		var ply = ConsoleSystem.Caller.Pawn as Pawn;
		if(ply == null) return;
		if(ply.currentMoney >= Math.Floor(100 * Math.Sqrt(ply.moneyPerClick)))
		{
			Log.Info("Inside");
			ply.currentMoney -= 55 * (ply.moneyPerClick * ply.moneyPerClick);
			ply.moneyPerClick++;
			Log.Info(ply.moneyPerClick);
		}
	}
	[ConCmd.Server("setMoney")]
	public static void setMoney(string amount)
	{
		var ply = ConsoleSystem.Caller.Pawn as Pawn;
		ply.currentMoney = Convert.ToInt64(amount);
	}
	[ConCmd.Server]
	public static void printLocation()
	{
		Log.Info(ConsoleSystem.Caller.Pawn.Position);
	}
}
