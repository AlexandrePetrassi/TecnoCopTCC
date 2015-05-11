using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	public class Knockback : Trigger {
		[Header("Specific")]
		public float  multiplier = 1; // Multiplicador do Knockback. Pode ser usado para diminuir a intensidade caso os valores estejam entre 0 e 1;
		private float power     = 0;  // Força de Knockback que sera aplicada a este personagem
		public float  endTime   = 0;  // Momento no tempo em que o Knockback acabarah

		protected override bool getTriggerInput(){
			return isKnocked(this);
		}

		protected override bool startCondition(){
			return isKnocked(this);
		}

		protected override void start(){
			applyKnockback();
		}

		protected override void continuous(){
			applyKnockback();
		}
		
		public void receiveKnockback(float power,float time){
			this.power = power * multiplier;
			this.endTime = time + Time.time;
		}

		private void applyKnockback(){
			move.setVelocity_x(power * -Mathf.Sign(transform.localScale.x),3);
		}

		public static bool isKnocked(){
			return (Player.player.knockback != null)? (Player.player.knockback.endTime > Time.time): false;
		}

		public static bool isKnocked(ModuleManager character){
			return (character.knockback != null)? (character.knockback.endTime > Time.time): false;
		}
	}
}

