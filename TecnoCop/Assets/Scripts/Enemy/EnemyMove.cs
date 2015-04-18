using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyMove : Trigger, MovementManager{
			[Header("Specific")]
			[Tooltip("Velocidade de movimento do personagem")]
			public  float speed;
			private float gravity;         // Força da gravidade. Valor utilizado para recuperar o valor padrao da gravidade depois que a gravidade eh desativada
			public float range = 5;

			//OBS: Nunca setar o valor destes quatro campos diretamente. Para isso, usar os metodos setVelocity e getVelocity
			private float x_velocity;      // Valor que pode sobreescrever a velocidade no eixo X
			private float y_velocity;      // Valor que pode sobreescrever a velocidade no eixo y
			private int   x_priority = -1; // Valor da prioridade de sobreescrita da velocidade no eixo x
			private int   y_priority = -1; // Valor da prioridade de sobreescrita da velocidade no eixo y
			
			/// <summary>
			/// Inicializa o valor da gravidade para que este possa ser recuperado posteriormente
			/// </summary>
			void Start(){
				gravity = ribo.gravityScale; // Memoriza o valor da gravidade do personagem, setada inicialmente no inspector do Rigidbody
			}

			/// <summary>
			/// Apenas move o persoangem caso o jogador esteja proximo dele
			/// </summary>
			protected override bool getTriggerInput(){
				return true;

			}

			/// <summary>
			/// Seta o valor da velocidade em ambos os eixos, baseado nos valores obtidos de x_velocity e y_velocity
			/// </summary>
			protected override void preStart(){
				//toogleGravity(!(Dash.isDashing() || WallStick.isWallSticking()));
				setVelocity_x(speed * Time.deltaTime * getMoveDirection(),0);
				ribo.velocity = new Vector2(getVelocity_x(),getVelocity_y()); 
			}
			
			/// <summary>
			/// Liga/Desliga a gravidade, tendo como referencia o valor gravity armazenado
			/// </summary>
			public void toogleGravity(bool value){
				ribo.gravityScale = value?gravity:0;
			}
			
			/// <summary>
			/// Tenta sobreescrever a velocidade no eixo X
			/// Caso uma tentativa de sobreescrita ja tenha sido realizada neste frame, prevalece a com maior prioridade
			/// </summary>
			public void setVelocity_x(float velocity, int priority){
				if(x_priority >= 0){
					if(priority < x_priority) return;
				}
				x_velocity = velocity;
				x_priority = priority;
			}
			
			/// <summary>
			/// Tenta sobreescrever a velocidade no eixo y
			/// Caso uma tentativa de sobreescrita ja tenha sido realizada neste frame, prevalece a com maior prioridade
			/// </summary>
			public void setVelocity_y(float velocity, int priority){
				if(y_priority >= 0){
					if(priority < y_priority) return;
				}
				y_velocity = velocity;
				y_priority = priority;
			}
			
			/// <summary>
			/// Retorna o valor da velocidade no eixo X
			/// Caso a velocidade nao tenha sido sobreescrita, retorna o proprio valor do eixo X
			/// Seta a prioridade como -1, indicando para o proximo frame que a velocidade nao foi sobreescrita ainda
			/// </summary>
			private float getVelocity_x(){
				if(x_priority < 0) return ribo.velocity.x;
				x_priority = -1;
				return x_velocity;
			}
			
			/// <summary>
			/// Retorna o valor da velocidade no eixo y
			/// Caso a velocidade nao tenha sido sobreescrita, retorna o proprio valor do eixo y
			/// Seta a prioridade como -1, indicando para o proximo frame que a velocidade nao foi sobreescrita ainda
			/// </summary>
			private float getVelocity_y(){
				if(y_priority < 0) return ribo.velocity.y;
				y_priority = -1;
				return y_velocity;
			}

			// Direçao para onde o personagem deve andar
			public int getMoveDirection(){
				if(Vector3.Distance(Player.player.transform.position,transform.position)>range) return 0;
				if(Player.player.transform.position.x > transform.position.x) return 1;
				if(Player.player.transform.position.x < transform.position.x) return -1;
				return 0;
			}
		}

	}
}
