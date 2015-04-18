using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PlayerCollider
/// Controla os subColliders do personagem
/// </summary>
namespace TecnoCop{
	namespace Collisions{
		public class CollisionDetector : MonoBehaviour {
			private bool wasColliding;
			public bool isColliding;
			public Damage damage;
			
			void OnTriggerEnter2D(Collider2D collider){
				Collideable script = collider.GetComponent<Collideable>();
				if(script != null) script.collideOnEnter(this);
			}
			
			void OnTriggerStay2D(Collider2D collider){
				Collideable script = collider.GetComponent<Collideable>();
				if(script != null) script.collideOnStay(this);
			}
			
			void OnTriggerExit2D(Collider2D collider){
				Collideable script = collider.GetComponent<Collideable>();
				if(script != null) script.collideOnExit(this);
			}
		}
	}
}