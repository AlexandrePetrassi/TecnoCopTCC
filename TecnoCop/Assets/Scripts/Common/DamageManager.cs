using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	public class DamageManager : Trigger{
		[Tooltip("Prefab de animaçao de destruiçao deste objeto")]
		public GameObject deathPrefab;

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

		float health;

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


		protected override bool getTriggerInput ()
		{
			return damage != null && !isinvulnerable();
		}

		protected override void preStart ()
		{
			unflashSprite();
		}

		protected override void start(){
			applyDamage();
		}

		private void applyDamage(){
			Health -= damage.power;
			knockback.receiveKnockback(damage.knockBackPower,0.5f);
			invulnerabilityEndTime = Time.time + invulnerabilityTime;
			damage = null;
			flashSprite();
			if(health <= 0) die ();
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

		protected virtual void die(){
			if(deathPrefab != null)
				Instantiate(deathPrefab,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
}

