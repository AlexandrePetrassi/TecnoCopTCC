using UnityEngine;
using System.Collections;

public class MusicSingleton : MonoBehaviour {

	private static MusicSingleton instance;
	
	public static MusicSingleton GetInstance(){
		return instance;
	}
	
	void Awake() {
		if (instance != null && instance != this) {
			if(instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip){
				instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
				instance.GetComponent<AudioSource>().Play();
			}
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
