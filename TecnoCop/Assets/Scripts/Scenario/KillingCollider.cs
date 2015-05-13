using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

public class KillingCollider : MonoBehaviour {

	[Tooltip("Tag alvo. Destroi todos os objetos desta tag que entrarem em contato com este objeto")]
	public string targetTag;

	void OnTriggerEnter2D(Collider2D collider){
		//if(collider.tag == targetTag){
			DamageCollider dmg = collider.GetComponent<DamageCollider>();
			if(dmg != null) 
			if(dmg.deleteOnCollision) dmg.onCollisionDestroy();
		//}
	}
}
