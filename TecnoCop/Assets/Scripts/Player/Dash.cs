using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TecnoCop{
	namespace PlayerControl{

		[RequireComponent(typeof(TecnoCop.PlayerControl.Move))]

		/// <summary>
		/// Dash 
		/// Controla a habilidade de Dash do jogador.
		///  O Dash eh um impulso de velocidade no eixo horizontal que dura meio segundo, mas aumenta drasticamente a velocidade do personagem.
		///  A habilidade eh interrompida caso o jogador solte o botao ou caso o personagem colida frontalmente com algum objeto.
		///  A Gravidade eh anulada temporariamente enquanto o dash estiver ativo
		/// </summary>
		public class Dash : PlayerTrigger {
			[Header("Specific")]
			[Tooltip("Velocidade do Dash")]
			public float speedMultiplier = 2;
			[Tooltip("Tempo que o Dash dura")]
			public float dashTime = 0.5f;
			private float maxTime;            // Momento em que o dash sera forçadamente interrompido.
			[Tooltip("Habilita/Desabilita o Air Dash")]
			public bool airDashEnabled; 
			[Tooltip("GameObject usado como particula para gerar o efeito de rastro")]
			public GameObject dashParticle; 
			[SerializeField]
			Image cdIcon;
			Image cooldownIcon{
				get{
					if(cdIcon == null) cdIcon = GameObject.FindGameObjectWithTag("DashIcon").GetComponent<Image>();
					return cdIcon;
				}
				set{
					cdIcon = value;
				}
			}

			/// <summary>
			/// O jogador nao pode iniciar um dash em meio a um dash, nem quando estiver grudado em uma parede,
			/// e caso nao esteja com airDash habilitado soh podera executa-lo quando estver no chao.
			/// </summary>
			protected override bool startCondition(){
				return !isDashing() && !WallStick.isWallSticking() && (airDashEnabled? true: collision.feet.isColliding); 
			}

			/// <summary>
			/// Inicia o Dash e define seu tempo de duraçao
			/// </summary>
			protected override void start(){
				maxTime = Time.time + dashTime; // Seta o momento no tempo onde dash termina
				startCooldown();
				executeDash();
				playSound();
			}

			protected override void startCooldown ()
			{
				base.startCooldown ();
				StartCoroutine (UpdateCooldownIcon (cooldownIcon));
			}

			void OnLevelWasLoaded(){
				cdIcon = null;
			}

			IEnumerator UpdateCooldownIcon(Image icon)
			{
				if (!icon)
					yield break;
				float totalTime = cooldown;
				float currentTime = totalTime;
				while (currentTime > 0) {
					icon.fillAmount = Mathf.Lerp(0, 1, getCooldown());
					currentTime -= Time.deltaTime;
					yield return new WaitForEndOfFrame();
				}
				icon.fillAmount = Mathf.Lerp(0, 1, 0);
			}

			/// <summary>
			/// Enquanto a tecla de gatilho estiver pressionada o dash continua,
			/// exceto se o tempo acabar ou se houver uma colisao frontal
			/// </summary>
			protected override void continuous(){
				if(maxTime > Time.time && !collision.front.isColliding){
					executeDash();
				}else{
					cancelDash();
				}
			}

			/// <summary>
			/// Cancela o dash caso o jogador solte o botao de gatilho
			/// </summary>
			protected override void end(){
				cancelDash();
			}

			/// <summary>
			/// Retorna a flag de Dash
			/// </summary>
			public static bool isDashing(){
				return (Player.dash != null) ? Player.dash.maxTime > Time.time : false;
			}

			/// <summary>
			/// Executa o dash em si, aumenta a velocidade horizontal, e anulando o movimento vertical.
			/// </summary>
			private void executeDash(){
				move.setVelocity_x(((Move)move).speed * speedMultiplier * Time.deltaTime * (transform.localScale.x>0?1:-1),1);
				move.setVelocity_y(0,1);
				if(lastParticleBornTime+0.075f < Time.time){
					Instantiate(dashParticle);
					lastParticleBornTime = Time.time;
				} 
			}
			float lastParticleBornTime;

			/// <summary>
			/// Cancela o dash
			/// </summary>
			public void cancelDash(){
				maxTime = Time.time;
			}
		}
	}
}
