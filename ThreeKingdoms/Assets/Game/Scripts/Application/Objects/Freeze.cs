using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void FreezeForSecond (float second){
		StartCoroutine (FreezeForSec (second));
	}

	IEnumerator FreezeForSec(float second) {
		GetComponent<Animator> ().speed = 0;
		yield return new WaitForSeconds (second);
		GetComponent<Animator> ().speed = 1.0f;
	}
}