using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyShot : Projectile {
			public float DamagePower = 2;
			public GameObject hitParticle;
			protected override void makeColliders ()
			{
				anim.SetBool("Started",true);
				DamageCollider dc = gameObject.AddComponent<DamageCollider>();
				dc.damage = new Damage(DamagePower,DamageType.blunt);
				dc.deleteOnCollision = true;
				dc.hitParticle = hitParticle;
				gameObject.tag = "Enemy";
				transform.SetParent(null);
				BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
				collider.isTrigger = true;
				collider.offset = new Vector2(-1.55f,0.2f);
				collider.size = new Vector2(1.5f,0.25f);

			}
		}

	}
}
