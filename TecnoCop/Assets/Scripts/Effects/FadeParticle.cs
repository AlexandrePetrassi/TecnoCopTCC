using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Effects{
		[RequireComponent(typeof(SpriteRenderer))]
		public class FadeParticle : MonoBehaviour {

			protected SpriteRenderer spriteRenderer;
			public float lifetime;
			
			// Use this for initialization
			void Awake () {
				spriteRenderer = GetComponent<SpriteRenderer>();
				Destroy(gameObject,lifetime);
			}
		}
	}
}
