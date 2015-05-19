using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	GameObject creditsScreen;
	[SerializeField]
	GameObject optionsScreen;
	[SerializeField]
	GameObject aboutScreen;
	[SerializeField]
	GameObject helpScreen;


	void OnEnable()
	{
		creditsScreen.SetActive (false);
		optionsScreen.SetActive (false);
		aboutScreen.SetActive (false);
		helpScreen.SetActive (false);
	}



	public void startButton(){
		Application.LoadLevel("Level1");
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

	public void Help_Click()
	{
		helpScreen.SetActive (true);
	}
}
