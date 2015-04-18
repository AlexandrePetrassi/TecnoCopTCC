using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Effects{
		public class DashParticle : TransparentParticle {

			// Use this for initialization
			void Start () {
				spriteRenderer.sprite = Player.player.spriteRenderer.sprite;
				transform.position = Player.player.transform.position;
				transform.localScale = Player.player.transform.localScale;
				transform.rotation = Player.player.transform.rotation;
			}
		}
	}
}
