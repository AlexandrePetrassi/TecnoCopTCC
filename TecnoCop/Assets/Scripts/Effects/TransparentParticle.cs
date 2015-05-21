using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Effects{
		public class TransparentParticle : FadeParticle {

			public float transparencyFade;
			public float pmin, pmax;

			void Update(){
				spriteRenderer.color = spriteRenderer.color - new Color(0,0,0,transparencyFade);
				if(spriteRenderer.color.a <= 0) Destroy(gameObject);
			}

			void Start(){
				AudioSource audioS = GetComponent<AudioSource>();
				audioS.pitch = Random.Range(pmin,pmax);
				audioS.volume = 10/Vector3.Distance(transform.position,Player.player.transform.position);
				audioS.Play();
			}
		}
	}
}
