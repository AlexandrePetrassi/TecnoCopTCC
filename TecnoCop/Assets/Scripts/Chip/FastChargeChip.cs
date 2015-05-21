using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;
using TecnoCop.PlayerControl;

public class FastChargeChip : UpgradeChip {

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.player.GetComponent<Pistol>().cooldown = 0;
		Player.player.GetComponent<Pistol>().charge = Player.player.GetComponent<Pistol>().charge/3;
		Player.fastChargeSkill = true;
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.fastChargeSkill) Destroy(gameObject);
	}
}
