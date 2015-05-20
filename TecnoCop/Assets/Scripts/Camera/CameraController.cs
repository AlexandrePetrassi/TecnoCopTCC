using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class CameraController : MonoBehaviour {

	ScenarioManager scenarioManager;
	[Header("Limite da camera")]
	public Rect cBounds; // Limites da camera
	[Header("Indice das cenas adjacentes")]
	public int right = 1;
	public int left = 1;
	public int down = 1;
	public int up = 1;

	// Use this for initialization
	void Start () {
		scenarioManager = Camera.main.GetComponent<ScenarioManager>();
	}

	// Update is called once per frame
	void Update () {
		if(Player.player == null) return;
		lockCamera();
		teleport();
	}

	// Trava a camera nas extremidades para nao exibir o background
	void lockCamera(){
		Vector3 t = Player.player.transform.position;
		float x = Mathf.Clamp(t.x,cBounds.xMin,scenarioManager.map.width  - cBounds.width);
		float y = Mathf.Clamp(t.y,cBounds.yMin,scenarioManager.map.height - cBounds.height);
		transform.position = new Vector3(x,y,-10);
	}

	// Teletransporta o player para o cenario adjacente
	void teleport(){
		Vector3 t = Player.player.transform.position;
		if(t.x < 0){
			Player.bornPosition = TecnoCop.Direction.right;
			Application.LoadLevel(left);
		} 
		else if(t.x > scenarioManager.map.width -1){
			Player.bornPosition = TecnoCop.Direction.left;
			Application.LoadLevel(right);
		}
		else if(t.y < 0){
			Player.bornPosition = TecnoCop.Direction.up;
			Application.LoadLevel(down);
		} 
		else if(t.y > scenarioManager.map.height -1){
			Player.bornPosition = TecnoCop.Direction.down;
			Application.LoadLevel(up);
		} 
	}
}
