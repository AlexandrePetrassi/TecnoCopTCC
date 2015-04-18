using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace PlayerControl{
		/// <summary>
		/// Wall Stick. 
		/// Habilidade do personagem grudar em paredes
		///  O personagem gruda na parede caso o jogador pressione o botao de trigger contra a parede.
		///  Caso o valor de "SlideSpeed" seja negativo, o personagem desliza pela parede em velocidade constante ao inves de grudar
		/// </summary>
		public class WallStick : PlayerTrigger {
			[Header("Specific")]
			[Tooltip("Velocidade em que o personagem desliza pela parede.\nCaso seja zero, ele gruda na parede")]
			public float slideSpeed = 0;  // 
			private bool wallSticking;    // Flag que indica se o personagem esta grudado na parede

			/// <summary>
			/// "Desgruda" o personagem da parede no começo de todo frame
			/// </summary>
			protected override void preStart(){
				wallSticking = false;
				//if(!collision.front.isColliding || collision.feet.isColliding || Jump.isJumping()) chargeValue = 0;
			}

			/// <summary>
			/// Gruda o personagem na parede
			/// </summary>
			protected override void start(){
				//chargeValue = 0;
				stick();
			}

			/// <summary>
			/// Enquanto o jogador pressionar o botao de trigger contra a parede, o personagem permanecerah grudado
			/// </summary>
			protected override void continuous(){
				//if(chargeValue < 1)
				stick();
			}

			/// <summary>
			/// Afasta ligeiramente o personagem da parede,
			/// para que um novo wallStick nao seja registrado caso o jogador mova o personagem contra a parede
			/// </summary>
			protected override void end(){
				//transform.position = transform.position + (new Vector3(0.1f,0,0)*(transform.localScale.x>0?-1:1));
			}

			/// <summary>
			/// Checa se as condiçoes para wallSticking sao propicias. 
			/// Caso sejam propicias, trava a velocidade no eixo Y no personagem
			/// </summary>
			private void stick(){
				wallSticking = !collision.feet.isColliding && collision.front.isColliding;
				if(wallSticking){
					move.setVelocity_y(slideSpeed,1);
				}
			}

			/// <summary>
			/// Retorna a flag de WallStick
			/// </summary>
			public static bool isWallSticking(){
				return (Player.wallStick != null) ? Player.wallStick.wallSticking : false;
			}
		}
	}
}

