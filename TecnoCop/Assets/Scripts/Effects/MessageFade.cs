using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageFade : MonoBehaviour {

	float bornTime;
	public float waitTime = 3;
	public Image image;
	Text[] texts;
	// Use this for initialization
	void Start () {
		bornTime = Time.time;
		texts = GetComponentsInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(bornTime + waitTime < Time.time){
			image.color = image.color - new Color(0,0,0,0.01f);
			foreach(Text text in texts) text.color = text.color - new Color(0,0,0,0.01f);
		}
		if(image.color.a <= 0)
			Destroy(gameObject);
	}
}
