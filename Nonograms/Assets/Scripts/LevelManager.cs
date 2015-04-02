using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		FindObjectOfType<PanelValidator>().onPanelValidated += onPanelValidated;	
	}
	

	void onPanelValidated(bool isValid)
	{
		if(isValid)
		{
			GameObject go = new GameObject("LevelCompleted");
			Level level = go.AddComponent<Level>();
			if(level.RecoverLevel(PlayerPrefs.GetString("LevelToLoad","0")))
			{
				level.IsCompleted = true;
				level.StoreLevel();
				Application.LoadLevel("LevelPicker");
			}
		}
	}
}
