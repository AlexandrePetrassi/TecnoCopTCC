using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;
using TecnoCop.PlayerControl;

public class EnergyDrink : Collideable {

	public float healing = 3;

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.player.damager.SendMessage("heal",healing);
		Destroy(gameObject);
	}

}
