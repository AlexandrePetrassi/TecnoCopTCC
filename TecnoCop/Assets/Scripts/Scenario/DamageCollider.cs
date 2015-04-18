using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Collisions{
		public class DamageCollider : Collideable {
			public Damage damage = new Damage(0,DamageType.blunt);

			public override void collideOnEnter(CollisionDetector collidingObject){
				collidingObject.isColliding = true;
				if(collidingObject.tag != gameObject.tag) collidingObject.damage = damage;
			}
		}
	}
}

