using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class PanelValidator : MonoBehaviour {
	public delegate void PanelEvent(bool isValid);
	
	public PanelEvent onPanelValidated;
	public Nonogram nonogram;
	private string[] _rows;
	private string[] _cols;
	public bool isValid;
	
	private bool[,] panelState;
	
	// Use this for initialization
	void Start () 
	{
		FindObjectOfType<LevelLoader>().onLevelLoaded += onLevelLoaded;
	}
	
	/// <summary>
	/// changes the state of the tile pressed in the local representation
	/// </summary>
	/// <param name="posX">Position x.</param>
	/// <param name="posY">Position y.</param>
	void onTilePressed(Tile tile,int posX, int posY)
	{
		if(tile.isPressed)
		{
			panelState [posX, posY] = true;
			if (validatePanel ()) {
				this.isValid = true;
				if (onPanelValidated != null)
					onPanelValidated (true);
			}
			else
			{
				this.isValid = false;
				if(onPanelValidated != null)
					onPanelValidated(false);
			}
		}
		else
			panelState [posX, posY] = false;
	}

	/// <summary>
	/// Validates the panel.
	/// </summary>
	/// <returns><c>true</c>, if panel was validated, <c>false</c> otherwise.</returns>
	bool validatePanel()
	{
		string[] rows = getRowsString();
		string[] cols = getColsString();

		//Compare the generated nonogram
		for (int i = 0; i < rows.Length; i++)
			if (rows [i] != _rows [i])
			if (onPanelValidated != null)
				return false;

		for (int i = 0; i < cols.Length; i++)
			if (cols [i] != _cols [i])
			if (onPanelValidated != null)
				return false;

		return true;

	}

	/// <summary>
	/// Gets the panel string for a row or a column
	/// </summary>
	/// <returns>The panel string.</returns>
	/// <param name="axisToCalculateLength">Axis to calculate length.</param>
	/// <param name="auxilaryAxis">Auxilary axis.</param>
	string[] getRowsString()
	{
		//Generate the Cols arrays
		string[] axis = new string[nonogram.sizeCol];
		for (int i = 0; i < nonogram.sizeCol; i++)
		{
			int consecutiveNumbers = 0;
			StringBuilder sb = new StringBuilder();
			
			for(int j = 0; j < nonogram.sizeRow; j++)
			{
				if(panelState[i,j])
					consecutiveNumbers++;
				else
				{
					if(consecutiveNumbers > 0)
					{
						sb.Append(consecutiveNumbers.ToString()).Append(" ");
						consecutiveNumbers = 0;
					}
				}
				
			}
			if(consecutiveNumbers > 0)
				sb.Append(consecutiveNumbers.ToString()).Append(" ");
			
			axis[i] = sb.ToString().Trim();
		}

		return axis;
	}



	string[] getColsString()
	{
		string[] axis = new string[nonogram.sizeCol];
		for (int i = 0; i < nonogram.sizeCol; i++)
		{
			int consecutiveNumbers = 0;
			StringBuilder sb = new StringBuilder();
			
			for(int j = 0; j < nonogram.sizeRow; j++)
			{
				if(panelState[j,i])
					consecutiveNumbers++;
				else
				{
					if(consecutiveNumbers > 0)
					{
						sb.Append(consecutiveNumbers.ToString()).Append(" ");
						consecutiveNumbers = 0;
					}
				}
				
			}
			if(consecutiveNumbers > 0)
				sb.Append(consecutiveNumbers.ToString()).Append(" ");
			
			axis[i] = sb.ToString().Trim();
		}
		
		return axis;
	}

	/// <summary>
	/// Ons the level loaded.
	/// </summary>
	/// <param name="nonogram">Nonogram.</param>
	private void onLevelLoaded(Nonogram nonogram)
	{
		this.nonogram = nonogram;
		
		_rows = nonogram.Rows;
		_cols = nonogram.Cols;
		panelState = new bool[nonogram.sizeRow,nonogram.sizeCol];
		foreach (Tile[] tile in nonogram.tiles) {
			foreach (Tile til in tile)
				til.onTilePressed += onTilePressed;
		}
	}
}
