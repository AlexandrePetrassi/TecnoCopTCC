using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Translation : MonoBehaviour {
	[Multiline]
	[SerializeField]
	string []translations;

	Text text;

	static int currentLanguage = -1;

	public static int CurrentLanguage
	{
		get{
			if(currentLanguage == -1)
			{
				if(!PlayerPrefs.HasKey("Language"))
				{
					PlayerPrefs.SetInt("Language",0);
				}
				currentLanguage = PlayerPrefs.GetInt("Language");
			}
			return currentLanguage;
		}
		set{
			currentLanguage = value;
		}
	}

	void Awake()
	{
		text = GetComponent<Text> ();
	}

	void OnEnable()
	{
		SetText ();
	}

	public void SetText()
	{
		if (text == null) {
			text = GetComponent<Text> ();
		}
		text.text = translations [CurrentLanguage];
	}
	
}
