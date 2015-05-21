using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace TecnoCop{
	namespace PlayerControl{

		/// <summary>
		/// Player trigger. 
		/// Controla o disparo das açoes do jogador
		/// </summary>
		public abstract class PlayerTrigger : ModuleManager{
			[Header("General")]
			[Tooltip("Botao/Eixo usado para ativar o script")]
			public TriggerAxis triggerAxis; 

			private bool myIsPressed;         // Checa se o botao esta precionado
			private bool myWasPressed;        // Checa se o botao estava pressionado na ultima iteraçao

			[Tooltip("Tempo de tolerancia de um tap pro outro")]
			public float tapInterval;

			[HideInInspector]
			public bool hasStarted;           // Flag que indica se trigger foi iniciado;

			[Tooltip("Tempo de cooldown deste comando \n(Valores negativos ou zero significam 'sem cooldown')")]
			public float cooldown;
			private float cooldownTime;       // Momento no tempo em que o cooldown estarah pronto

			[Tooltip("Habilita o que esta açao tenha um resultado diferente quando carregada)")]
			public bool chargeable;
			[Tooltip("Tempo de carregamento do trigger \n(Exemplo: Tiro carregado do megaman)")]
			public float charge;
			protected float chargeTime;       // Momento no tempo em que o carregamento estarah completo

			public AudioClip sound;
			public float minPitch;
			public float maxPitch;
			public float volume = 0.5f;

			protected virtual void playSound(){
				playSound(maxPitch,minPitch,volume);
			}

			protected virtual void playSound(float min,float max, float vol){
				if(sound == null) return;
				audioS.clip = sound;
				audioS.pitch = Random.Range(min,max);
				audioS.volume = vol;
				audioS.Play();
			}

			/// <summary>
			/// Retorna o valor do eixo da tecla apertada
			/// </summary>
			protected float getAxisValue(){
				switch(triggerAxis){
				case TriggerAxis.HorizontalAxis:
					return CrossPlatformInputManager.GetAxisRaw("Horizontal");
				case TriggerAxis.Up:
					return CrossPlatformInputManager.GetAxisRaw("Vertical");
				case TriggerAxis.Down:
					return CrossPlatformInputManager.GetAxisRaw("Vertical");
				case TriggerAxis.Accept:
					return CrossPlatformInputManager.GetAxisRaw("Accept");
				case TriggerAxis.Cancel:
					return CrossPlatformInputManager.GetAxisRaw("Cancel");
				case TriggerAxis.Switch:
					return CrossPlatformInputManager.GetAxisRaw("Switch");
				case TriggerAxis.Menu:
					return CrossPlatformInputManager.GetAxisRaw("Menu");
				case TriggerAxis.Jump:
					return CrossPlatformInputManager.GetButton("Jump") ? 1 : 0;
				case TriggerAxis.Click:
					return CrossPlatformInputManager.GetButton("Click") ? 1 : 0;
				default:
					return 0;
				}
			}

			/// <summary>
			/// Checa se os botoes correspondentes ao trigger estao pressionados
			/// </summary>
			private bool isPressed(TriggerAxis triggerAxis){
				switch(triggerAxis){
				case TriggerAxis.HorizontalAxis:
					return CrossPlatformInputManager.GetAxisRaw("Horizontal") != 0;
				case TriggerAxis.Up:
					return CrossPlatformInputManager.GetAxisRaw("Vertical") > 0;
				case TriggerAxis.Down:
					return CrossPlatformInputManager.GetAxisRaw("Vertical") < 0;
				default:
					return getAxisValue() != 0;
				}
			}

			/// <summary>
			/// Atualiza
			/// </summary>
			public virtual void update(){
				updateFlags();
				callEvent();
			}

			/// <summary>
			/// Atualiza as flags
			/// </summary>
			private void updateFlags(){
				myWasPressed = myIsPressed;
				myIsPressed = isPressed(triggerAxis);
			}

			/// <summary>
			/// Retorna o evento que deve ser ativado neste frame
			/// </summary>
			private TriggerEvent getEvent(){

				// Bifurca entre start e falseStart baseado no retorno do metodo startCondition()
				if(myIsPressed  && !myWasPressed){
					if(startCondition() && isCooldownReady()){
						chargeTime = charge + Time.time;
						return TriggerEvent.Start;
					}
					return TriggerEvent.FalseStart;
				} 

				if(myIsPressed  && myWasPressed && hasStarted)
					return TriggerEvent.Continue;

				// Bifurca entre releaseCharge e end baseado no valor de chargeValue
				if(!myIsPressed && myWasPressed){
					if(isCharged())
						return TriggerEvent.ReleaseCharge;
					return TriggerEvent.End;
				}

				if(!myIsPressed && !myWasPressed) return TriggerEvent.PostEnd;

				return TriggerEvent.none;
			}

			/// <summary>
			/// Chama o metodo de evento
			/// </summary>
			private void callEvent(){
				preStart();
				switch(getEvent()){
				case TriggerEvent.Start:
					hasStarted = true; start(); break;
				case TriggerEvent.FalseStart:
					hasStarted = false; falseStart(); break;
				case TriggerEvent.Continue:
					continuous(); break;
				case TriggerEvent.End:
					hasStarted = false; 
					if(!Knockback.isKnocked())
						end(); 
					break;
				case TriggerEvent.ReleaseCharge:
					hasStarted = false; 
					if(!Knockback.isKnocked())
						release(); 
					break;
				case TriggerEvent.PostEnd:
					postEnd(); break;
				default:
					break;
				}
			}

			/// <summary>
			/// Caso retorne false, interrompe o fluxo do trigger
			/// </summary>
			virtual protected void preStart(){} 

			/// <summary>
			/// Condiçao utilizada para alternar entre start_positive e start_negative
			/// </summary>
			/// <returns><c>true</c>, Chama start_positive, <c>false</c> Chama start_negative.</returns>
			virtual protected bool startCondition(){return true;}

			/// <summary>
			/// Eh iniciado quando o jogador aperta o botao do trigger
			/// </summary>
			virtual protected void start(){}

			/// <summary>
			/// Eh iniciado quando o jogador aperta o botao do trigger, mas as condiçoes de inicio da açao NAO sao propicias
			/// </summary>
			virtual protected void falseStart(){}

			/// <summary>
			/// Eh chamado todos os frames enquanto a tecla do trigger continuar pressionada apos seu inicio
			/// </summary>
			virtual protected void continuous(){}

			/// <summary>
			/// Eh chamado no momento em que a tecla de trigger eh solta
			/// </summary>
			virtual protected void end(){}

			/// <summary>
			/// Eh chamado no momento em que a tecla de trigger eh solta E o valor de carregamento deste trigger estah no maximo
			/// </summary>
			virtual protected void release(){}

			/// <summary>
			/// Eh chamado todos os frames enquanto a tecla continuar solta
			/// </summary>
			virtual protected void postEnd(){}

			/// <summary>
			/// Retorna um valor de zero a um representando o tempo de cooldown.
			/// Valor 1 significa que a habilidade estah pronta.
			/// </summary>
			public float getCooldown(){
				if(Time.time > cooldownTime) return 0;
				return  1 - ((Time.time - (cooldownTime - cooldown))/ cooldown);
			}

			/// <summary>
			/// Inicia o cooldown
			/// </summary>
			protected virtual void startCooldown(){
				cooldownTime = Time.time + cooldown;
			}

			/// <summary>
			/// Retorna true se a habilidade estiver pronta
			/// </summary>
			public bool isCooldownReady(){
				return cooldownTime<=Time.time;
			}

			/// <summary>
			/// Retorna true caso o trigger esteja completamente carregado
			/// </summary>
			public bool isCharged(){
				return Time.time > chargeTime && hasStarted && chargeable;
			}

			/// <summary>
			/// Retorna um valor de zero a um representando o tempo de carregamento.
			/// Valor 1 significa que a habilidade estah completamente carregada.
			/// </summary>
			public float getCharge(){
				if(Time.time > chargeTime) return 1;
				return  (Time.time - (chargeTime - charge) )/ charge;
			}
		}

		public enum TriggerAxis{
			HorizontalAxis,
			Up,
			Down,
			Accept,
			Cancel,
			Switch,
			Menu,
			Jump,
			Click,
			None
		}

		public enum TriggerEvent{
			none,
			Start,
			FalseStart,
			Continue,
			End,
			ReleaseCharge,
			PostEnd,

		}
	}
}