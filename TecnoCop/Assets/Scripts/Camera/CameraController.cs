using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class CameraController : MonoBehaviour {

	ScenarioManager scenarioManager;
	public Rect cBounds; // Limites da camera

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
		if(t.x < 0) Debug.Log("Left");
		else if(t.x > scenarioManager.map.width -1) Debug.Log("Right");
		else if(t.y < 0) Debug.Log("Down");
		else if(t.y > scenarioManager.map.height -1) Debug.Log("Up");
	}
}
