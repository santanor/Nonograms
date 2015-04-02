using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelLoader : MonoBehaviour {

	public delegate void LevelEvent (Nonogram nonogram);

	public LevelEvent onLevelLoaded;
	public TextAsset[] levels;

	// Use this for initialization
	void Start () 
	{
		levels = levels.OrderBy(text => int.Parse(text.name.Replace("Level",""))).ToArray();
		int level = int.Parse(PlayerPrefs.GetString("LevelToLoad", "Level1"))-1;
		LoadLevel(level);
		FindObjectOfType<PanelValidator>().onPanelValidated += OnPanelValidated;
	}

	/// <summary>
	/// Loads the level from a TextAsset
	/// </summary>
	/// <returns>The level.</returns>
	/// <param name="levelNumber">Level number.</param>
	public void LoadLevel(int levelNumber)
	{
		string[] levelText = levels [levelNumber-1].text.Split ('\n');
		int sizeRows = int.Parse(levelText [0].Split ('x') [0]);
		int sizeCols = int.Parse (levelText [0].Split ('x') [1]);
		IList<string> rows = new List<string> ();
		IList<string> cols = new List<string>();

		//Iterate over the rows
		for (int i = 1; i < sizeRows +1; i++)
			rows.Add(levelText[i].Split(':')[1].Trim());

		//Iterate over the columns
		for(int i = sizeRows+1; i < sizeCols + sizeRows +1; i++)
			cols.Add(levelText[i].Split(':')[1].Trim());
		
		Nonogram nonogram = new Nonogram(sizeCols, sizeRows, rows.ToArray(), cols.ToArray());	
		if(onLevelLoaded != null)
			onLevelLoaded(nonogram);
	}

	private void OnPanelValidated(bool isValid)
	{
		Debug.Log(isValid);
	}
}
