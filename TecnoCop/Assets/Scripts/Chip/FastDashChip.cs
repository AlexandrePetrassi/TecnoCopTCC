using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;
using TecnoCop.PlayerControl;

public class FastDashChip : UpgradeChip {
	
	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.dash.cooldown = Player.dash.cooldown/2;
		Player.player.fastDashSkill = true;
		Instantiate(messageObject);
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.player.fastDashSkill) Destroy(gameObject);
	}
}
