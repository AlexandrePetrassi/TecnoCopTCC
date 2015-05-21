using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroController : MonoBehaviour {

	public RectTransform rt;
	float bornTime;
	public float skipTime = 1;

	// Use this for initialization
	void Start () {
		bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		rt.Translate(new Vector3(0,0.007f,0));
		if((bornTime + skipTime < Time.time && Input.anyKeyDown) || rt.anchoredPosition.y > 1700)
			Application.LoadLevel("Level2");
	}
}
