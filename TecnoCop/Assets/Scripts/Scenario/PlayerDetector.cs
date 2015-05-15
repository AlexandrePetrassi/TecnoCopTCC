using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TecnoCop.PlayerControl;

/// <summary>
/// PlayerDetector
/// Detecta a presença do personagem do jogador
/// </summary>
namespace TecnoCop{
	namespace Collisions{
		public class PlayerDetector : MonoBehaviour {
			private bool wasColliding;
			public bool isColliding;

			/*void OnTriggerEnter2D(Collider2D collider){
				if(collider.GetComponent<Player>() != null)
					isColliding = true;
				else if(collider.transform.parent != null)
					if(collider.transform.parent.GetComponent<Player>() != null)
						isColliding = true;
			}
			
			void OnTriggerStay2D(Collider2D collider){
				if(collider.GetComponent<Player>() != null)
					isColliding = true;
				else if(collider.transform.parent != null)
					if(collider.transform.parent.GetComponent<Player>() != null)
						isColliding = true;
			}*/


		}
	}
}