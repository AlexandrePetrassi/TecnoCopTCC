using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class WallJumpChip : UpgradeChip {

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.player.wallJumpSkill = true;
		Instantiate(messageObject);
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.player.wallJumpSkill) Destroy(gameObject);
	}
}
