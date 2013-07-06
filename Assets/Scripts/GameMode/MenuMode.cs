using UnityEngine;
using System.Collections;

public class MenuMode : GameMode
{
	public MenuMode() : base(GameMode.eGameMode.MENU_MODE)
	{
		
	}
	public override bool isPausable 
	{
		get 
		{
			return false;
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
	public override void OnPause ()
	{
		base.OnPause ();
	}
	public override void OnResume ()
	{
		base.OnResume ();
	}
	public override void OnUpdate ()
	{
		base.OnUpdate ();
	}
}

