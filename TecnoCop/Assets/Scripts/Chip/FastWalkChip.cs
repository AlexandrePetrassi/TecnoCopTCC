using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;
using TecnoCop.PlayerControl;

public class FastWalkChip : UpgradeChip {
	
	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		((Move)Player.player.move).speed *= 1.5f;
		Player.fastWalkSkill = true;
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.fastWalkSkill) Destroy(gameObject);
	}
}