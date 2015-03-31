using UnityEngine;
using System.Collections;

public class Nonogram{

	public int sizeCol { get; private set; }
	public int sizeRow { get; private set; }
	public string[] Rows { get; private set; }
	public string[] Cols { get; private set; }
	public Tile[][] tiles { get; set; }

	public Nonogram(int sizeCol, int sizeRow, string[] Rows, string[] Cols)
	{
		this.sizeCol = sizeCol;
		this.sizeRow = sizeRow;
		this.Rows = Rows;
		this.Cols = Cols;
	}
}
