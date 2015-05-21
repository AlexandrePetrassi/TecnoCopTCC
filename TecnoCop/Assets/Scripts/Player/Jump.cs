using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace PlayerControl{

		/// <summary>
		/// Player jump. 
		/// Controla a açao de salto do jogador
		/// </summary>
		public class Jump : PlayerTrigger {
			[Header("Specific")]
			[Tooltip("Quantidade de vezes que o jogador pode pular sem tocar o chao")]
			public int maxJump = 1;
			public int jumpCount;                // Quantidade de vezes que o jogador pulou. Nunca pode ultrapassar o valor de maxJump
			[Tooltip("Intensidade do salto.\nQuanto maior o valor, mais alto o personagem pula.")]
			public float jumpPower;
			[Tooltip("Objeto usado para o efeito de double Jump")]
			public GameObject doubleJumpParticle;

			override protected bool startCondition(){
				if(!collision.feet.isColliding && jumpCount == 0) jumpCount = 1; // Considera como salto toda vez que o personagem sair de uma plataforma sem pular
				return jumpCount < maxJump && !WallStick.isWallSticking() && !Dash.isDashing() && !Knockback.isKnocked(); // Bloqueia o salto caso o limite de saltos tenha sido atingido ou caso o personagem esteja grudado na parede
			}

			/// <summary>
			/// Controla quando o jogador pode pular novamente
			/// </summary>
			override protected void preStart(){
				refreshJumpCount();
			}

			/// <summary>
			/// Inicia o Salto
			/// </summary>
			override protected void start(){
				move.setVelocity_y(jumpPower,0);
				if(++jumpCount > 1) Instantiate(doubleJumpParticle,transform.position,Quaternion.identity); // Incrementa +1 no contador de pulos e instancia o efeito de DoubleJump
				playSound();
			}

			override protected void continuous(){
				if(collision.head.isColliding && ribo.velocity.y > 0) move.setVelocity_y(0,0);
			}

			/// <summary>
			/// Cancela o salto caso ainda esteja sendo executado
			/// </summary>
			override protected void end(){
				if(ribo.velocity.y > 0)	move.setVelocity_y(0,0);
			}

			/// <summary>
			/// Cancela o salto caso ainda esteja sendo executado
			/// </summary>
			override protected void postEnd(){
				if(ribo.velocity.y > 0)	move.setVelocity_y(0,0);
			}

			/// <summary>
			/// Caso o personagem toque o chao, o contador de saltos eh resetado
			/// </summary>
			private void refreshJumpCount(){
				if(collision.feet.isColliding || WallStick.isWallSticking())
					jumpCount = 0;
			}

			/// <summary>
			/// Retorna a flag de Salto
			/// </summary>
			public static bool isJumping(){
				return (Player.jump != null) ? Player.player.ribo.velocity.y > 0 : false;
			}
		}
	}
}
