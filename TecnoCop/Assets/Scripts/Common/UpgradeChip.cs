using UnityEngine;
using System.Collections;

public class UpgradeChip : MonoBehaviour {

	public float spin = 0.01f;
	SpriteRenderer spriteRenderer;
	float lastFlashTime;
	public float flashCooldown;
	public float cooldownVariation;
	public Color flashColor;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.material.SetColor("_FlashColor",flashColor);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.localScale.x > 1){
			transform.localScale = new Vector3(1,1,1);
			if(spin > 0) spin *= -1;
		}
		if(transform.localScale.x < -1){
			transform.localScale = new Vector3(-1,1,1);
			if(spin < 0) spin *= -1;
		}
		transform.localScale = transform.localScale + new Vector3(spin,0,0);
		unflashSprite();
		if(lastFlashTime + flashCooldown < Time.time) flashSprite();
	}

	void flashSprite(){
		spriteRenderer.material.SetFloat("_FlashAmount",1.0f);
		lastFlashTime = Time.time - Random.Range(0,cooldownVariation);
	}
	
	void unflashSprite(){
		float amount = spriteRenderer.material.GetFloat("_FlashAmount") - 0.05f;
		spriteRenderer.material.SetFloat("_FlashAmount",amount > 0? amount: 0);
	}
}
