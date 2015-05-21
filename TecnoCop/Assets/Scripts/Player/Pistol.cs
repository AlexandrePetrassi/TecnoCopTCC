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
			Image cdIcon;
			Image cooldownIcon{
				get{
					if(cdIcon == null) cdIcon = GameObject.FindGameObjectWithTag("ShootIcon").GetComponent<Image>();
					return cdIcon;
				}
				set{
					cdIcon = value;
				}
			}
			[SerializeField]
			Image chIcon;
			Image chargeIcon{
				get{
					if(chIcon == null) chIcon = GameObject.FindGameObjectWithTag("ChargeIcon").GetComponent<Image>();
					return chIcon;
				}
				set{
					chIcon = value;
				}
			}

			void OnLevelWasLoaded(){
				cdIcon = null;
				chIcon = null;
			}

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
				if (!WallStick.isWallSticking ()) {
					var bullet = Instantiate(normalShot,transform.position + new Vector3(0,-0.1f,0),Quaternion.Euler(0,0,getBulletDirection())) as GameObject;
					bullet.GetComponent<DamageCollider>().parentGameObject = gameObject;
					bullet.tag = "Player";
					if(chargeIcon)
						chargeIcon.fillAmount = Mathf.Lerp(0, 1, 0);
					animator.SetTrigger ("Shoot");
					if(Dash.isDashing()) Player.dash.cancelDash();
					playSound();
				}
			}

			protected override void release(){
				if (!WallStick.isWallSticking ()) {
					startCooldown ();
					var bullet = Instantiate(chargedShot,transform.position+ new Vector3(0,-0.1f,0),Quaternion.Euler(0,0,getBulletDirection())) as GameObject;
					bullet.GetComponent<DamageCollider>().parentGameObject = gameObject;
					bullet.tag = "Player";
					animator.SetTrigger ("Shoot");
					playSound(0.5f,0.75f,1);
				}
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

