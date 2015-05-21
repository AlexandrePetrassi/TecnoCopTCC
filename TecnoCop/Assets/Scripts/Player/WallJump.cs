using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace PlayerControl{

		/// <summary>
		/// Wall jump.
		/// Gerencia a habilidade do personagem de saltar enquanto estah grudado em uma parede
		///  O personagem realiza um salto caso a tecla de gatilho seja apertada quando o personagem estiver grudado na parede
		///  O salto impede a movimentaçao manual do jogador por um pequeno periodo de tempo.
		///  O "impedimento de movimentaçao" eh o impulso que o personagem faz no sentido oposto ao da parede
		/// </summary>
		public class WallJump : PlayerTrigger {
			[Header("Specific")]
			[Tooltip("Tempo de duraçao do wallJump")]
			public float wallJumpTime = 0.1f; 
			private float maxTime;            // Momento no Tempo onde o wallJump terminarah
			private int jumpDirection;        // Direçao do salto

			/// <summary>
			/// O wallJump apenas pode ser realizado caso o personagem esteja grudado na parede
			/// </summary>
			protected override bool startCondition(){
				return WallStick.isWallSticking() && !WallJump.isWallJumping() && Player.player.wallJumpSkill;

			}

			/// <summary>
			/// Move o personagem contra a parede em todos os frames em que o WallJump estiver ativo
			/// </summary>
			protected override void preStart(){
				if(isWallJumping())	setWallJumpSpeed();
			}

			/// <summary>
			/// Inicia o wallJump
			/// </summary>
			protected override void start(){
				setJumpDirection();
				setWallJumpSpeed();
				setEndingTime();
			}

			/// <summary>
			/// Cancela o wallJump em meio a sua execuçao caso o jogador solte o botao de trigger
			/// </summary>
			protected override void end(){
				cancelWallJump();
			}

			/// <summary>
			/// Retorna a flag de wallJump
			/// </summary>
			public static bool isWallJumping(){
				return (Player.wallJump != null) ? Player.wallJump.maxTime > Time.time : false;
			}

			/// <summary>
			/// Calcula o periodo no tempo em que o WallJump terminarah
			/// </summary>
			private void setEndingTime(){
				maxTime = Time.time + wallJumpTime;
			}

			/// <summary>
			/// Cancela o WallJump em meio a sua execuçao
			/// </summary>
			private void cancelWallJump(){
				maxTime = Time.time;
			}

			/// <summary>
			/// Calcula a direçao para qual o personagem irah pular.
			/// A direçao eh a direçao no sentido oposto ao da parede
			/// </summary>
			private void setJumpDirection(){
				jumpDirection = transform.localScale.x>0?-1:1;
			}

			/// <summary>
			/// Move o jogador na direçao oposta a parede e para cima
			/// </summary>
			private void setWallJumpSpeed(){
				move.setVelocity_x(((Move)move).speed * Time.deltaTime * jumpDirection,2);
				move.setVelocity_y(Player.jump.jumpPower*0.75f,2);
			}

		}
	}
}
