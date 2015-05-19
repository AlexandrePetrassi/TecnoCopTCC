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

			public override float Health {
				get {
					return base.Health;
				}
				set {
					base.Health = value;
					healthBar.value = value / MaxHealth;
				}
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

