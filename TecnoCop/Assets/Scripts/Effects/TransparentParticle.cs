using UnityEngine;
using System.Collections;

namespace TecnoCop{
	namespace Effects{
		public class TransparentParticle : FadeParticle {

			public float transparencyFade;
			
			void Update(){
				spriteRenderer.color = spriteRenderer.color - new Color(0,0,0,transparencyFade);
				if(spriteRenderer.color.a <= 0) Destroy(gameObject);
			}
		}
	}
}
