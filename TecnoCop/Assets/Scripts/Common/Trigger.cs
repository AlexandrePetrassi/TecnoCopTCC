using UnityEngine;
using System.Collections;

namespace TecnoCop{
	public abstract class Trigger : ModuleManager, IUpdatable {
		[Header("General")]
		private bool myIsTriggered;     // Checa se o trigger esta ativo
		private bool myWasTriggered;    // Checa se o trigger jah estava ativo anteriormente
		protected bool hasStarted;        // Checa se o trigger realmente se iniciou

		[Tooltip("Tempo de cooldown deste comando \n(Valores negativos ou zero significam 'sem cooldown')")]
		public float cooldown;
		private float cooldownTime;       // Momento no tempo em que o cooldown estarah pronto

		protected abstract bool getTriggerInput(); // Equivalente ao isPressed do playerTrigger

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
			myWasTriggered = myIsTriggered;
			myIsTriggered = getTriggerInput();
		}
		
		/// <summary>
		/// Retorna o evento que deve ser ativado neste frame
		/// </summary>
		private TriggerEvent getEvent(){
			
			// Bifurca entre start e falseStart baseado no retorno do metodo startCondition()
			if(myIsTriggered  && !myWasTriggered){
				if(startCondition() && isCooldownReady()){
					hasStarted = true;
					return TriggerEvent.Start;
				}
				return TriggerEvent.FalseStart;
			} 
			
			if(myIsTriggered  && myWasTriggered && hasStarted){
				return TriggerEvent.Continue;
			}
			
			// Bifurca entre releaseCharge e end baseado no valor de chargeValue
			if(!myIsTriggered && myWasTriggered){
				hasStarted = false;
				return TriggerEvent.End;
			}
			
			if(!myIsTriggered && !myWasTriggered) return TriggerEvent.PostEnd;
			
			return TriggerEvent.none;
		}
		
		/// <summary>
		/// Chama o metodo de evento
		/// </summary>
		private void callEvent(){
			preStart();
			switch(getEvent()){
			case TriggerEvent.Start:
				start(); break;
			case TriggerEvent.FalseStart:
				falseStart(); break;
			case TriggerEvent.Continue:
				continuous(); break;
			case TriggerEvent.End:
				end(); break;
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
		/// Eh chamado todos os frames enquanto a tecla continuar solta
		/// </summary>
		virtual protected void postEnd(){}
		
		/// <summary>
		/// Retorna um valor de zero a um representando o tempo de cooldown.
		/// Valor 1 significa que a habilidade estah pronta.
		/// </summary>
		/// <returns>The cooldown.</returns>
		public float getCooldown(){
			if(Time.time > cooldownTime) return 1;
			return  (Time.time - cooldownTime - cooldown)/ cooldownTime;
		}
		
		/// <summary>
		/// Inicia o cooldown
		/// </summary>
		protected void startCooldown(){
			cooldownTime = Time.time + cooldown;
		}
		
		/// <summary>
		/// Retorna true se a habilidade estiver pronta
		/// </summary>
		public bool isCooldownReady(){
			return cooldownTime<=Time.time;
		}

		public enum TriggerEvent{
			none,
			Start,
			FalseStart,
			Continue,
			End,
			PostEnd
		}
	}
}
