using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Collisions{
		public abstract class Collideable : MonoBehaviour {

			public virtual void collideOnEnter(CollisionDetector playerCollider){}

			public virtual void collideOnStay(CollisionDetector playerCollider){}

			public virtual void collideOnExit(CollisionDetector playerCollider){}
		}
	}
}
