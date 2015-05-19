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

			DamageManager myDamager;
			DamageManager damager{
				get{
					if(myDamager == null){
						Transform parent = transform.parent;
						if(parent == null)
							myDamager = GetComponent<DamageManager>();
						else
							myDamager = parent.GetComponent<DamageManager>();
					}
					return myDamager;
				}
			}

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

			public void transferDamage (Damage damage){
				if(damager == null) return;
				if(damager.damage == null)
					hardTransferDamage(damage);
			}
			
			public void hardTransferDamage(Damage damage){
				if(damage == null) return;
				damager.damage = damage;
				damage = null;
			}
		}
	}
}