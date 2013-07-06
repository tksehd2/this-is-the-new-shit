using UnityEngine;
using System.Collections;

public class ArcadeMode : GameMode 
{
	public ArcadeMode() : base(GameMode.eGameMode.ARCADE_MODE)
	{
		
	}
	
	public override bool isPausable {
		get {
			return true;
		}
	}
	public override void OnEnterMode ()
	{
		base.OnEnterMode ();
	}
	public override void OnLeaveMode ()
	{
		base.OnLeaveMode ();
	}
	public override void OnUpdate ()
	{
		base.OnUpdate ();
	}
	public override void OnPause ()
	{
		base.OnPause ();
	}
	
	public override void OnResume ()
	{
		base.OnResume ();
	}
	
}
