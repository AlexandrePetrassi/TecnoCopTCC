using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Collisions{
		public class SolidBlock : Tile {

			public override void collideOnEnter(CollisionDetector playerCollider){
				playerCollider.isColliding = true;
			}

			public override void collideOnStay(CollisionDetector playerCollider){
				playerCollider.isColliding = true;
			}
		}
	}
}
