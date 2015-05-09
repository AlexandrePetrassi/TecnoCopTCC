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

		private float health;

		public virtual float Health {
			get {
				return health;
			}
			set {
				health = value;
			}
		}

		private Damage damage;

		void OnEnable()
		{
			Health = MaxHealth;
		}

		public void update(){
			consumeDamage(collision.feet);
			consumeDamage(collision.head);
			consumeDamage(collision.front);
			applyDamage();
		}

		public void consumeDamage(CollisionDetector detector){
			if(damage == null && detector.damage != null) 
				damage = detector.damage;
			detector.damage = null;
		}

		private void applyDamage(){

			if(damage != null && !Knockback.isKnocked(this)){
				Health -= damage.power;
				knockback.receiveKnockback(damage.knockBackPower,0.5f);
			}
			damage = null;
		}
	}
}

