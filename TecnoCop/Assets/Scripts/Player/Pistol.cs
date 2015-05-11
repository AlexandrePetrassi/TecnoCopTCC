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
				//var bullet = Instantiate(normalShot,transform.position,Quaternion.Euler(0,0,angleBetweenMouse())) as GameObject;
				var bullet = Instantiate(normalShot,transform.position,Quaternion.Euler(0,0,transform.localScale.x>=0?0:180)) as GameObject;
				bullet.tag = "Player";
			}

			protected override void release(){
				//var bullet = Instantiate(chargedShot,transform.position,Quaternion.Euler(0,0,angleBetweenMouse())) as GameObject;
				var bullet = Instantiate(chargedShot,transform.position,Quaternion.Euler(0,0,transform.localScale.x>=0?0:180)) as GameObject;
				bullet.tag = "Player";
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

