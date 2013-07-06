using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public GameMode[] _modes;
	
	protected Dictionary<GameMode.eGameMode , GameMode> _gameModes;
	protected GameMode _curMode;
	private static GameManager _instance;
	public static GameManager Instance
	{
		get 	
		{
			return _instance;
		}
	}
	
	public GameMode.eGameMode _curModeId;
	
	public GameMode curGameMode
	{
		get 	
		{
			return _curMode;
		}
	}
	
	void Awake()
	{
		_instance = this;
		Init();
	}
	void Init()
	{
		_gameModes = new Dictionary<GameMode.eGameMode, GameMode>();
		
		foreach(GameMode mode in _modes)
		{
			_gameModes.Add(mode.ModeID , mode);
		}
		
		ChangeMode(GameMode.eGameMode.MENU_MODE);
	}
	
	public void ChangeMode(GameMode.eGameMode mode)
	{
		if(_curMode != null)
		{
			_curMode.OnLeaveMode();
		}
		
		_curMode = _gameModes[mode];
		_curModeId = _curMode.ModeID;
		
		if(_curMode != null)
		{
			_curMode.OnEnterMode();
		}
	}
	
	void Update()
	{
		if(_curMode != null)
		{
			_curMode.OnUpdate();	
		}
	}
	
	void OnApplicationPause(bool pause)
	{
		if(_curMode != null)	
		{
			if(pause) 	_curMode.OnPause();
			else 		_curMode.OnResume();
		}
	}
	
	
	void testMode()
	{
		if(_curMode.ModeID == GameMode.eGameMode.MENU_MODE)
		{
			ChangeMode(GameMode.eGameMode.ARCADE_MODE);
		}
		else 
		{
			ChangeMode(GameMode.eGameMode.MENU_MODE);
		}	
	}
	
#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		if(false == Application.isPlaying)
		{
			_modes = GetComponentsInChildren<GameMode>();
		}
	}
#endif
}
