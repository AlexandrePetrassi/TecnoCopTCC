using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	public class DamageManager : ModuleManager, IUpdatable {

		public float health;
		private Damage damage;

		public void update(){
			consumeDamage(collision.feet);
			consumeDamage(collision.head);
			consumeDamage(collision.front);
			applyDamage();
		}

		public void consumeDamage(CollisionDetector detector){
			if(damage == null && detector.damage != null) damage = detector.damage;
			detector.damage = null;
		}

		private void applyDamage(){

			if(damage != null && !Knockback.isKnocked(this)){
				health -= damage.power;
				knockback.receiveKnockback(damage.knockBackPower,0.5f);
			}
			damage = null;
		}
	}
}

