using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelCreator : MonoBehaviour {
	
	public GridLayoutGroup grid;
	public GridLayoutGroup rowTextGrid;
	public GridLayoutGroup colTextGrid;
	public GameObject basicTile;
	public GameObject nonogramInfoLabel;

	// Use this for initialization
	void Start () {
		FindObjectOfType<LevelLoader> ().onLevelLoaded += onLevelLoaded;
	}
	

	public void onLevelLoaded(Nonogram nonogram)
	{
		float width = grid.GetComponent<RectTransform> ().rect.width;
		float height = grid.GetComponent<RectTransform> ().rect.height;
		grid.cellSize = new Vector2 (width/ nonogram.sizeCol, height/nonogram.sizeRow);
		grid.constraintCount = nonogram.sizeCol;
		Tile[][] tiles = new Tile[nonogram.sizeRow][];

		for (int i = 0; i < nonogram.sizeRow; i++) 
		{
			tiles[i] = new Tile[nonogram.sizeCol];
			for (int j = 0; j < nonogram.sizeCol; j++) 
			{
				GameObject go = (GameObject)Instantiate (basicTile, Vector3.zero, Quaternion.identity);
				go.transform.parent = grid.transform;
				go.transform.localScale = Vector3.one;
				go.GetComponent<Tile>().positionX = i;
				go.GetComponent<Tile>().positionY = j;
				tiles [i][j] = go.GetComponent<Tile>();
			}
		}


		rowTextGrid.cellSize = new Vector2(rowTextGrid.cellSize.x, height/nonogram.sizeRow);
		foreach(string row in nonogram.Rows)
		{
			GameObject go = (GameObject) Instantiate(nonogramInfoLabel, Vector3.zero, Quaternion.identity);
			go.transform.parent = rowTextGrid.transform;
			go.transform.localScale = Vector3.one;
			go.GetComponent<Text>().text = row;
		}

		colTextGrid.cellSize = new Vector2(width/nonogram.sizeRow, colTextGrid.cellSize.y);
		foreach(string col in nonogram.Cols)
		{
			GameObject go = (GameObject) Instantiate(nonogramInfoLabel, Vector3.zero, Quaternion.identity);
			go.transform.parent = colTextGrid.transform;
			go.transform.localScale = Vector3.one;
			go.GetComponent<Text>().text = col;
		}

		nonogram.tiles = tiles;


	}
}
