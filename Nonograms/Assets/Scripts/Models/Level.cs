using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public string Id {get;set;}
	public bool IsCompleted {get; set;}
	public int SpriteIndex {get; set;}

	/// <summary>
	/// Stores the level in the PlayerPrefs
	/// </summary>
	public void StoreLevel()
	{
		string level = Id+","+IsCompleted+","+SpriteIndex;
		PlayerPrefs.SetString(Id, level);
	}

	/// <summary>
	/// Recovers the level from the PlayerPrefs and fills the level
	/// </summary>
	/// <returns><c>true</c>, if level was recovered, <c>false</c> otherwise.</returns>
	/// <param name="id">Identifier.</param>
	public bool RecoverLevel(string id)
	{
		string level = PlayerPrefs.GetString(id, "");
		if(level == "")
			return false;

		string[] levelParams = level.Split(',');
		this.Id = levelParams[0];
		this.IsCompleted = bool.Parse(levelParams[1]);
		this.SpriteIndex = int.Parse(levelParams[2]);
		this.gameObject.name = "Level"+this.Id;
		return true;
	}

	public void ProcessInput()
	{
		FindObjectOfType<MenuLevelManager>().LoadLevel(this.Id);
	}
}
