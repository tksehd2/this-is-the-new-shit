using UnityEngine;
using System.Collections;
using System.Reflection;

[System.Serializable]
public class TweenObject
{
	public string _tweenId;
	public UITweener _tweenPrefab;
}

public class UITweenManager : MonoBehaviour 
{
	public TweenObject[] _objs;
	
	protected int anim_index = 0;
	
	void Awake()
	{
		Reset();
	}
	
	public void Play(int index)
	{
		anim_index = index;
		
		UITweener compInstance = gameObject.AddComponent(_objs[anim_index]._tweenPrefab.GetType()) as UITweener;
		DuplicateComponent<UITweener>(ref _objs[anim_index]._tweenPrefab , ref compInstance);
		
		compInstance.onFinished = OnAnimEnd;
	}
	
	public void PlayById(string containsId)
	{
		foreach(TweenObject obj in _objs)	
		{
			if(obj._tweenId.Contains(containsId))	
			{
				UITweener compInstance = gameObject.AddComponent(obj._tweenPrefab.GetType()) as UITweener;
				DuplicateComponent<UITweener>(ref obj._tweenPrefab , ref compInstance);
				
				compInstance.onFinished = OnAnimEnd;
			}
		}
		
		
	}
	
	public void PlayNext()
	{
		Play(anim_index);
		if(_objs.Length - 1 > anim_index)
		{
			anim_index++;
		}
	}
	
	public void Reset()
	{
		anim_index = 0;
	}
	
	void OnAnimEnd(UITweener tween)
	{
		GameObject.Destroy(tween);
	}
	
	void DuplicateComponent<T>(ref T src, ref T dest)
	{
		foreach(FieldInfo field in src.GetType().GetFields())
    		field.SetValue(dest, field.GetValue(src));
	}
	
	
}
