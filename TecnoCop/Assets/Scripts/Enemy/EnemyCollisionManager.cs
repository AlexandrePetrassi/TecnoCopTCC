using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyCollisionManager : CollisionManager {

			[Header("Specific")]
			public Rect damagerRect;
			public float damagePower;
			[HideInInspector]public DamageCollider damageCollider;



			/// <summary>
			///	Inicia os colliders
			/// </summary>
			void Start(){
				feet           = makeCollider("Feet" ,feetRect);
				head           = makeCollider("Head" ,headRect);
				front          = makeCollider("Front",frontRect);
				damageCollider = makeCollider("Damager",damagerRect,damagePower,DamageType.heat);
			}

			/// <summary>
			/// A cada frame torna false todas as flags de colisao. 
			/// Caso a colisao ainda esteja ocorrendo, a flag sera setada como true antes do fim do frame
			/// Senao a flag continua como false, identificando assim uma "nao-colisao".
			/// </summary>
			public override void update(){
				feet.isColliding  = false;
				head.isColliding  = false;
				front.isColliding = false;
			}
		}
	}
}

