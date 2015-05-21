using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class DoubleJumpChip : UpgradeChip {

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.jump.maxJump = 2;
		Player.doubleJumpSkill = true;
		Instantiate(messageObject);
		Destroy(gameObject);
	}

	void Start(){
		if(Player.doubleJumpSkill) Destroy(gameObject);
	}
}
