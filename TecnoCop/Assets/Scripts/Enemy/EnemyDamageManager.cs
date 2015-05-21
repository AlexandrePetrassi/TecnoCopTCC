using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyDamageManager : DamageManager {

			public GameObject drop;
			public float dropRate = 0.5f;

			protected override void die ()
			{
				float roll = Random.Range(0.0f,1.0f);
				if(drop != null && roll <= dropRate)
					Instantiate(drop, transform.position,Quaternion.identity);
				base.die ();
			}
		}
	}
}

