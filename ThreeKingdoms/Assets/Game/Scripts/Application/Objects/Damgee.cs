using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damgee : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Destroy()
	{
		Game.Instance.ObjectPool.Unspawn(this.gameObject);
	}
}
