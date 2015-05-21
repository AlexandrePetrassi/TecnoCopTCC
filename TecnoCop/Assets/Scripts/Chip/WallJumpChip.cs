using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class WallJumpChip : UpgradeChip {

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.wallJumpSkill = true;
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.wallJumpSkill) Destroy(gameObject);
	}
}
