using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyAnimationManager : AnimationManager {
			
			protected override void setAnimationFlags(){
				animator.SetFloat("SpeedX",(int)(Mathf.Abs(ribo.velocity.x)));
			}
		}
	}
}

