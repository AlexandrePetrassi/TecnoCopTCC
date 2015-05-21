using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	float bornTime;
	public float WaitTime;
	public float fadeTime;
	public Image gameOverImage;

	// Use this for initialization
	void Start () {
		TecnoCop.PlayerControl.Player.bornPosition = TecnoCop.Direction.left;
		bornTime = Time.time;
		if(TecnoCop.PlayerControl.Player.player != null) Destroy(TecnoCop.PlayerControl.Player.player.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(bornTime + fadeTime < Time.time)
			gameOverImage.color = gameOverImage.color + new Color(0.025f,0.025f,0.025f);
		if(bornTime + WaitTime < Time.time || (Input.GetButtonDown("Click") && gameOverImage.color.r >= 1)){
			Application.LoadLevel("MainMenu");
		}
	}
}
