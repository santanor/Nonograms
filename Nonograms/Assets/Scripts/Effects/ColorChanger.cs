using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {

	public Color colorPressed;
	public Color colorUnPressed;
	public Color colorPanelValidated;

	void Start()
	{
		this.GetComponent<Tile>().onTilePressed += ChangeColor;
		FindObjectOfType<PanelValidator>().onPanelValidated += onPanelValidated;
	}

	/// <summary>
	/// Changes the color.
	/// </summary>
	/// <param name="tile">Tile.</param>
	/// <param name="unused">Unused.</param>
	/// <param name="unusedd">Unusedd.</param>
	private void ChangeColor(Tile tile, int unused, int unusedd)
	{
		if(!FindObjectOfType<PanelValidator>().isValid)
		{
			if(tile.isPressed)
			{
				Image image = this.GetComponent<Image>();
				image.color = colorPressed;
			}
			else
			{
				Image image = this.GetComponent<Image>();
				image.color = colorUnPressed;
			}
		}
	}

	/// <summary>
	/// Ons the panel validated.
	/// </summary>
	/// <param name="isValid">If set to <c>true</c> is valid.</param>
	private void onPanelValidated(bool isValid)
	{
		if(isValid)
		{
			if(this.GetComponent<Tile>().isPressed)
			{
				Image image = this.GetComponent<Image>();
				image.color = colorPanelValidated;
			}
		}

	}
}
