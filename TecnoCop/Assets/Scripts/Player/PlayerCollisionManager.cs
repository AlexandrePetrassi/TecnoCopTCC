using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TecnoCop.Collisions;

namespace TecnoCop{
	namespace PlayerControl{
		/// <summary>
		/// Player collision manager.
		/// Modulo responsavel por gerenciar as colisoes em partes do personagem
		/// </summary>
		public class PlayerCollisionManager : CollisionManager {

			public Damage damage;

			/// <summary>
			///	Inicia os colliders
			/// </summary>
			void Start(){
				feet  = makeCollider("Feet" ,feetRect);
				head  = makeCollider("Head" ,headRect);
				front = makeCollider("Front",frontRect);
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