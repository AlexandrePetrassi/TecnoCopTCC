using UnityEngine;
using System.Collections;
using TecnoCop.Effects;
using UnityEngine.UI;
using TecnoCop.Collisions;

namespace TecnoCop{
	namespace PlayerControl{
		public class Pistol : PlayerTrigger {
			[Header("Specific")]
			public GameObject normalShot;  // Tiro nao carregado
			public GameObject chargedShot; // Tiro carregado

			[SerializeField]
			Image cooldownIcon;
			[SerializeField]
			Image chargeIcon;

			protected override void startCooldown ()
			{
				base.startCooldown ();
				StartCoroutine (UpdateCooldownIcon (cooldownIcon));
			}
			
			IEnumerator UpdateCooldownIcon(Image icon)
			{
				if (!icon)
					yield break;
				float totalTime = cooldown;
				float currentTime = totalTime;
				while (currentTime > 0) {
					icon.fillAmount = Mathf.Lerp(0, 1, getCooldown());
					currentTime -= Time.deltaTime;
					yield return new WaitForEndOfFrame();
				}
				icon.fillAmount = Mathf.Lerp(0, 1, 0);
			}


			protected override void end(){
				var bullet = Instantiate(normalShot,transform.position,Quaternion.Euler(0,0,getBulletDirection())) as GameObject;
				bullet.GetComponent<DamageCollider>().parentGameObject = gameObject;
				bullet.tag = "Player";
				if(chargeIcon)
					chargeIcon.fillAmount = Mathf.Lerp(0, 1, 0);
			}

			protected override void release(){
				startCooldown ();
				var bullet = Instantiate(chargedShot,transform.position,Quaternion.Euler(0,0,getBulletDirection())) as GameObject;
				bullet.GetComponent<DamageCollider>().parentGameObject = gameObject;
				bullet.tag = "Player";
			}

			protected override void continuous ()
			{
				base.continuous ();
				if(chargeIcon)
					chargeIcon.fillAmount = Mathf.Lerp(0, 1, getCharge());
			}

			private float angleBetweenMouse(){
				Vector3 mousePos = Input.mousePosition;
				Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
				Vector3 delta = mousePos -pos;
				return Mathf.Atan2(delta.y,delta.x) * Mathf.Rad2Deg;
				
			}

			private float getBulletDirection(){
				return (transform.localScale.x>=0?0:180) + (WallStick.isWallSticking()?180:0);
			}
		}
	}
}

