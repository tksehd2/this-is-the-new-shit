using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundSource : MonoBehaviour 
{
	public enum SOUND_TYPE
	{
		_BGM,
		_EFFECT,
	};
	public SOUND_TYPE sound_type;
	public bool Autodestruct;
	
	void Update()
	{
		if(Autodestruct == true && audio.isPlaying == false)
		{
			DestroyImmediate(gameObject);	
		}
	}
	
}
