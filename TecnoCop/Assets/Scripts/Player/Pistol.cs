using UnityEngine;
using System.Collections;
using TecnoCop.Effects;

namespace TecnoCop{
	namespace PlayerControl{
		public class Pistol : PlayerTrigger {
			[Header("Specific")]
			public GameObject normalShot;  // Tiro nao carregado
			public GameObject chargedShot; // Tiro carregado

			protected override void end(){
				Instantiate(normalShot,transform.position,Quaternion.Euler(0,0,angleBetweenMouse()));
			}

			protected override void release(){
				Instantiate(chargedShot,transform.position,Quaternion.Euler(0,0,angleBetweenMouse()));
			}

			private float angleBetweenMouse(){
				Vector3 mousePos = Input.mousePosition;
				Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
				Vector3 delta = mousePos -pos;
				return Mathf.Atan2(delta.y,delta.x) * Mathf.Rad2Deg;
			}
		}
	}
}

