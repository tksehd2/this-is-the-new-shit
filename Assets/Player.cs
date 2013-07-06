using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public GameObject _trail;
	
	void Awake()
	{
		Util.AttachGameObject(_trail , gameObject , Vector3.zero);
	}
	
	void Update()
	{
		transform.Translate(0 , Time.time , 0);	
	}
	
}
