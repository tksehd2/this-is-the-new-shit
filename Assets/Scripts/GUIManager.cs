using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class UITransition
{
	public GameMode.eGameMode _modeId;
	public UITweenManager[] _transitions;
}

public class GUIManager : MonoBehaviour 
{
	public UITransition[] _uiTransitions;
	protected static GUIManager _instance;
	public static GUIManager Instance
	{
		get 
		{
			return _instance;	
		}
	}
	
	void Awake()
	{
		_instance = this;	
	}
	
	
	public UITweenManager[] FindTransitionById(GameMode.eGameMode mode)
	{
		foreach(UITransition trs in _uiTransitions)	
		{
			if(trs._modeId == mode)
			{
				return trs._transitions;	
			}
		}
		return null;
	}
	
}
