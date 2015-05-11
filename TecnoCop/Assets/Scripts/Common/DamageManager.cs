using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	public class DamageManager : ModuleManager, IUpdatable {
		[SerializeField]
		float maxHealth;

		public float MaxHealth {
			get {
				return maxHealth;
			}
			private set {
				maxHealth = value;
			}
		}

		public float health;

		public virtual float Health {
			get {
				return health;
			}
			set {
				health = value;
			}
		}

		Damage myDamage;
		public Damage damage{
			get{
				return myDamage;
			}
			set{
				myDamage = value;
			}
		}

		void OnEnable()
		{
			Health = MaxHealth;
		}

		public float invulnerabilityTime = 0;
		private float invulnerabilityEndTime = 0;

		public void update(){
			if(damage != null && !isinvulnerable()) applyDamage();
			unflashSprite();
		}

		private void applyDamage(){
			Health -= damage.power;
			knockback.receiveKnockback(damage.knockBackPower,0.5f);
			invulnerabilityEndTime = Time.time + invulnerabilityTime;
			damage = null;
			flashSprite();
		}

		public bool isinvulnerable(){
			return Time.time<invulnerabilityEndTime;
		}

		void flashSprite(){
			spriteRenderer.material.SetFloat("_FlashAmount",1.0f);
		}

		void unflashSprite(){
			float amount = spriteRenderer.material.GetFloat("_FlashAmount") - 0.05f;
			spriteRenderer.material.SetFloat("_FlashAmount",amount > 0? amount: 0);
		}
	}
}

