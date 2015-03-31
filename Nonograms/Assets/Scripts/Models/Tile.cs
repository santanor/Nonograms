using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour
{
	public delegate void TileEvent(Tile tile,int positionX, int positionY);

	public TileEvent onTilePressed;
	public int positionX {get; set;}
	public int positionY {get; set;}
	public bool isPressed {get; set;}

	public void ProcessInput()
	{
		this.isPressed = true;
		if (onTilePressed != null)
			onTilePressed (this, this.positionX, this.positionY);
	}
}


