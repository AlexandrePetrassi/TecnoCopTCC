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
		}
	}
}

