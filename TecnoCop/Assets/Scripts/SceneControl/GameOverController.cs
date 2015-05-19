using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	float bornTime;
	public float WaitTime;

	// Use this for initialization
	void Start () {
		bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(bornTime + WaitTime < Time.time || Input.anyKeyDown){
			Application.LoadLevel(0);
		}
	}
}
