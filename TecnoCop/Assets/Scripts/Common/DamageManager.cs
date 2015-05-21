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

		protected virtual void applyDamage(){
			Health -= damage.power;
			knockback.receiveKnockback(damage.knockBackPower,0.5f);
			invulnerabilityEndTime = Time.time + invulnerabilityTime;
			damage = null;
			flashSprite();
			if(Health <= 0) die ();
			else playSound();
		}

		public bool isinvulnerable(){
			return Time.time<invulnerabilityEndTime;
		}

		protected void flashSprite(){
			spriteRenderer.material.SetColor("_FlashColor",Color.white);
			spriteRenderer.material.SetFloat("_FlashAmount",1.0f);
		}

		protected void flashSprite(Color color){
			spriteRenderer.material.SetColor("_FlashColor",color);
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
	}
}

