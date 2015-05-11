using UnityEngine;
using System.Collections;

namespace TecnoCop{
	/// <summary>
	/// Damage.
	/// Representa uma quantidade de dano que um personagem recebe.
	/// Dano pode ser de varios tipos e eh alterado em base as resistencias do personagem que o recebe
	/// </summary>
	public class Damage {

		public float power = 0;
		public DamageType type = DamageType.none;
		public float knockBackPower = 0.5f;

		public Damage(float power){
			this.power = power;
		}

		public Damage(float power, DamageType type):this(power){
			this.type = type;
		}

		public Damage(float power,DamageType type, float knockBackPower):this(power,type){
			this.knockBackPower = knockBackPower;
		}
	}
	public enum DamageType{
		blunt,
		pierce,
		slash,
		heat,
		cold,
		eletric,
		acid,
		none
	}


}
