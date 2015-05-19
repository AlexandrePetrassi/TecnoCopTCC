using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Collisions{
		public class DamageCollider : Collideable {
			// Referencia ao objeto que instanciou este objeto colisor. Usado para que os tiros nao colidam com seus atiradores
			[HideInInspector] public GameObject parentGameObject; 
			public bool deleteOnCollision = false;

			[Header("Specific")]
			[Tooltip("Força do golpe. O quanto ele causa de dano")]
			public float damagePower = 1;
			[Tooltip("Tipo de dano (Eletrico, Gelo, Corte, Perfuraçao etc")]
			public DamageType damageType = DamageType.blunt;
			[Tooltip("Força do Knockback. Força que empurra o personagem para tras apos o dano")]
			public float damageKnockback = 1;

			// Objeto Dano, que reune os valores setados acima.
			public Damage damage;
			public GameObject hitParticle;
			void Start(){
				if(damage == null)
					damage = new Damage(damagePower,damageType, damageKnockback);
			}

			public override void collideOnStay(CollisionDetector collidingObject){
				//if(parentGameObject == collidingObject) return;
				if(collidingObject.tag != gameObject.tag){
					collidingObject.isColliding = true;
					collidingObject.transferDamage(damage);
					if(deleteOnCollision) onCollisionDestroy();
				}
			}

			public void onCollisionDestroy(){
				if(hitParticle != null)
					Instantiate(hitParticle,transform.position,transform.rotation);
				Destroy(gameObject);
			}
		}
	}
}

