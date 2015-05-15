using UnityEngine;
using System.Collections;

namespace TecnoCop{
	public class Projectile : MonoBehaviour {
		
		public float speed = 1;
		[HideInInspector]public bool started = false;
		protected Animator anim;
		
		// Use this for initialization
		void Awake () {
			anim = GetComponent<Animator>();
			if(anim == null) start();
		}
		
		void start(){
			if(started) return;
			started = true;
			float angle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z; 
			Vector2 direction = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
			GetComponent<Rigidbody2D>().velocity = direction * speed;
			Destroy(gameObject,5);
			if(anim != null){
				makeColliders();
			} 
		}

		protected virtual void makeColliders(){}
	}
}
