using UnityEngine;
using System.Collections;

/// <summary>
/// Game mode base class
/// </summary>
public class GameMode : MonoBehaviour
{
	public enum eGameMode
	{
		MENU_MODE,
		ARCADE_MODE,
		SHOP_MODE,
		OPTION_MODE,
	}
	
	protected eGameMode _mode;
	
	protected GameMode(eGameMode mode)
	{
		_mode = mode;
	}
	
	public virtual bool isPausable
	{
		get { return false; }		
	}
	
	public virtual void OnEnterMode()
	{
		
	}
	public virtual void OnLeaveMode()
	{
		
	}
	public virtual void OnUpdate()
	{
	}
	
	public virtual void OnPause()
	{
		
	}
	public virtual void OnResume()
	{
		
	}
	
}
