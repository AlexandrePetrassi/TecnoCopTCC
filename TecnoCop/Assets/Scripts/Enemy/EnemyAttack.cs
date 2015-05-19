using UnityEngine;
using System.Collections;

using TecnoCop.PlayerControl;

namespace TecnoCop{
	namespace Enemy{
		public class EnemyAttack : Trigger{

			public float x_range = 10;// Campo de visao do inimigo no eixo X
			public float y_range = 2; // Campo de visao do inimigo do eixo Y
			public GameObject shotPrefab;

			protected override bool getTriggerInput ()
			{
				if(getCooldown() != 1) return false;
				return checkCollision();
			}

			protected override bool startCondition ()
			{
				return checkCollision();
			}

			protected override void start ()
			{
				startCooldown();
				GameObject shot = Instantiate(shotPrefab,transform.position+new Vector3(3.2f * (transform.localScale.x>0?1:-1),1.1f,0),Quaternion.Euler(new Vector3(0,0,transform.localScale.x>0?0:180))) as GameObject;
				shot.transform.SetParent(transform);
			}

			/// <summary>
			/// Checa se o personagem do jogador esta no campo de visao
			/// </summary>
			/// <returns><c>true</c>, if collision was checked, <c>false</c> otherwise.</returns>
			bool checkCollision(){
				if(Player.player == null) return false;
				float px = Player.player.transform.position.x;
				float tx = transform.position.x;
				float py = Player.player.transform.position.y;
				float ty = transform.position.y;
				if(transform.localScale.x>0){
					if(px > tx + x_range)   return false;
					if(px < tx)           return false;
				}else{
					if(px < tx - x_range)   return false;
					if(px > tx)           return false;
				}
				if(py > ty + y_range) return false;
				if(py < ty) return false;
				return true;
			}
		}
	}
}
