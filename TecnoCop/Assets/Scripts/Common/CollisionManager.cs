using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

namespace TecnoCop{
	/// <summary>
	/// Collision manager.
	/// Gerencia a colisao entre os diferentes CollisionDetectors do objeto
	/// </summary>
	public abstract class CollisionManager: ModuleManager, IUpdatable{
		[Header("Generic")]
		[HideInInspector]public CollisionDetector feet;   // Usado para detectar colisoes com o chao
		[HideInInspector]public CollisionDetector head;   // Usado para detectar colisoes com o teto
		[HideInInspector]public CollisionDetector front;  // Usado para detectar colisoes com paredes
		
		public Rect feetRect;  // Posiçao e tamanho dos pes do personagem
		public Rect headRect;  // Posiçao e tamanho da cabeça do personagem
		public Rect frontRect; // posiçao e tamanho do collider frontal do personagem

		/// <summary>
		/// Cria um collider para gerenciar colisoes em uma parte especifica do corpo do personagem
		/// Este metodo recebe apenas como tipo CollisionDetector ou DamageCollider
		/// </summary>
		public CollisionDetector makeCollider(string name, Rect rect){
			return buildGameObject(name,rect).AddComponent<CollisionDetector>();
		}

		public DamageCollider makeCollider(string name, Rect rect, float power, DamageType type){
			DamageCollider newCollider = buildGameObject(name,rect).AddComponent<DamageCollider>();
			newCollider.damage = new Damage(power,type,1);
			return newCollider;
		}

		private GameObject buildGameObject(string name, Rect rect){
			GameObject myCollider =new GameObject();
			myCollider.name = name;
			myCollider.tag = tag;
			setPosition(myCollider,new Vector3(rect.x    ,rect.y));
			setSize    (myCollider,new Vector2(rect.width,rect.height));

			return myCollider;
		}

		private void setPosition(GameObject myCollider, Vector3 position){
			myCollider.transform.position = transform.position + position;
			myCollider.transform.parent = transform;
		}

		private void setSize(GameObject myCollider, Vector2 size){
			BoxCollider2D boxCollider = myCollider.AddComponent<BoxCollider2D>();
			boxCollider.size = size;
			boxCollider.isTrigger = true;
		}

		public abstract void update();
	}
}

