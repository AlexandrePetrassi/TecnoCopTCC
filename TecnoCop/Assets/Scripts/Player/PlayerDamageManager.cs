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

			public AudioClip healthSound;

			public static float playerHealth;
			public override float Health {
				get {
					//return base.Health;
					return playerHealth;
				}
				set {
					//base.Health = value;
					playerHealth = Mathf.Clamp(value,0,MaxHealth);
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

			public void heal(float healing){
				playSound(1,2,0.5f,healthSound);
				Health += healing;
				flashSprite(Color.green + Color.gray);
			}

			protected virtual void playSound(float min,float max, float vol,AudioClip clip){
				if(clip == null) return;
				audioS.clip = clip;
				audioS.pitch = Random.Range(min,max);
				audioS.volume = vol;
				audioS.Play();
			}
		}
	}
}

