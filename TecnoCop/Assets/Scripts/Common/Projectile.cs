using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 1;
	//Vector2 direction = new Vector2(0,0);


	// Use this for initialization
	void Awake () {
		float angle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z; 
		Vector2 direction = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		Destroy(gameObject,5);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
