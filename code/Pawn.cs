using Sandbox;
using System;
using System.Diagnostics;
using System.Linq;

namespace Sandbox;

partial class Pawn : Player
{
	
	/// <summary>
	/// Called when the entity is first created 
	/// </summary>
	[Net]
	public long currentMoney { get; set; } = 0;
	[Net]
	public int moneyPerClick { get; set; } = 1;
	public override void Spawn()
	{
		base.Spawn();

        SetModel( "models/citizen/citizen.vmdl_c" );

        Controller = new WalkController();
		if ( DevController is NoclipController )
		{
			DevController = null;
		}

		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
		CameraMode = new FirstPersonCamera();
	}
	

	/// <summary>
	/// Called every tick, clientside and serverside.
	/// </summary>
	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		Rotation = Input.Rotation;
		EyeRotation = Rotation;

		if ( IsServer && Input.Pressed( InputButton.PrimaryAttack ) )
		{
			currentMoney += moneyPerClick;
		}
	}

	/// <summary>
	/// Called every frame on the client
	/// </summary>
	public override void FrameSimulate( Client cl )
	{
		base.FrameSimulate( cl );

		// Update rotation every frame, to keep things smooth
		Rotation = Input.Rotation;
		EyeRotation = Rotation;
	}
}
