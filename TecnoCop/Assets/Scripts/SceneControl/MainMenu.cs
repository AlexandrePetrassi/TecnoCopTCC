using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	GameObject creditsScreen;
	[SerializeField]
	GameObject optionsScreen;
	[SerializeField]
	GameObject aboutScreen;

	[SerializeField]
	Toggle[] languagesToggle;

	void OnEnable()
	{
		creditsScreen.SetActive (false);
		optionsScreen.SetActive (false);
		aboutScreen.SetActive (false);

		for (int i = 0; i < languagesToggle.Length; i++) {
			if(i == Translation.CurrentLanguage)
			{
				languagesToggle[i].isOn = true;
			}else{
				languagesToggle[i].isOn = false;
			}
		}

	}

	public void SetTranslation(int language)
	{
		switch (language) {
		case 0: // Portuguese
			PlayerPrefs.SetInt("Language",language);
			break;
		case 1: // English
			PlayerPrefs.SetInt("Language",language);
			break;
		default:
			language = 0;
			PlayerPrefs.SetInt("Language",language);
			break;
		}
		Translation.CurrentLanguage = language;
		var texts = GameObject.FindObjectsOfType<Translation> () as Translation[];
		for (int i = 0; i < texts.Length; i++) {
			texts[i].SetText();
		}
	}

	public void startButton(){
		TecnoCop.PlayerControl.PlayerDamageManager.playerHealth = 20;
		Application.LoadLevel("Intro");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void Credits_Click()
	{
		creditsScreen.SetActive (true);
	}

	public void About_Click()
	{
		aboutScreen.SetActive (true);
	}

	public void Options_Click()
	{
		optionsScreen.SetActive (true);
	}
	
}
