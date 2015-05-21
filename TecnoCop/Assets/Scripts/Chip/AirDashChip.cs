using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;
using TecnoCop.PlayerControl;

public class AirDashChip : UpgradeChip {

	public override void collideOnEnter(CollisionDetector collider){
		if(collider.gameObject.tag != "Player" || collider.gameObject.transform.parent.tag != "Player") return;
		Player.dash.airDashEnabled = true;
		Player.airDashSkill = true;
		Instantiate(messageObject);
		Destroy(gameObject);
	}
	
	void Start(){
		if(Player.airDashSkill) Destroy(gameObject);
	}
}
