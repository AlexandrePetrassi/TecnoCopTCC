using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace TecnoCop{
	namespace PlayerControl{
		/// <summary>
		/// Player damage manager.
		/// Gerencia o dano que o personagem do jogador recebe
		/// </summary>
		public class PlayerDamageManager : DamageManager {
			[Header("Specific")]
			[SerializeField]
			Slider healthBar;

			public static float playerHealth;
			public override float Health {
				get {
					//return base.Health;
					return playerHealth;
				}
				set {
					//base.Health = value;
					playerHealth = value;
					if(healthBar == null) healthBar = GameObject.FindGameObjectWithTag("Lifebar").GetComponent<Slider>();
					healthBar.value = value / MaxHealth;
				}
			}

			void OnLevelWasLoaded(){
				healthBar = null;
				Health = playerHealth;
			}

			protected override void die ()
			{
				Camera.main.transform.SetParent(null);
				Instantiate(Resources.Load("Prefabs/Abstract/GameOver"));
				base.die ();
			}
		}
	}
}

