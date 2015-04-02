using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class MenuLevelManager : MonoBehaviour {

	public GridLayoutGroup gridLevels;
	public GameObject levelPrefab;
	public int numberLevels;
	public Sprite[] levelsImages;
	private bool[] levelCompleted;
	public Sprite levelNotCompletedImage;


	// Use this for initialization
	void Start () 
	{
		string[] levels = PlayerPrefs.GetString("Levels","").Split(',');

		if(levels.Length < this.numberLevels)
			GenerateNewLevels();
		else
			RecoverLevels();

	}

	/// <summary>
	/// Generates the new levels if the player prefs has not all the levels
	/// </summary>
	private void GenerateNewLevels()
	{
		string levelsPlayerPrefs = PlayerPrefs.GetString("Levels","");
		string[] levels = levelsPlayerPrefs.Split(',');
		RecoverLevels();
		StringBuilder sb = new StringBuilder(levelsPlayerPrefs);

		for(int i = levels.Length; i < this.numberLevels; i++)
		{
			InstantiateLevel("Level"+i, i);
			sb.Append(i+",");
		}

		PlayerPrefs.SetString("Levels", sb.ToString());

	}

	/// <summary>
	/// Recovers the levels.
	/// </summary>
	private void RecoverLevels()
	{
		string[] levels = PlayerPrefs.GetString("Levels","").Split(',');

		for(int i = 0; i < levels.Length; i++)
			if(!string.IsNullOrEmpty(levels[i]))
				InstantiateLevel(levels[i],i);
	}

	/// <summary>
	/// Instantiates the level and adds it to the grid
	/// </summary>
	/// <param name="levelName">Level name.</param>
	private void InstantiateLevel(string levelName, int spriteIndex)
	{
		GameObject levelGo = (GameObject) Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
		Level level = levelGo.GetComponent<Level>();
		if(!level.RecoverLevel(levelName))
		{
			level.Id = levelName;
			level.IsCompleted = false;
			level.SpriteIndex = spriteIndex;
			level.StoreLevel();
		}
		levelGo.transform.parent = gridLevels.transform;
		levelGo.transform.localScale = Vector3.one;
		levelGo.GetComponentInChildren<Text>().text = "Level "+level.Id;
		if(level.IsCompleted)
			levelGo.GetComponentInChildren<Image>().sprite = this.levelsImages[int.Parse (level.Id)-1];
		else
			levelGo.GetComponentInChildren<Image>().sprite = this.levelNotCompletedImage;
	}

	public void LoadLevel(string id)
	{
		PlayerPrefs.SetString("LevelToLoad", id);
		Application.LoadLevel("Game");
	}
}
