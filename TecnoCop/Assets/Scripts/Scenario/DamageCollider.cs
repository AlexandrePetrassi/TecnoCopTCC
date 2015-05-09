using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Collisions{
		public class DamageCollider : Collideable {
			public Damage damage = new Damage(1,DamageType.blunt);

			public override void collideOnEnter(CollisionDetector collidingObject){
				if (collidingObject.tag != gameObject.tag) {
					collidingObject.isColliding = true;
					collidingObject.damage = damage;
				}
			}
		}
	}
}

