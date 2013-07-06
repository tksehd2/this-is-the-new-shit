using UnityEngine;
using System.Collections;

public class MenuMode : GameMode
{	
	UITweenManager[] _modeTransition;
	
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
		_modeTransition = GUIManager.Instance.FindTransitionById(_modeId);
		
		_modeTransition[0].PlayById("show");
		
		base.OnEnterMode ();
	}
	public override void OnLeaveMode ()
	{
		_modeTransition[0].PlayById("hide");
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

