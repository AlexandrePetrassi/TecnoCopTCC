using UnityEngine;
using System.Collections;
using TecnoCop.Collisions;

public class UpgradeChip : Collideable {

	public float spin = 0.01f;
	SpriteRenderer spriteRenderer;
	float lastFlashTime;
	public float flashCooldown;
	public float cooldownVariation;
	public Color flashColor;
	float initialPosition;
	public float moveSpeed;
	public GameObject messageObject;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.material.SetColor("_FlashColor",flashColor);
		initialPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rotate();
		move();
		unflashSprite();
		if(lastFlashTime + flashCooldown < Time.time) flashSprite();
	}

	void move(){
		if(transform.position.y > initialPosition + 0.5f){
			if(moveSpeed > 0) moveSpeed *= -1;
		}
		if(transform.position.y < initialPosition){
			if(moveSpeed < 0) moveSpeed *= -1;
		}
		transform.position = new Vector3(0,moveSpeed,0) + transform.position;
	}

	void rotate(){
		if(transform.localScale.x > 1){
			transform.localScale = new Vector3(1,1,1);
			if(spin > 0) spin *= -1;
		}
		if(transform.localScale.x < -1){
			transform.localScale = new Vector3(-1,1,1);
			if(spin < 0) spin *= -1;
		}
		transform.localScale = transform.localScale + new Vector3(spin,0,0);
	}

	void flashSprite(){
		spriteRenderer.material.SetFloat("_FlashAmount",1.0f);
		lastFlashTime = Time.time - Random.Range(0,cooldownVariation);
	}
	
	void unflashSprite(){
		float amount = spriteRenderer.material.GetFloat("_FlashAmount") - 0.05f;
		spriteRenderer.material.SetFloat("_FlashAmount",amount > 0? amount: 0);
	}

	void showUpgradeMessage(){

	}
}
