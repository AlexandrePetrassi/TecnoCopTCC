using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	namespace PlayerControl{
		/// <summary>
		/// Animation manager.
		/// Gerencia as flags de animaçao do personagem e o "espelhamento" das sprites
		/// </summary>
		public class PlayerAnimationManager : AnimationManager {
			
			/// <summary>
			/// Atualiza as Flags das animaçoes
			/// </summary>
			protected override void setAnimationFlags(){
				animator.SetInteger("SpeedX",(int)(ribo.velocity.x>0?1:(ribo.velocity.x<0?-2:0)));
				animator.SetInteger("SpeedY",(int)(ribo.velocity.y>0?1:(ribo.velocity.y<0?-2:0)));
				animator.SetBool("WallSticking",WallStick.isWallSticking());
				animator.SetBool("Dash",Dash.isDashing());
				animator.SetBool("OnGround",collision.feet.isColliding);
				animator.SetBool("TryingToPassThroughWall",collision.front.isColliding && Mathf.Sign(ribo.velocity.x) == Mathf.Sign(transform.localScale.x));
				animator.SetBool("Damage",Knockback.isKnocked());
			}
		}
	}
}
