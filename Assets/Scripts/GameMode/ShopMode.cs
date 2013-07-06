using UnityEngine;
using System.Collections;

public class ShopMode : GameMode {
	
	public ShopMode() : base(GameMode.eGameMode.SHOP_MODE)
	{
		
	}
	public override bool isPausable {
		get {
			return base.isPausable;
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
	
	public override void OnUpdate()
	{
	}
	
	public override void OnPause()
	{
		
		
	}
	public override void OnResume()
	{
	}	
}