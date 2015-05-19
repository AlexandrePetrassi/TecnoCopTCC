using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public float waitTime; // Tempo que demora para a image de GameOver aparecer
	float deathTime; // Momento no tempo em que o personagem morreu

	// Use this for initialization
	void Start () {
		deathTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(deathTime + waitTime < Time.time){
			Application.LoadLevel(1);
			Destroy(gameObject);
		}
	}
}
