using UnityEngine;
using System.Collections;
using TecnoCop.PlayerControl;
using TecnoCop.Collisions;

public class CameraController : MonoBehaviour {

	ScenarioManager scenarioManager;
	// Use this for initialization
	void Start () {
		scenarioManager = Camera.main.GetComponent<ScenarioManager>();
	}

	public Rect cBounds; // Limites da camera

	// Update is called once per frame
	void Update () {
		if(Player.player == null) return;
		Vector3 t = Player.player.transform.position;
		float x = Mathf.Clamp(t.x,cBounds.xMin,scenarioManager.map.width  - cBounds.width);
		float y = Mathf.Clamp(t.y,cBounds.yMin,scenarioManager.map.height - cBounds.height);
		transform.position = new Vector3(x,y,-10);
	}
}
