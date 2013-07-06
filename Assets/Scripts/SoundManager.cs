using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	public SoundSource[] sources;
	public Dictionary<string , SoundSource> soundMap;
	GameObject bgm;
	GameObject effect;
    protected static SoundManager _instance;

    public static SoundManager Instance
    {
        get { return _instance; }
    }

	void Awake()
	{
        if(_instance == null)
            _instance = this;

        bgm = new GameObject();
        bgm.transform.parent = transform;
        bgm.name = "BGM";


        effect = new GameObject();
        effect.transform.parent = transform;
        effect.name = "EFFECT";

		soundMap = new Dictionary<string, SoundSource>();
		for(int i = 0; i < sources.Length; i++)	
		{
			soundMap.Add(sources[i].name , sources[i]);
		}
	}
    public SoundSource GetSound(SoundSource.SOUND_TYPE type,  string key)
    {
        Transform t = null;
        try
        {
            switch (type)
            {
                case SoundSource.SOUND_TYPE._BGM:
                    t = bgm.transform.FindChild(key);
                    break;
                case SoundSource.SOUND_TYPE._EFFECT:
                    t = effect.transform.FindChild(key);
                    break;
            }

            return t.GetComponent<SoundSource>();
        }
        catch (System.Exception e)
        {
            return null;
        }
    }
    public void Play(string key, float pitch)
    {
        SoundSource s = soundMap[key];
        switch (s.sound_type)
        {
            case SoundSource.SOUND_TYPE._BGM:
                for (int i = 0; i < bgm.transform.childCount; i++)
                {
                    DestroyObject(bgm.transform.GetChild(0).gameObject);
                }
                GameObject b = GameObject.Instantiate(s.gameObject) as GameObject;
                b.transform.parent = bgm.transform;
                b.name = key;
                b.audio.pitch = pitch;

                break;
            case SoundSource.SOUND_TYPE._EFFECT:
                GameObject e = GameObject.Instantiate(s.gameObject) as GameObject;
                e.transform.parent = effect.transform;
                e.name = key;
                e.audio.pitch = pitch;
                break;
        }	

    }
	public void Play(string key)
	{
        Play(key, 1.0f);
	}
}
